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
});

//#region Eventos Selectores Filtro

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

                    // Coloca fecha efectiva en card de resultados de búsqueda
                    $("#card-mostrar-filtros-seleccionados [name='fechaEfectiva']").html(o.def_tfl_fefect);
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

function buscar()
{
    document.querySelector("#buscar").style.display = "none";
    document.querySelector("#resultados").style.display = "block";

    // Colocar descripciones en la Cabecera
    let periodoVigencia = $("#buscar [name='periodoVigencia'] option:selected").text();
    let direccionSectorial = $("#buscar [name='direccionSectorial'] option:selected").text();
    let area = $("#buscar [name='area'] option:selected").text();
    let tfl = $("#buscar [name='tfl'] option:selected").text();
    $("#resultados [name='periodoVigencia']").text(periodoVigencia);
    $("#resultados [name='direccionSectorial']").text(direccionSectorial);
    $("#resultados [name='area']").text(area);
    $("#resultados [name='tfl']").text(tfl);

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

        //if (errores.length > 0) {
        //    errores.reverse().forEach(x => toastr.error(x));
        //    return;
        //}
    }

    console.log(params);
    construirCards();
}

function construirCards()
{
    this.listaObjetos = [1, 2, 3, 4].map(x =>
    {
        return {
            titulo: `Etapa ${x}`,
            items: [1, 2, 3, 4, 5, 6, 7, 8, 9].map(y =>
            {
                return {
                    titulo: `Pantalla ${x}${y}`,
                };
            })
        };
    });

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
                            return `<li class="mr-0 card-header d-flex align-items-baseline justify-content-between" id="iconos-hechos">
                                <div>
                                    <i class="material-icons icon-lg mr-1 success-text">done</i>
                                    <span class="d-md-inline">${y.titulo}</span>
                                </div>
                                <a href="#" data-toggle="modal" data-target="#ModalVolverAtras" class="material-icons icon-lg ml-1 info-text undo-icon">undo</a>
                            </li>`;
                        }).join("")}

                    </ul>
                </div>
            </div>
        </div>`;
    }).join("");

    document.querySelector("#contenidoCards").innerHTML = cadena;
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


