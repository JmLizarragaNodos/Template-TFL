using Inacap.LogginException;
using MCTP_a_Exception;
using MCTP_c_Modelos_de_Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFL_x_WEB.Helpers;

namespace TFL_x_WEB.PROBANDO
{
    public partial class PROBANDO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

		[WebMethod]
		public static void ObtenerUsuario()
		{
			RespuestaBackend res = new RespuestaBackend();

			try
			{
				var usuario = SesionHelper.GetUsuario();
				res.objeto = usuario;
			}
			catch (Exception ex)
			{
				string msnError = LogException.WriteToEventLog(ex);
				res.AgregarInternalServerError(msnError);
			}

			RetornarJson(res);
		}

		[WebMethod]
		public static void Prueba()
		{
			RespuestaBackend res = new RespuestaBackend();

			try
			{
				var _dataAccess = new VOLVER_ATRAS_Modelo_Datos();
				var _dataAccess_DEF_DIR_SEC_VRA = new DEF_DIR_SEC_VRA_Modelo_Datos();
				var periodosVigenciaTFL = new List<COMBOBOX_ENT>();
				var dir_sec_vra = new List<COMBOBOX_ENT>();

				var dataAccessDefPvigencia = new DEF_PVIGENCIA_TFL_Modelo_Datos();
				RespuestaSP resSP_DefPvigenciaTfl = dataAccessDefPvigencia.GetCombobox(out List<COMBOBOX_ENT> listaPeriodosVigenciaTFL);

				if (resSP_DefPvigenciaTfl.swt == 0)
					periodosVigenciaTFL = listaPeriodosVigenciaTFL;

				RespuestaSP resSP_DefDirSecVra = _dataAccess_DEF_DIR_SEC_VRA.GetCombobox(out List<COMBOBOX_ENT> listaDefDirSecVra);

				if (resSP_DefDirSecVra.swt == 0 || resSP_DefDirSecVra.swt == 1)
					dir_sec_vra = listaDefDirSecVra;

				res.objeto = new { periodosVigenciaTFL, dir_sec_vra };
			}
			catch (Exception ex)
			{
				string msnError = LogException.WriteToEventLog(ex);
				res.AgregarInternalServerError(msnError);
			}

			RetornarJson(res);
		}

		private static void RetornarJson(object res)
		{
			string json = JsonConvert.SerializeObject(res);
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
			HttpContext.Current.Response.Write(json);
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}
	}
}