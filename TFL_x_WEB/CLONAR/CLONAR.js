let usarDatosDummy = false;  // false;
let paramsFiltro = {};
let sePuedeModificar = true;
this.p_def_tfl_version = 0;

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

    $("#modificarTFL [name='fechaEfectiva']").datepicker({ language: "es", autoClose: true });

    if (errorCarga != "") {
        mostrarErroresRespuestaBackend({ objeto: null, errores: [errorCarga], status: 500 });
    }
});

//#region Eventos Selectores Filtro

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
            url: "CLONAR.aspx/DEF_AREA_SEL",
            data: JSON.stringify({ direccionSectorialNcorr: direccionSectorialNcorr }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: () => showLoading(),
            success: (res) =>
            {
                if (res.status == 200)
                    llenarCombobox(`#buscar [name="area"]`, res.objeto); 
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

$("#buscar [name='tfl']").on("change", (e) =>
{
    let def_tfl_ncorr = e.currentTarget.value;

    if (validarNuloVacio(def_tfl_ncorr))
    {
        this.p_def_tfl_version = findVersionFromSelect("#buscar [name='tfl']");

        /*
        $.ajax({
            method: "POST",
            url: "CLONAR.aspx/DEF_TFL_LEE",
            data: JSON.stringify({ def_tfl_ncorr }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: () => showLoading(),
            success: (res) =>
            {
                this.p_def_tfl_version = res.objeto.def_tfl_version;
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => {
                toastr.error("Ocurrió un error al obtener la información de la TFL");
            },
            complete: () => hideLoading()
        });
        */
    }
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
        url: "CLONAR.aspx/DEF_TFL_SEL",
        data: JSON.stringify({ defAreaNcorr, defPvigenciaTFL_ncorr }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: () => showLoading(),
        success: (res) =>
        {
            if (res.status == 200)
                llenarCombobox(`#buscar [name="tfl"]`, res.objeto);
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

        if (errores.length > 0) {
            errores.reverse().forEach(x => toastr.error(x));
            return;
        }
    }

    showLoading();

    try
    {
        let res = await traeEstadoTFL(params.tfl);
        let infoTFL = res.infoTFL;

        /*
        if (res.resSP.swt == 2) 
            definirMensaje({ mostrar: true, icono: "lock", mensaje: res.resSP.msg });
        else 
            definirMensaje({ mostrar: false });
        */

        $("#buscar").hide();
        $("#resultados").show();

        $("#resultados [name='nombre_tfl']").text(infoTFL.nombre_tfl);
        $("#resultados [name='estado_tfl']").text(infoTFL.estado_tfl);
        $("#resultados [name='fecha_efectiva_tfl']").text(infoTFL.fecha_efectiva);
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
            url: "CLONAR.aspx/TRAE_ESTADO_TFL", 
            data: JSON.stringify({
                p_def_tfl_ncorr,
                p_def_tfl_version: this.p_def_tfl_version
            }),
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

//#region Clonar

function abrirModalClonar() {
    $("#ModalClonar").modal("show");
}

async function clonar()  // clonar()
{
    let card = $("#modificarTFL");

    let periodoVigencia = card.find("[name='periodoVigencia_Clonar']").val();
    let nombreTFL = card.find("[name='nombreTFL']").val();
    let numeroMaximoCualificaciones = card.find("[name='numeroMaximoCualificaciones']").val();
    let numeroMaximoUCLs = card.find("[name='numeroMaximoUCLs']").val();
    let fechaEfectiva = card.find("[name='fechaEfectiva']").val();
    let descripcion = card.find("[name='descripcion']").val();

    let errores = [];

    if (!validarNuloVacio(periodoVigencia))
        errores.push("Período Vigencia es requerido");

    if (!validarNuloVacio(nombreTFL))
        errores.push("Nombre TFL es requerido");

    if (!validarNuloVacio(numeroMaximoCualificaciones))
        errores.push("Número máximo cualificaciones es requerido");

    if (!validarNuloVacio(numeroMaximoUCLs))
        errores.push("Número máximo UCLs es requerido");

    if (!validarNuloVacio(fechaEfectiva))
        errores.push("Fecha efectiva es requerido");

    if (!validarNuloVacio(descripcion))
        errores.push("Descripción es requerido");

    if (errores.length > 0) {
        errores.reverse().forEach(error => toastr.error(error));
        return;
    }

    fechaEfectiva = fechaEfectiva.split("/").reverse().join("");      // Queda en formato YYYYMMDD

    showLoading();

    try
    {
        let res = await CLONAR_BACKEND({
            p_def_tfl_ncorr: $("#buscar [name='tfl']").val(),
            p_def_tfl_version: this.p_def_tfl_version,
            p_nperiodo: periodoVigencia,                            
            p_def_tfl_nombre: nombreTFL,                            
            p_def_tfl_ncualficaciones: numeroMaximoCualificaciones, 
            p_def_tfl_nucl: numeroMaximoUCLs,                       
            p_def_tfl_fefect: fechaEfectiva,                        
            p_def_tfl_descrip: descripcion                          
        });

        toastr.success(res.mensajeExito);
        limpiarFormulario("#modificarTFL");
    }
    catch (ex) {
        if (ex.errores != null) mostrarErroresRespuestaBackend(ex);
        else toastr.error(ex);
    }
    finally { hideLoading() }
}

function CLONAR_BACKEND(objeto = {})
{
    return new Promise((resolve, reject) => 
    {
        $.ajax({
            method: "POST",
            url: "CLONAR.aspx/CLONAR_BACKEND",
            data: JSON.stringify(objeto),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) =>
            {
                if (res.status == 200)
                    resolve(res);
                else 
                    reject(res);
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => reject("Ocurrió un error al clonar")
        });
    });
}

//#endregion Clonar

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