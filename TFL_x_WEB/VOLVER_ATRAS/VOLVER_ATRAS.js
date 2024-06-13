this.listaObjetos = [];

let usarDatosDummy = false;  // false;
let paramsFiltro = {};
let sePuedeModificar = true;

$(document).ready(function ()
{
    $("body").tooltip({ selector: "[data-toggle=tooltip]", trigger: "hover" });  // Habilitar tooltips

    $(".chosen-select").chosen({
        disable_search_threshold: 10,
        no_results_text: "Sin Resultados para: ",
        width: "100%"
    });

    $('[data-toggle="tooltip"]').tooltip();
    $('.mdb-select').material_select();

    if (errorCarga != "") {
        mostrarErroresRespuestaBackend({ objeto: null, errores: [errorCarga], status: 500 });
    }
});

//#region Eventos Selectores Filtro

/*
$("#buscar [name='tfl']").on("change", () =>  // Obtener datos de la tfl seleccionada en el filtro
{
    let tfl = $("#buscar [name='tfl']").val();
    let colFechaEfectiva = document.querySelector("#buscar [name='col-fecha-efectiva']");

    if (validarNuloVacio(tfl))
    {
        $.ajax({
            method: "POST",
            url: "VOLVER_ATRAS.aspx/GetDatosDefTFL",
            data: JSON.stringify({ def_tfl_ncorr: tfl }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: () => showLoading(),
            success: (res) => {
                if (res.status == 200) {
                    let o = res.objeto;

                    console.log(`def_tfl_nombre: ${o.def_tfl_nombre}`);
                    console.log(`def_tfl_descrip: ${o.def_tfl_descrip}`);
                    console.log(`def_tfl_version: ${o.def_tfl_version}`);
                    console.log(`def_tfl_fefect: ${o.def_tfl_fefect}`);

                    $("#buscar [name='fechaEfectiva']").html(o.def_tfl_fefect);
                    colFechaEfectiva.style.visibility = "visible";     // Hace visible la fecha efectiva
                }
                else {
                    mostrarErroresRespuestaBackend(res);
                }
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => {
                toastr.error("Ocurrió un error al obtener los datos de DEF_TFL");
            },
            complete: () => hideLoading()
        });
    }
    else {
        colFechaEfectiva.style.visibility = "hidden";   // Oculta la fecha efectiva
    }
});
*/

