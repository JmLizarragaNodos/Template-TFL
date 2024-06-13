using System;
using System.Diagnostics;

namespace TFL_x_WEB.Helpers
{
    public static class ExceptionHelper
    {
        public static string ToErrorPersonalizado(this Exception ex)
        {
            string mensajeError = ex.Message;
            string pilaLlamadas = ex.StackTrace;

            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(st.FrameCount - 1);
            int lineaError = frame.GetFileLineNumber();

            if (ex.InnerException != null)  // Si la excepción tiene una excepción interna
                mensajeError += " Inner Exception: " + ex.InnerException.Message;

            string retorno = "";
            retorno += "<b>Error:</b> " + mensajeError + "<br/><br/>";
            retorno += "<b>Origen:</b> " + pilaLlamadas + "<br/><br/>";
            retorno += "<b>Línea:</b> " + lineaError;

            return retorno.Replace("`", "");
        }
    }
}