
this.validarSesionExpirada = true;
var datosTFL = {};

this.global = {};
this.global.fechaTerminoSesion = null;

this.tareaCuentaRegresiva = null;
this.verificarSesionActiva = null;
this.tareaAvisarCierreSesion = null;
this.tareaCerrarSesion = null;

this.estaAbiertoModalAvisoCierreSesion = false;

(async () =>
{
    colocarModalAvisoCierreSesion();
    // localStorage.removeItem("datosTFL");

    if (localStorage.getItem("datosTFL") !== null)
    {
        console.log("El localStorage contiene datosTFL");
        let json = localStorage.getItem("datosTFL");
        datosTFL = JSON.parse(json);

        //================>>>

        let fechaExpiracion = (datosTFL.fechaTerminoSesion != null && datosTFL.fechaTerminoSesion != "") ?
            getDateFromString(datosTFL.fechaTerminoSesion) : null;

        if (fechaExpiracion == null || new Date() > fechaExpiracion)  // Si la fecha guardada anteriormente está vencida o no existe
        {
            datosTFL = await TRAER_FTERMINO_SESION();
            localStorage.setItem("datosTFL", JSON.stringify(datosTFL));
            console.log(`La fechaExpiracion guardada anteriormente estaba vencida. La nueva fechaExpiracion es: ${datosTFL.fechaTerminoSesion}`);
        }

        //================>>>
    }
    else
    {
        console.log("El localStorage no contiene datosTFL");

        datosTFL = await TRAER_FTERMINO_SESION();
        localStorage.setItem("datosTFL", JSON.stringify(datosTFL));
    }

    if (datosTFL.fechaTerminoSesion == "")
        return finalizarSesion();

    console.log("datosTFL", datosTFL);
    this.global.fechaTerminoSesion = datosTFL.fechaTerminoSesion;

    //========================>>>>

    ngAfterViewInit();

    //========================>>>>
})();

function ngAfterViewInit()
{
    //this.global.fechaTerminoSesion = "07-02-2024 09:13:20";  // SOLO DE PRUEBA

    if (this.validarSesionExpirada) 
    {
      definirTareasSesion();

      this.verificarSesionActiva = programarTareaRepetitiva({ 
        //segundosIntervalo: 5,
        segundosIntervalo: 60,  // 1 minuto
        tarea: async () => 
        {
          let res = await TRAER_FTERMINO_SESION();

          if (res.fechaTerminoSesion != "") 
          {
            if (res.fechaTerminoSesion != this.global.fechaTerminoSesion) 
            {
              console.log("El usuario abrio sesión en una nueva pestaña");
              this.global.fechaTerminoSesion = res.fechaTerminoSesion;
              console.log(`this.global.fechaTerminoSesion = "${this.global.fechaTerminoSesion}"`);
              definirTareasSesion();
              cerrarModalAvisoCierreSesion();
            }
            else {
              console.log("La sesión sigue como estaba al inicio");
            }
          }
          else
          {
              localStorage.removeItem("datosTFL");
              //console.log("ACA HAY QUE FINALIZAR SESIÓN PORQUE SE CERRÓ LA SESIÓN EN OTRA PESTAÑA");
              finalizarSesion();
          }
        }
      });
    }
}

function definirTareasSesion()
{
    if (this.tareaAvisarCierreSesion != null)
        this.tareaAvisarCierreSesion.detener();

    if (this.tareaCerrarSesion != null)
        this.tareaCerrarSesion.detener();

    let fechaExpiracion = getDateFromString(this.global.fechaTerminoSesion);
    let fechaHoy = new Date();

    if (fechaHoy > fechaExpiracion)
        return finalizarSesion();  // Finaliza la sesión y no ejecuta tareas

    let tiempoRestante = fechaExpiracion - fechaHoy;  // Calcula el tiempo restante en milisegundos

    //let tiempoAviso = tiempoRestante - 10000;  // Establece el tiempo para el aviso 10 segundos antes
    let tiempoAviso = tiempoRestante - 5 * 60 * 1000; // Establece el tiempo para el aviso 5 minutos antes (en milisegundos)

    this.tareaAvisarCierreSesion = programarTarea({
        milisegundos: tiempoAviso,
        tarea: async () =>
        {
            abrirModalAvisoCierreSesion();
        }
    });

    this.tareaCerrarSesion = programarTarea({
        milisegundos: tiempoRestante,
        tarea: () => {
            console.log("Se terminó la sesión");
            finalizarSesion();
        }
    });
}

