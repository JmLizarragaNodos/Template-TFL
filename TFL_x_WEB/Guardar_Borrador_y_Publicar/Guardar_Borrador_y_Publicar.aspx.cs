using Inacap.LogginException;
using Inacap.Validators;
using MCTP_a_Exception;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFL_x_WEB.Helpers;

namespace TFL_x_WEB.Guardar_Borrador_y_Publicar
{
    public partial class Guardar_Borrador_y_Publicar : Page
    {
        protected static InfoUsuario infoUsuario = InfoUsuarioHelper.ObtenerInfoUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

		public class Probando
		{
            public int ncorr { get; set; }
            public int prelacion { get; set; }
            public string nombre { get; set; }
			public string descripcion { get; set; }
			public string fecha { get; set; }
			public string estado { get; set; }
		}

		[WebMethod]
		public static void ObtenerDatosGrilla()
		{
			RespuestaBackend res = new RespuestaBackend();

			try
			{
				//throw new Exception("Probando error");

				var arreglo = new List<Probando>();

				for (int x = 1; x < 20; x++)
				{
					arreglo.Add(new Probando
					{
						ncorr = x,
						prelacion = x,
						nombre = $"Probando {x}",
						descripcion = $"Probando descripción {x}",
						fecha = "07-11-2022",
						estado = (x % 2 == 0) ? "Activo" : "Inactivo"
					});
				}

				res.objeto = arreglo;
			}
			catch (Exception ex)
			{
				string msnError = LogException.WriteToEventLog(ex);
				res.AgregarInternalServerError(msnError);
			}

			RetornarJson(res);
		}

		[WebMethod]
		public static void PRUEBA_GRABAR(string p_json_datos, bool esBorrador)
		{
			RespuestaBackend res = new RespuestaBackend();

			try
			{
				res.AgregarMensajeExito("Prueba Éxito");
			}
			catch (Exception ex)
			{
				string msnError = LogException.WriteToEventLog(ex);
				res.AgregarInternalServerError(msnError);
			}

			RetornarJson(res);
		}

		[WebMethod]
		public static void ValidarDetalle(string nombre, string descripcion)
		{
			RespuestaBackend res = new RespuestaBackend();
			List<OutValidator> outValidators = new List<OutValidator>();

			outValidators.Add(validator.validaExpr(validator.typeValidator.valTyvalTypeDescriptions, nombre, validator.typeRequired.valTypeUnRequired, 500, 0, "nombre"));
			outValidators.Add(validator.validaExpr(validator.typeValidator.valTyvalTypeDescriptions, descripcion, validator.typeRequired.valTypeUnRequired, 500, 0, "descripción"));

			List<string> listaErrores = outValidators.FindAll(x => !x.OutMethod).Select(p => p.OutMessage).ToList();

			if (listaErrores.Any())
			{
				foreach (string error in listaErrores)
				{
					res.AgregarBadRequest(error);
				}
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