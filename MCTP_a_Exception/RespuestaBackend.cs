using System.Collections.Generic;

namespace MCTP_a_Exception
{
    public class RespuestaBackend
    {
        public object objeto { get; set; }
        public List<string> errores { get; set; } = new List<string>();
        public int status { get; set; } = 200;
        public string mensajeExito { get; set; }
        public string informacionExtra { get; set; }
        public bool seCambioPackage { get; set; } = false;

        public void AgregarBadRequest(string mensaje)
        {
            this.errores.Add(mensaje);
            this.status = 400;
        }

        public void AgregarInternalServerError(string mensaje)
        {
            this.errores.Add(mensaje);
            this.status = 500;
        }

        public void NoAutorizado()
        {
            this.errores.Add("No autorizado");   // 401 Unauthorized
            this.status = 401;
        }

        public void AgregarMensajeCambioPackage(string mensaje)  // Prueba 22-11-2022
        {
            this.errores.Add(mensaje);
            this.status = 500;
            this.seCambioPackage = true;
        }

        public void AgregarInternalServerError(int swt, string msg, string sts, string tbl, string pkgp, string p_kya = null)
        {
            // string p_kya;   // Valor de la llave que se pidió leer

            string mensaje = $"<b>Origen:</b> <br/>" +
            $"{pkgp} <br/><br/>";

            if (!string.IsNullOrEmpty(p_kya))
            {
                mensaje += $"<b>Llave:</b> <br/>" +
                $"{p_kya} <br/><br/>";
            }

            if (!string.IsNullOrEmpty(sts))
            {
                mensaje += $"<b>Status:</b> <br/>" +
                $"{sts} <br/><br/>";
            }

            mensaje += $"<b>Tabla:</b> <br/>" +
            $"{tbl} <br/><br/>" +

            $"<b>Mensaje:</b> <br/>" +
            $"{msg}";

            this.errores.Add(mensaje);
            this.status = 500;
        }

        public void AgregarMensajeExito(string mensaje)
        {
            mensajeExito = mensaje;
        }
    }
}