function finalizarSesion()
{
    //================>>>
    // Finaliza tareas para que no se ejecuten

    // this.verificarSesionActiva?.unsubscribe();
    // this.tareaAvisarCierreSesion?.unsubscribe();
    // this.tareaCuentaRegresiva?.unsubscribe();

    if (this.verificarSesionActiva != null && this.verificarSesionActiva === "function")
        this.verificarSesionActiva.detener();

    if (this.tareaAvisarCierreSesion != null && this.tareaAvisarCierreSesion === "function")
        this.tareaAvisarCierreSesion.detener();

    if (this.tareaCuentaRegresiva != null && this.tareaCuentaRegresiva === "function")
        this.tareaCuentaRegresiva.detener();

    //================>>>

    $("#modalAvisoCierreSesion").modal("hide");

    //==========================================>>>
    //this.router.navigate(['/sesionTerminada'], { queryParams: { refrescarPagina: 'true' } });

    //window.location.href = "../sesion-terminada/sesion-terminada.aspx";

    let form = document.createElement("form");
    form.method = "POST";
    form.action = "../sesion-terminada/sesion-terminada.aspx"; // URL a la que deseas redirigir
    document.body.appendChild(form);
    form.submit();

    //==========================================>>>
}

function cerrarModalAvisoCierreSesion()
{
    //=====================>>>
    // Finaliza tareas para que no se ejecuten
    //this.tareaCuentaRegresiva?.unsubscribe();

    if (this.tareaCuentaRegresiva != null && this.tareaCuentaRegresiva === "function")
        this.tareaCuentaRegresiva.detener();

    //=====================>>>

    console.log("cerrarModalAvisoCierreSesion");
    $("#modalAvisoCierreSesion").modal("hide");
    this.estaAbiertoModalAvisoCierreSesion = false;
}

function abrirModalAvisoCierreSesion() 
{
    let modal = $("#modalAvisoCierreSesion");

    this.tareaCuentaRegresiva = programarTareaRepetitiva({
      segundosIntervalo: 1,
      tarea: () => 
      {
        let fecha_hora_actual = new Date();
        let fechaTermino = getDateFromString(this.global.fechaTerminoSesion);
        let tiempoRestante = fechaTermino - fecha_hora_actual;
        modal.find("[name='tiempoRestante']").text(`Tiempo restante: ${formatTiempo(tiempoRestante)}`);
      }
    });

    modal.find("[name='mensaje']").text("La sesión pronto expirará ¿Desea continuar en la página?");
    modal.modal({ backdrop: 'static' });
    modal.modal("show");
    this.estaAbiertoModalAvisoCierreSesion = true;
}

function colocarModalAvisoCierreSesion()
{
    if (document.querySelector("#modalAvisoCierreSesion") == null)
    {
        let cadena = `<div class="modal fade" id="modalAvisoCierreSesion" tabindex="-1" role="dialog"  aria-hidden="true">
          <div class="modal-dialog modal-notify modal-success modal-sm" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <p class="heading lead">Alerta</p>
                  <button type="button" onclick="cerrarModalAvisoCierreSesion()" class="close" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="modal-body">
                  <div class="text-center">
                    <p name="mensaje"></p>
                    <p name="tiempoRestante"></p>
                  </div>
                </div>
                <div class="modal-footer d-flex justify-content-center">
                  <button onclick="finalizarSesion()" class="btn btn-secondary waves-effect waves-light" data-dismiss="modal">No</button>
                  <button onclick="renovarSesion()" class="btn btn-default waves-effect waves-light" data-dismiss="modal">Sí</button>
                </div>
              </div>
            </div>
        </div>`;

        let contenedor = document.body;
        contenedor.insertAdjacentHTML('beforeend', cadena);  // Pone el modal en el html, ya que no existe.
    }
}

