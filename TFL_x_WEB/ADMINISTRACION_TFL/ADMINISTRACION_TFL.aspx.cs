using MCTP_c_Modelos_de_Datos.Entity;
using System;
using System.Web.UI;
using TFL_x_WEB.Helpers;

namespace TFL_x_WEB.ADMINISTRACION_TFL
{
    public partial class ADMINISTRACION_TFL : Page
    {
        protected static USUARIO_ENT usuario { get; set; } = new USUARIO_ENT();

        public ADMINISTRACION_TFL()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = SesionHelper.GetUsuario();

            // No leer cache del navegador web
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");
        }
    }
}