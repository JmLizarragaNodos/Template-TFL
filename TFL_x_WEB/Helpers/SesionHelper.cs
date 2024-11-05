using MCTP_c_Modelos_de_Datos.Entity;

namespace TFL_x_WEB.Helpers
{
    public static class SesionHelper
    {
        public static USUARIO_ENT GetUsuario()
        {
            return new USUARIO_ENT()
            {
                nombre = "José Miguel (CAMBIAR ESTO)",
                pers_ncorr = 3270864,
                rutNumero = 17083122
            };
        }
    }
}