async function renovarSesion()
{
    try {
        let res = await ACTUALIZA_SESION();

        //================>>>>
        // Finaliza tareas para que no se ejecuten
        //this.tareaCuentaRegresiva?.unsubscribe();

        if (this.tareaCuentaRegresiva != null && this.tareaCuentaRegresiva === "function")
            this.tareaCuentaRegresiva.detener();

        //================>>>>

        this.global.fechaTerminoSesion = res.fechaTerminoSesion;
        console.log("this.global.fechaTerminoSesion", this.global.fechaTerminoSesion);

        console.log("La sesión ha sido renovada");
        $("#modalAvisoCierreSesion").modal("hide");
        this.estaAbiertoModalAvisoCierreSesion = false;

        definirTareasSesion();

        //================>>>
        localStorage.setItem("datosTFL", JSON.stringify(res));
        //================>>>
    }
    catch (ex) {
        if (ex.errores != null) mostrarErroresRespuestaBackend(ex);
        else toastr.error(ex);
    }
}

function TRAER_FTERMINO_SESION()
{
    return new Promise((resolve, reject) => {
        $.ajax({
            method: "POST",
            url: "../Login.asmx/TRAER_FTERMINO_SESION",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) =>
            {
                if (res.status == 200)
                    resolve(res.objeto);
                else
                    reject(res);
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => reject("Ocurrió un error al obtener la fecha de término de sesión")
        });
    });
}

function ACTUALIZA_SESION()
{
    return new Promise((resolve, reject) => {
        $.ajax({
            method: "POST",
            url: "../Login.asmx/ACTUALIZA_SESION",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) =>
            {
                if (res.status == 200)
                    resolve(res.objeto);
                else
                    reject(res);
            },
            error: (XMLHttpRequest, textStatus, errorThrown) => reject("Ocurrió un error al obtener actualizar la sesión")
        });
    });
}

function programarTareaRepetitiva(datos)
{
    let milisegundos = datos.segundosIntervalo * 1000;

    let tarea = setInterval(() => {
        if (datos.tarea != null && typeof datos.tarea === "function") datos.tarea();
    }, milisegundos);

    return {
        detener: function () {
            clearInterval(tarea);
        }
    };
}

function programarTarea(datos)
{
    const tarea = setInterval(() => {
        if (datos.tarea != null && typeof datos.tarea === "function") {
            datos.tarea();
            clearInterval(tarea); // Detener la tarea después de ejecutarse una vez
        }
    }, datos.milisegundos);

    //return tarea;
	return {
        detener: function () {
            clearInterval(tarea);
        }
    };
}

function getDateFromString(cadena)
{
    if (cadena == null) return null;

    // Parsea la cadena de fecha manualmente en el formato adecuado
    let partesFecha = cadena.split(' ');
    let partesFechaNumerica = partesFecha[0].split('-');
    let partesHoraNumerica = partesFecha[1].split(':');

    let ano = parseInt(partesFechaNumerica[2], 10);
    let mes = parseInt(partesFechaNumerica[1], 10) - 1; // Resta 1 porque los meses en JavaScript son 0-indexados
    let dia = parseInt(partesFechaNumerica[0], 10);
    let hora = parseInt(partesHoraNumerica[0], 10);
    let minuto = parseInt(partesHoraNumerica[1], 10);
    let segundo = parseInt(partesHoraNumerica[2], 10);

    // Crea el objeto Date
    let fechaTerminoSesion = new Date(ano, mes, dia, hora, minuto, segundo);
    return fechaTerminoSesion;
}

function formatTiempo(tiempo)
{
    // Verifica si el tiempo es un objeto Date
    //if (!(tiempo instanceof Date)) {
    //    // Si no es un objeto Date, devuelve un mensaje de error
    //    return "Error: tiempo no es un objeto Date";
    //}

    // Calcula las horas, minutos y segundos restantes
    let horas = Math.floor(tiempo / (1000 * 60 * 60));
    let minutos = Math.floor((tiempo % (1000 * 60 * 60)) / (1000 * 60));
    let segundos = Math.floor((tiempo % (1000 * 60)) / 1000);

    // Agrega ceros a la izquierda si es necesario
    horas = horas < 10 ? "0" + horas : horas;
    minutos = minutos < 10 ? "0" + minutos : minutos;
    segundos = segundos < 10 ? "0" + segundos : segundos;

    return `${horas}:${minutos}:${segundos}`;
}
