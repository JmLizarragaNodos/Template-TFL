//importarArchivoJS("../MCTP_Disenio/sesion.js");

console.log("AppVersion, 30-09-2024 v1.0");

$("#appBody").adjustFontSize();   // Incrementar Tamaño de Fuente

$("body").tooltip({ selector: "[data-toggle=tooltip]", trigger: "hover" });  // Habilitar tooltips

// Menú de navegación

const navbar = document.getElementById("offCanvas");
const overlay = document.querySelector(".navbar-overlay")
const navbarItems = document.querySelectorAll(".nav-item")

function openNav() {
    navbar.classList.add("navbar-offcanvas--visible");
    overlay.style.display = "block";
    $(navbarItems).each(function () {
        $(this).css("display", "block");
    });
}

function closeNav() {
    navbar.classList.remove("navbar-offcanvas--visible");
    overlay.style.display = "none";
    $(navbarItems).each(function () {
        $(this).css("display", "none");
    });
}

overlay.addEventListener("click", closeNav);   // Cierra menú reducido al hacer click fuera de el

function mostrarErroresRespuestaBackend(res)
{
    /*
    mostrarErroresRespuestaBackend({ objeto: null, errores: ["No autorizado"], status: 401 });
    mostrarErroresRespuestaBackend({ objeto: null, errores: ["Probando Error"], status: 500 });
    */

    if (res.status == 400)  // Si es Bad Request
    {
        res.errores.forEach(error => toastr.error(error));
    }

    if (res.status == 401)
    {
        res.errores.forEach(error => toastr.error(error));
    }

    if (res.status == 500)  // Si es Internal Server Error
    {
        let modalError = $("#ModalError");

        if (modalError.length && modalError.find("[name='mensaje-error']").length)
        {
            modalError.modal("show");
            modalError.find("[name='mensaje-error']").html("");  // Limpia mensajes anteriores

            let cadena = "";

            res.errores.forEach(error => cadena += `${error} <br/>`);
            modalError.find("[name='mensaje-error']").html(cadena);
        }
        else {
            console.log("No se encontró el modal para desplegar los errores");
            res.errores.forEach(error => toastr.error(error));
        }
    }
}

function encriptarHtml(cadena)
{
    cadena = cadena.replace(/&/g, '/amp/');  // Reeemplazar el &
    cadena = cadena.replace(/</g, '/lt/');
    cadena = cadena.replace(/>/g, '/gt/');

    // cadena = cadena.replace(/&/g, '|amp;');  // Reeemplazar el &
    // cadena = cadena.replace(/</g, '|lt;');
    // cadena = cadena.replace(/>/g, '|gt;');
    return cadena;
}

function findVersionFromSelect(querySelector)  // findVersionFromSelect("#buscar [name='tfl']");
{
    let texto = $(`${querySelector} option:selected`).text();   // $("#buscar [name='tfl'] option:selected").text();

    let match = texto.match(/Versión:\s*(\d+)/); 
    let version = match ? match[1] : null;

    if (version == null || version == "" || isNaN(version))
        return console.log("No se pudo obtener la versión de la TFL");

    let retorno = parseInt(version);
    return retorno;
}

//===================================================================================================>>>>>



//===================================================================================================>>>>>
// Prueba para usar Token de Autenticación

/*
var token = localStorage.getItem("token_tfl");

$(document).ajaxSend(async function (event, jqxhr, settings)  // Interceptor
{
    if (token) {
        jqxhr.setRequestHeader('X-Bearer-Token', token);  // Agrega el bearer token
    }
    else {
        console.log("No está definido el token");
    }

    //console.log('Interceptando AJAX Send');
    //console.log('URL:', settings.url);
    //console.log('Datos:', settings.data);
});

if (!token)
{
    $.ajax({
        method: "POST",
        url: "../Login.asmx/Auth",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: (res) =>
        {
            if (res.status == 200)
            {
                localStorage.setItem("token_tfl", res.objeto.token);
                // localStorage.removeItem("token_tfl");

                console.log(`token_tfl = ${localStorage.getItem("token_tfl")}`);
                setTimeout(() => window.location.reload(), 200);
            }
        },
        error: (XMLHttpRequest, textStatus, errorThrown) => console.log("Ocurrió un error al llamar a Auth")
    });
}
*/
//===================================================================================================>>>>>
/*
let cantidadMinutos = 15;
let segundosIntervalo = cantidadMinutos * 60;

setTimeout(() => {
    console.log(`Esta página renovará la sesión en caso de inactividad después de ${cantidadMinutos} minutos`);
}, 300);

let contador = 0;
let maximoVeces = 3;  // Es la cantidad máxima de veces que se realizará la cuenta

function renovarSesion()
{
    if (contador < maximoVeces)
    {
        console.log(`¡Han pasado ${segundosIntervalo} segundos! y es la vez número ${contador}`);

        $.ajax({
            method: "POST",
            url: "../Login.asmx/RenovarSesion",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) => 
            {
                if (res.status == 200)
                    console.log("Se ha renovado la sesión", res);
                else
                    console.log("Se encontró un error al renovar la sesión", res);
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => console.log("Ocurrió un error al llamar a RenovarSesion")
        });

        contador++;
    }
}

// Establecer el intervalo en milisegundos (10 segundos = 10 * 1000 milisegundos)
let milisegundos = segundosIntervalo * 1000;  // intervalo

// Configurar el temporizador para que llame a la función cada 10 segundos
let temporizador = setInterval(renovarSesion, milisegundos);
*/
//===================================================================================================>>>>>

function importarArchivoJS(src)
{
    if (src == null || src == "") return;

    let script = document.createElement("script");
    script.src = src;
    document.head.appendChild(script);
}

















