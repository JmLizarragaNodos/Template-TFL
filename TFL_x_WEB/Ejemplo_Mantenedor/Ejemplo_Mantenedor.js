
this.objeto = {};

$(document).ready(function ()
{
    // Activar selects
    $(".chosen-select").chosen({ disable_search_threshold: 10, no_results_text: "Sin Resultados para: ", width: "100%" });
    $(".mdb-select").material_select();

    $("#formCrear [name='fechaEfectiva']").datepicker({ language: "es", autoClose: true });
    $("#formFiltrar [name='fechaEfectiva']").datepicker({ language: "es", autoClose: true });
    $("#formEditar [name='fechaEfectiva']").datepicker({ language: "es", autoClose: true });
});


$("#formCambiarEstado [name='estado']").on("change", (e) =>  // Al seleccionar un option en em modal para cambiar estado
{
    if (e.currentTarget.value != this.objeto.estado)
    {
        $("#btnCambiarEstado").removeAttr("disabled")
            .attr("class", "btn btn-default waves-effect waves-light");  // Habilita botón y lo oscurece
    }
    else {
        $("#btnCambiarEstado").attr("disabled", true).attr("class", "btn");     // Deshabilita botón y lo aclara
    }
});

function obtenerDatosGrilla(params) 
{
    return new Promise((resolve, reject) => 
    {
        $.ajax({
            method: "POST",
            url: "Ejemplo_Mantenedor.aspx/PRUEBA_SEL",
            data: JSON.stringify(params),
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

async function construirGrilla()
{
    let formFiltrar = $("#formFiltrar");

    let filtroAplicado = {
        codigo: formFiltrar.find("[name='codigo']").val(),
        estado: formFiltrar.find("[name='estado']").val(),
        fechaEfectiva: formFiltrar.find("[name='fechaEfectiva']").val(),
        entradas: formFiltrar.find("[name='entradas']").val()
    };

    if (!validarNuloVacio(filtroAplicado.entradas))
        return toastr.error("La cantidad de entradas a leer es requerida");

    document.querySelector("#seccionGrilla").style.display = "block";
    document.querySelector("#seccionInsertar").style.display = "none";

    showLoading();
    limpiarFormulario("#formCrear");

    try
    {
        let res = await obtenerDatosGrilla(filtroAplicado);
        hideLoading();

        $("#TablaPrincipal").DataTable({
	        data: res,
	        destroy: true,
	        pageLength: 10,
	        dom: '<"top top-grey"<"dataTables_actions"f>> <t> <"bottom mt-2 d-flex align-items-center justify-content-between flex-wrap"<"d-flex" il>p>',
	        //scrollY: "60vh",
	        "scrollX": true, 
	        //lengthMenu: [[10, 20, 30], [10, 20, 30]],
            bLengthChange: false, // Oculta el select de registros por página
            columnDefs: [
                { "width": "215px", "targets": 4 }  // Ancho de la columna acciones
            ],
	        aaSorting: [],
	        processing: true,
	        //serverSide: true, 
	        filter: true,
	        orderMulti: false,
	        responsive: true,
	        fnDrawCallback: () =>
            {
                let recordsTotal = $("#TablaPrincipal").DataTable().page.info().recordsDisplay      // Al usar paginacion front

                if (recordsTotal > 0)
                {
                    $("#TablaPrincipal_filter").show();            // Muestra search box
                    $("#table-search").attr("maxlength", "20");    // Search box maxlength

                    $("#TablaPrincipal_info, #TablaPrincipal_paginate, #TablaPrincipal_length, #divDescargaReportes").show();

                    if (!$("#divDescargaReportes").length)   // Si no existe, lo crea
                    {
                        let divBotonesTabla = `
                        <div id="divDescargaReportes" class="dataTables_actions-buttons">
                            <button type="button" onclick="descargarExcel()"
                            class="btn btn-round btn-outline dataTables_actions-button waves-effect waves-light" data-toggle="tooltip"
                            data-placement="top" title="Descargar Excel">
                                <i class="material-icons icon-1x">arrow_downward</i>
                            </button>
                        </div>
                        `;

                        $("#TablaPrincipal_wrapper .dataTables_actions").append(divBotonesTabla);
                    }
                    else {
                        $("#divDescargaReportes").show();
                    }
                }
                else {
                    $("#TablaPrincipal_info, #TablaPrincipal_paginate, #TablaPrincipal_length, #divDescargaReportes").hide();
                }
            },
	        columns: [
                { "className": "text-nowrap", "render": (data, type, row, meta) => row.nombre }, 
		        { "className": "text-nowrap", "render": (data, type, row, meta) => row.descripcion },
                { "className": "text-nowrap", "render": (data, type, row, meta) => row.fechaEfectiva },
                { "className": "text-nowrap", "render": (data, type, row, meta) => row.estado },
                {
                    "className": "text-nowrap", "sortable": false, "render": (data, type, row, meta) =>
                    {
                        let cadena = "";

                        if (row.estado.toUpperCase().startsWith("A"))  // Activo
                        {
                            cadena += `
                            <a onclick="abrirModalEditar(${row.codigo})" class="mr-0 mr-md-4 d-flex align-items-center float-left">
                                <i class="material-icons mr-1 icon-lg grey-text">edit</i>
                                <span class="d-none d-md-inline">Modificar</span>
                            </a>
                            `;
                        }

                        cadena += `
                        <a onclick="abrirModalCambiarEstado(${row.codigo})" class="mr-0 mr-md-4 d-flex align-items-center float-left">
                            <i class="material-icons mr-1 icon-lg grey-text">assignment</i>
                            <span class="d-none d-md-inline">Cambiar Estado</span>
                        </a>
                        `;

                        return cadena;
                    }
                }
	        ]
        })
        .draw(false);
    }
    catch (e) {

    }
}

function volverBuscar()
{
    document.querySelector("#seccionInsertar").style.display = "block";
    document.querySelector("#seccionGrilla").style.display = "none";

    // limpiarFormulario("#formFiltrar");  // Con esto se limpia el filtro de búsqueda
}

function validar()
{
    let errores = [];

    if (!validarNuloVacio(this.objeto.nombre))
        errores.push("El nombre es requerido");

    if (!validarNuloVacio(this.objeto.fechaEfectiva))
        errores.push("La fecha efectiva es requerida")

    if (!validarNuloVacio(this.objeto.descripcion))
        errores.push("La descripción es requerida");

    errores.reverse().forEach(error => toastr.error(error));

    return (errores.length == 0);
}

/*
async function agregarNuevo()  // Esta es otra forma mas corta (Usa async porque ocupa await)
{
    $("#formCrear").serializeArray().forEach(c => this.objeto[c.name] = c.value);

    if (!validar()) return;

    showLoading();

    try {
        let mensajeExito = await httpPost("Ejemplo_Mantenedor.aspx/PRUEBA_GRA", this.objeto, "mensajeExito");
        toastr.success(mensajeExito);
        limpiarFormulario("#formCrear");
        this.objeto = {};
    }
    catch (ex) {
        mostrarErroresRespuestaBackend(ex);
    }
    finally { hideLoading() }
}
*/


function agregarNuevo()
{
    $("#formCrear").serializeArray().forEach(c => this.objeto[c.name] = c.value);

    if (!validar()) return;

    showLoading();

    $.ajax({
        method: "POST",
        url: "Ejemplo_Mantenedor.aspx/PRUEBA_GRA",
        data: JSON.stringify(this.objeto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (res) =>
        {
            if (res.status == 200)
            {
                toastr.success(res.mensajeExito);
                limpiarFormulario("#formCrear");
                this.objeto = {};
            }
            else {
                mostrarErroresRespuestaBackend(res);
            }  
        },
        complete: (res) => hideLoading()
    });
}

/*
function obtener(ncorr)  // Esta es otra forma mas corta
{
    return httpPost("Ejemplo_Mantenedor.aspx/PRUEBA_LEE", { ncorr });
}
*/

function obtener(ncorr)
{
    return new Promise((resolve, reject) =>
    {
        $.ajax({
            method: "POST",
            url: "Ejemplo_Mantenedor.aspx/PRUEBA_LEE",
            data: JSON.stringify({ ncorr }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) =>
            {
                if (res.status == 200) {
                    resolve(res.objeto);
                }
                else {
                    reject(res);
                }   
            }
        });
    });
}

function abrirModalEditar(ncorr)
{
    this.objeto = {};  // Limpia el objeto
    showLoading();

    obtener(ncorr).then(o =>
    {
        this.objeto = {
            ncorr: ncorr,
            nombre: o.nombre,
            fechaEfectiva: (validarNuloVacio(o.fechaEfectiva)) ? o.fechaEfectiva.substring(0, 10).replaceAll("-", "/") : "",
            descripcion: o.descripcion,
            estado: o.estado
        };

        $("#formEditar [name='nombre']").val(this.objeto.nombre);
        $("#formEditar [name='descripcion']").val(this.objeto.descripcion);
        $("#formEditar [name='estado']").val(this.objeto.estado).trigger("chosen:updated");
        $("#formEditar [name='fechaEfectiva']").val(this.objeto.fechaEfectiva);

        $("#ModalEditar").modal("show");
    })
    .catch(res => {
        mostrarErroresRespuestaBackend(res);
    })
    .finally(() => {
        hideLoading();
    });
}

/*
async function actualizar()  // Esta es otra forma mas corta (Usa async porque ocupa await)
{
    $("#formEditar").serializeArray().forEach(c => this.objeto[c.name] = c.value);  // Asigna al objeto los valores del formulario

    if (!validar()) return;

    showLoading();

    try {
        let mensajeExito = await httpPost("Ejemplo_Mantenedor.aspx/PRUEBA_UPD", this.objeto, "mensajeExito");

        cerrarModal("#ModalEditar");
        toastr.success(mensajeExito);
        this.objeto = {};
        limpiarFormulario("#formEditar");

        construirGrilla();   
    }
    catch (ex) {
        mostrarErroresRespuestaBackend(ex);
    }
    finally { hideLoading() }
}
*/

function actualizar()
{
    $("#formEditar").serializeArray().forEach(c => this.objeto[c.name] = c.value);  // Asigna al objeto los valores del formulario

    if (!validar()) return;

    showLoading();

    $.ajax({
        method: "POST",
        url: "Ejemplo_Mantenedor.aspx/PRUEBA_UPD",
        data: JSON.stringify(this.objeto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (res) =>
        {
            if (res.status == 200)
            {
                cerrarModal("#ModalEditar");
                toastr.success(res.mensajeExito);
                this.objeto = {};
                limpiarFormulario("#formEditar");

                construirGrilla();
            }
            else {
                hideLoading();
                mostrarErroresRespuestaBackend(res);
            }  
        },
        error: (XMLHttpRequest, textStatus, errorThrown) => {
            toastr.error("Ocurrió un error");
            hideLoading();
        }
    });
}

function abrirModalCambiarEstado(ncorr)
{
    showLoading();

    obtener(ncorr).then(o =>
    {
        //console.log(o);
        this.objeto = { ncorr: ncorr, estado: o.estado };

        $("#formCambiarEstado [name='estado']").val(this.objeto.estado).trigger("chosen:updated");

        // Colocar descripciones en el modal para orientar al usuario
        $("#ModalCambiarEstado [name='nombreDesplegado']").html(o.nombre);

        $("#ModalCambiarEstado").modal("show");

        $("#btnCambiarEstado").attr("disabled", true);   // Deshabilita botón
        $("#btnCambiarEstado").attr("class", "btn");     // Aclara color del botón
    })
    .catch(res => {
        mostrarErroresRespuestaBackend(res);
    })
    .finally(() => {
        hideLoading();
    });
}

/*
function cambiarEstado()  // Esta es otra forma mas corta (No usa async porque ocupa then)
{
    showLoading();

    httpPost("Ejemplo_Mantenedor.aspx/PRUEBA_DEL", { kya: this.objeto.ncorr }, "mensajeExito")
    .then(mensajeExito =>
    {
        this.objeto = {};
        cerrarModal("#ModalCambiarEstado");
        toastr.success(mensajeExito);

        construirGrilla();
    })
    .catch(res => 
    {
        hideLoading();
        mostrarErroresRespuestaBackend(res)
    });
    //.finally(() => hideLoading());  // Del hideLoading se encarga el método construirGrilla
}
*/

function cambiarEstado()
{
    showLoading();

    $.ajax({
        method: "POST",
        url: "Ejemplo_Mantenedor.aspx/PRUEBA_DEL",
        data: JSON.stringify({ kya: this.objeto.ncorr }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (res) =>
        {
            if (res.status == 200)
            {
                this.objeto = {};
                cerrarModal("#ModalCambiarEstado");
                toastr.success(res.mensajeExito);

                construirGrilla();
            }
            else {
                hideLoading();
                mostrarErroresRespuestaBackend(res);
            }  
        },
        error: (XMLHttpRequest, textStatus, errorThrown) => {
            toastr.error("Ocurrió un error");
            hideLoading();
        }
    });
}

async function obtenerFiltros()
{
    if (document.querySelector("#collapseFiltrar").className.includes("show"))  // No consulta filtros si el item del acordeon está abierto
        return;

    showLoading();
    $("#formFiltrar").css("display", "none");

    try {
        let arreglo = await httpPost("Ejemplo_Mantenedor.aspx/GetCombobox");
        llenarCombobox(`#formFiltrar [name="codigo"]`, arreglo);

        $("#formFiltrar").css("display", "block");
    }
    catch (ex) {
        mostrarErroresRespuestaBackend(ex);
    }
    finally { hideLoading() }
}

function descargarExcel()
{
    let data = [
        ["Nombre", "Estado", "Descripción", "Fecha Efectiva"]
    ];

    Array.from($("#TablaPrincipal").DataTable().rows({ filter: "applied" }).data()).forEach(x =>
    {
        data.push([
            x.nombre,
            x.estado,
            x.descripcion,
            x.fechaEfectiva
        ]);
    });

    cerrarModal("#ModalDescargar");
    let nombreArchivo = formatearNombreArchivo("Probando_Excel");
    generarExcel(data, nombreArchivo);
}

function llenarCombobox(querySelector, arreglo)  // El arreglo debe tener los elementos codigo y descripcion
{
    if (!Array.isArray(arreglo)) 
        return console.log("La función llenarCombobox debe recibir un arreglo");
    
    let select = $(querySelector);
    select.find("option[value!='']").remove().end();
    arreglo.forEach(x => select.append(`<option value="${x.codigo}">${x.descripcion}</option>`))
    select.trigger("chosen:updated");
}

function limpiarFormulario(querySelector)
{
    Array.from($(querySelector).find(":input"))
    .filter(x => x.className.includes("form-control") && x.attributes.getNamedItem("no-limpiar") == null).forEach(x => {
        x.value = "";
    });

    Array.from($(querySelector).find("select"))
    .filter(x => x.attributes.getNamedItem("no-limpiar") == null).forEach(x => {
        $(`${querySelector} [name='${x.name}']`).find("option:first-child").prop("selected", true).trigger("chosen:updated");
    });
}

function showLoading()
{
    $('#loadingOverlay').show();
}

function hideLoading()
{
    $('#loadingOverlay').hide();
}

function cerrarModal(querySelector)
{
    $(querySelector).modal("toggle");   // Cierra el modal
    $(".modal-backdrop").hide();        // Agregado para que el fondo del modal desaparezca
}

function validarNuloVacio(contenido)
{
    if (contenido == null || contenido == "")
        return false;
    else
        return true;
}

function formatearNombreArchivo(nombreArchivo)
{
    let agregarCero = (i) => {
        if (i < 10) { i = "0" + i }
        return i;
    }

    let today = new Date();
    let dia = String(today.getDate()).padStart(2, "0");
    let mes = String(today.getMonth() + 1).padStart(2, "0");
    let anio = today.getFullYear();
    let hora = agregarCero(today.getHours());
    let minuto = agregarCero(today.getMinutes());
    let segundo = agregarCero(today.getSeconds());

    // console.log(`${dia}/${mes}/${anio} ${hora}:${minuto}:${segundo}`);

    return `${nombreArchivo}__${dia}${mes}${anio}_${hora}${minuto}${segundo}`;  // Estandar de nombre archivo usado en Inacap
}

function generarExcel(data, nombreArchivo)
{
    let workbook = XLSX.utils.book_new(),
        worksheet = XLSX.utils.aoa_to_sheet(data);

    workbook.SheetNames.push("Hoja 1");
    workbook.Sheets["Hoja 1"] = worksheet;

    let xlsbin = XLSX.write(workbook, {
        bookType: "xlsx",
        type: "binary"
    });

    let buffer = new ArrayBuffer(xlsbin.length),
        array = new Uint8Array(buffer);

    for (let i = 0; i < xlsbin.length; i++) {
        array[i] = xlsbin.charCodeAt(i) & 0XFF;
    }

    let xlsblob = new Blob([buffer], { type: "application/octet-stream" });
    delete array; delete buffer; delete xlsbin;

    let url = window.URL.createObjectURL(xlsblob),
        anchor = document.createElement("a");
    anchor.href = url;
    anchor.download = `${nombreArchivo}.xlsx`;
    anchor.click();
    window.URL.revokeObjectURL(url);
    delete anchor;
}