$("#buscar [name='direccionSectorial']").on("change", () =>  // Al seleccionar un option para filtrar por DIR_SEC_VRA
{
    let direccionSectorialNcorr = $("#buscar [name='direccionSectorial']").val();
    limpiarCombobox('#buscar [name="area"]');
    limpiarCombobox('#buscar [name="tfl"]');
    document.querySelector("#buscar [name='col-fecha-efectiva']").style.visibility = "hidden";

    if (validarNuloVacio(direccionSectorialNcorr))
    {
        $.ajax({
            method: "POST",
            url: "VOLVER_ATRAS.aspx/GetCboDefArea",
            data: JSON.stringify({ direccionSectorialNcorr: direccionSectorialNcorr }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: () => showLoading(),
            success: (res) =>
            {
                if (res.status == 200)
                    llenarCombobox(`#buscar [name="area"]`, res.objeto.cboCboDefArea);
                else {
                    mostrarErroresRespuestaBackend(res);
                }
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => {
                toastr.error("Ocurrió un error al obtener las áreas por su Dirección Sectorial");
            },
            complete: () => hideLoading()
        });
    }
});

$("#buscar [name='periodoVigencia']").on("change", () =>  // Al seleccionar un option para filtrar por Período Vigencia Diseño de TFL
{
    document.querySelector("#buscar [name='col-fecha-efectiva']").style.visibility = "hidden";
    llenarSelectorDef_TFL();
});

$("#buscar [name='area']").on("change", () =>  // Al seleccionar un option para filtrar por DIR_SEC_VRA
{
    document.querySelector("#buscar [name='col-fecha-efectiva']").style.visibility = "hidden";
    llenarSelectorDef_TFL();
});

function llenarSelectorDef_TFL()
{
    let defPvigenciaTFL_ncorr = $("#buscar [name='periodoVigencia']").val();
    let defAreaNcorr = $("#buscar [name='area']").val();
    limpiarCombobox('#buscar [name="tfl"]');

    if (!validarNuloVacio(defAreaNcorr) || !validarNuloVacio(defPvigenciaTFL_ncorr))
        return;

    $.ajax({
        method: "POST",
        url: "VOLVER_ATRAS.aspx/GetCboDefTFL",
        data: JSON.stringify({ defAreaNcorr, defPvigenciaTFL_ncorr }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: () => showLoading(),
        success: (res) =>
        {
            if (res.status == 200)
                llenarCombobox(`#buscar [name="tfl"]`, res.objeto.cboDefTFL);
            else {
                mostrarErroresRespuestaBackend(res);
            }
        },
        error: (XMLHttpRequest, textStatus, errorThrown) => {
            toastr.error("Ocurrió un error al obtener las áreas por su Dirección Sectorial");
        },
        complete: () => hideLoading()
    });
}

//#endregion Eventos Selectores Filtro

async function buscar()
{
    // Colocar descripciones en la Cabecera
    let periodoVigencia = $("#buscar [name='periodoVigencia'] option:selected").text();
    let direccionSectorial = $("#buscar [name='direccionSectorial'] option:selected").text();
    let area = $("#buscar [name='area'] option:selected").text();
    //let tfl = $("#buscar [name='tfl'] option:selected").text();
    $("#resultados [name='periodoVigencia']").text(periodoVigencia);
    $("#resultados [name='direccionSectorial']").text(direccionSectorial);
    $("#resultados [name='area']").text(area);

    let card = $("#buscar");

    let params = {
        usarDatosDummy,
        periodoVigencia: card.find("[name='periodoVigencia']").val(),
        direccionSectorial: card.find("[name='direccionSectorial']").val(),
        area: card.find("[name='area']").val(),
        tfl: card.find("[name='tfl']").val(),
        fechaEfectiva: card.find("[name='fechaEfectiva']").html()
    };

    if (!usarDatosDummy)
    {
        let errores = [];

        if (!validarNuloVacio(params.periodoVigencia))
            errores.push("El período vigencia es requerido");

        if (!validarNuloVacio(params.direccionSectorial))
            errores.push("La dirección sectorial es requerida");

        if (!validarNuloVacio(params.area))
            errores.push("El área es requerida");

        if (!validarNuloVacio(params.tfl))
            errores.push("La TFL es requerida");

        /*
        if (!validarNuloVacio(params.fechaEfectiva))
            errores.push("La fecha efectiva es requerida");

        if (!validarNuloVacio(params.sectorProductivo))
            errores.push("El sector productivo es requerido");
        */

        if (errores.length > 0) {
            errores.reverse().forEach(x => toastr.error(x));
            return;
        }
    }

    showLoading();

    try {
        let res = await traeEstadoTFL(params.tfl);
        // console.log(res);

        this.listaObjetos = res.listaObjetos;
        let infoTFL = res.infoTFL;
        let habilitadoVolverAtras = true;

        if (res.resSP.swt == 2) 
        {
            habilitadoVolverAtras = false;
            definirMensaje({ mostrar: true, icono: "lock", mensaje: res.resSP.msg });
        }
        else 
            definirMensaje({ mostrar: false });
        
        $("#buscar").hide();
        $("#resultados").show();

        $("#resultados [name='nombre_tfl']").text(infoTFL.nombre_tfl);
        $("#resultados [name='estado_tfl']").text(infoTFL.estado_tfl);
        $("#resultados [name='fecha_efectiva_tfl']").text(infoTFL.fecha_efectiva);

        //=====================>>>

        let cadena = this.listaObjetos.map(x =>
        {
            return `<div class="col-md-3 mb-4">
                <div class="card card-home">
                    <div class="card-header">
                        <h3 class="h3-responsive">${x.titulo}</h3>
                    </div>

                    <div class="card-body">
                        <ul class="list-card">

                            ${x.items.map(y =>
                            {
                                // Solamente los que están publicados pueden volver atras (Los que tienen fecha de publicación)

                                let iconoCheck = "";

                                /*
                                if (y.publicada == 1) // Tiene Datos pero no está Publicado
                                {
                                    iconoCheck = `<i class="material-icons icon-lg mr-1" 
                                    data-html="true" data-toggle="tooltip" data-placement="top" data-original-title="Tiene Datos"
                                    style="color: #999292; font-weight: bold; cursor: pointer">done</i>`;
                                }
                                */
                                
                                if (y.publicada == 2) // Está Publicado
                                {
                                    iconoCheck = `<i class="material-icons icon-lg mr-1 success-text" 
                                    data-html="true" data-toggle="tooltip" data-placement="top" data-original-title="Publicado"
                                    style="font-weight: bold; cursor: pointer">done</i>`;
                                }
                                
                                let cssVolverAtras = (y.publicada == 2 && habilitadoVolverAtras) ? "visibility: visible" : "visibility: hidden";

                                /*
                                publicada:
                                    0 NO TIENE DATOS
                                    1 TIENE DATOS
                                    2 PUBLICADO

                                El volver atras debe estar habilitado solo cuando el publicada es 2 
                                La columna  volver_atras   se va a eliminar
                                */

                                return `<li class="mr-0 card-header d-flex align-items-baseline justify-content-between">
                               
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 1em; font-size: 1em">${iconoCheck}</td>
                                            <td style="width: 83%">
                                                <p style="font-size: 14px" class="d-md-inline">${y.titulo}</p>
                                            </td>
                                            <td style="font-size: 1em">
                                                <a href="#" onclick="abrirModalVolverAtras(event, '${y.apli_caplicacion}')" 
                                                style="font-weight: bold; ${cssVolverAtras}"
                                                data-html="true" data-toggle="tooltip" data-placement="top" data-original-title="Volver Atrás"
                                                class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                            </td>
                                        </tr>
                                    </table>

                                </li>`;

                                /*
                                return `<li class="mr-0 card-header d-flex align-items-baseline justify-content-between" id="iconos-hechos">
                                    <div>
                                        <i style="${cssIconoCheck}" class="material-icons icon-lg mr-1 success-text">done</i>
                                        <span class="d-md-inline">${y.titulo}</span>
                                    </div>

                                    <a href="#" onclick="abrirModalVolverAtras(event, '${y.apli_caplicacion}')" style="${cssVolverAtras}"
                                    class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                                </li>`;
                                */
                            }).join("")}

                        </ul>
                    </div>
                </div>
            </div>`;
        }).join("");

        document.querySelector("#contenidoCards").innerHTML = cadena;

        //=====================>>>
    }
    catch (ex) {
        if (ex.errores != null) mostrarErroresRespuestaBackend(ex);
        else toastr.error(ex);
    }
    finally { hideLoading() }
}

function realizarNuevaBusqueda()
{
    $("#buscar").show();
    $("#resultados").hide();
    llenarSelectorDef_TFL();
}

function traeEstadoTFL(p_def_tfl_ncorr)
{
    return new Promise((resolve, reject) => 
    {
        $.ajax({
            method: "POST",
            url: "VOLVER_ATRAS.aspx/TRAE_ESTADO_TFL", 
            data: JSON.stringify({ p_def_tfl_ncorr }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) =>
            {
                if (res.status == 200)
                    resolve(res.objeto);
                else 
                    reject(res);
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => reject("Ocurrió un error al obtener la información")
        });
    });
}

function obtenerMensajeVolverAtras(apli_caplicacion)
{
    return new Promise((resolve, reject) => 
    {
        $.ajax({
            method: "POST",
            url: "VOLVER_ATRAS.aspx/ObtenerMensajeVolverAtras",
            data: JSON.stringify({ p_apli_caplicacion: apli_caplicacion }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) =>
            {
                if (res.status == 200)
                    resolve(res.objeto);
                else 
                    reject(res);
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => reject("Ocurrió un error al obtener la información")
        });
    });
}

async function abrirModalVolverAtras(e, apli_caplicacion)
{
    e.preventDefault();
    let modal = $("#ModalVolverAtras");

    showLoading();

    try {
        let res = await obtenerMensajeVolverAtras(apli_caplicacion);
        //let mensaje = "Estás a punto de volver atrás en el proceso. Recuerda que al volver atrás, el proceso se reiniciará desde donde fue seleccionado para volver atrás, y el resto de los datos se guardarán en modo borrador, así que podrás retomar justo donde lo dejaste en cualquier momento. ¿Estás seguro de que deseas continuar con esta acción?";

        modal.find("[name='contenido']").html(res.mensaje);

        modal.find("[name='apli_caplicacion']").val(apli_caplicacion);
        modal.modal("show");
    }
    catch (ex) {
        if (ex.errores != null) mostrarErroresRespuestaBackend(ex);
        else toastr.error(ex);
    }
    finally { hideLoading() }
}

async function volverAtras()
{
    let p_def_tfl_ncorr = $("#buscar [name='tfl']").val();
    console.log("p_def_tfl_ncorr", p_def_tfl_ncorr);

    let p_apli_caplicacion = $("#ModalVolverAtras [name='apli_caplicacion']").val();
    console.log("p_apli_caplicacion", p_apli_caplicacion);

    showLoading();

    try {
        let res = await VOLVER_ATRAS_BACKEND(p_def_tfl_ncorr, p_apli_caplicacion);
        console.log(res);

        toastr.success(res.mensajeExito);
        $("#ModalVolverAtras").modal("hide");
        buscar();
    }
    catch (ex) {
        if (ex.errores != null) mostrarErroresRespuestaBackend(ex);
        else toastr.error(ex);
    }
    finally { hideLoading() }
}

function VOLVER_ATRAS_BACKEND(p_def_tfl_ncorr, p_apli_caplicacion)
{
    return new Promise((resolve, reject) => 
    {
        $.ajax({
            method: "POST",
            url: "VOLVER_ATRAS.aspx/VOLVER_ATRAS_BACKEND",
            data: JSON.stringify({ p_def_tfl_ncorr, p_apli_caplicacion }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) =>
            {
                if (res.status == 200) {
                    resolve(res);
                    //resolve(res.objeto);
                }
                else 
                    reject(res);
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => reject("Ocurrió un error al realizar la operación")
        });
    });
}

function definirMensaje(datos = {})
{
    // definirMensaje({ mostrar: false });
    // definirMensaje({ mostrar: true, icono: "warning", mensaje: "Hay duplicados. Revisar" });
    // definirMensaje({ mostrar: true, icono: "lock", mensaje: "No se puede modificar" });

    let querySelector = datos.querySelector ?? "#resultados #mensaje";
    let mensajeNoModificar = document.querySelector(querySelector);

    if (datos.mostrar && datos.mensaje != null)
    {
        mensajeNoModificar.style.cssText = "visibility: visible";  // Muestra mensaje "Análisis de Demanda publicada, no se puede modificar"
        let p = Array.from(mensajeNoModificar.children).find(x => x.tagName.toLowerCase() == "p");
        let i = Array.from(mensajeNoModificar.children).find(x => x.tagName.toLowerCase() == "i");

        p.textContent = datos.mensaje;
        i.textContent = datos.icono ?? "error";

        let color = (datos.icono == "warning") ?
            "#ffa000"   // Amarillo
            : "#c00";   // Rojo

        p.style.color = color;
        i.style.color = color;
    }
    else {
        mensajeNoModificar.style.cssText = "visibility: hidden";
    }
}

//#region Funciones Generales

function showLoading() {
    $('#loadingOverlay').show();
}

function hideLoading() {
    $('#loadingOverlay').hide();
}

function validarNuloVacio(contenido)
{
    if (contenido == null || contenido == "")
        return false;
    else
        return true;
}

const limpiarFormulario = (querySelector) =>
{
    Array.from($(querySelector).find(":input"))
        .filter(x => x.className.includes("form-control") && x.attributes.getNamedItem("no-limpiar") == null).forEach(x => {
            x.value = "";
        });

    Array.from($(querySelector).find("select"))
        .filter(x => x.attributes.getNamedItem("no-limpiar") == null).forEach(x => {
            $(`${querySelector} [name='${x.name}']`).find("option:first-child").prop("selected", true).trigger("chosen:updated");
        });
};

function limpiarCombobox(querySelector) {
    $(querySelector + ' option[value!=""]').remove().end();    // Remueve todos los options excepto el seleccione
    $(querySelector).trigger("chosen:updated");
}

function llenarCombobox(querySelector, arreglo)
{
    if (!Array.isArray(arreglo))
        return console.log("La función llenarCombobox debe recibir un arreglo");

    let select = $(querySelector);
    select.find("option[value!='']").remove().end();
    arreglo.forEach(x => select.append(`<option value="${x.codigo}">${x.descripcion}</option>`))  // El arreglo debe tener los elementos codigo y descripcion
    select.trigger("chosen:updated");
}

//#endregion Funciones Generales


