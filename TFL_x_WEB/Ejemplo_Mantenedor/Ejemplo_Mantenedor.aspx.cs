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

namespace TFL_x_WEB.Ejemplo_Mantenedor
{
    public partial class Ejemplo_Mantenedor : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

		[WebMethod]
		public static void GetCombobox()
		{
			RespuestaBackend res = new RespuestaBackend();

			try
			{
				//throw new Exception("Probando error");

				var arreglo = new List<object>();

				for (int x = 1; x < 20; x++)
				{
					arreglo.Add(new 
					{
						codigo = x,
						descripcion = $"Probando {x}",
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
		public static void PRUEBA_SEL(int? codigo, string estado, string fechaEfectiva, int entradas)
		{
			RespuestaBackend res = new RespuestaBackend();

			try
			{
				//throw new Exception("Probando error");

				var arreglo = new List<object>();

				for (int x = 1; x < entradas; x++)
				{
					arreglo.Add(new
					{
						codigo = x,
						nombre = $"Prueba {x}",
						descripcion = $"Prueba {x}",
						estado = (x % 2 == 0) ? "A" : "I",
						fechaEfectiva = "25-7-2022"
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

		private static bool Validar(string nombre, string descripcion, out List<string> listaErrores)
		{
			List<OutValidator> outValidators = new List<OutValidator>();

			outValidators.Add(validator.validaExpr(validator.typeValidator.valTyvalTypeDescriptions, nombre, validator.typeRequired.valTypeUnRequired, 500, 0, "nombre"));
			outValidators.Add(validator.validaExpr(validator.typeValidator.valTyvalTypeDescriptions, descripcion, validator.typeRequired.valTypeUnRequired, 500, 0, "descripción"));

			listaErrores = outValidators.FindAll(x => !x.OutMethod).Select(p => p.OutMessage).ToList();

			return !listaErrores.Any();
		}

		[WebMethod]
		public static void PRUEBA_GRA(string nombre, string descripcion)
		{
			RespuestaBackend res = new RespuestaBackend();

			if (!Validar(nombre, descripcion, out List<string> listaErrores))
			{
				foreach (string error in listaErrores)
				{
					res.AgregarBadRequest(error);
				}
			}
			else
			{
				// Llama al procedimiento
				res.AgregarMensajeExito("Prueba Éxito");
			}

			RetornarJson(res);
		}

		[WebMethod]
		public static void PRUEBA_UPD(int ncorr, string nombre, string descripcion)
		{
			RespuestaBackend res = new RespuestaBackend();

			if (!Validar(nombre, descripcion, out List<string> listaErrores))
			{
				foreach (string error in listaErrores)
				{
					res.AgregarBadRequest(error);
				}
			}
			else
			{
				// Llama al procedimiento
				res.AgregarMensajeExito("Prueba Éxito");
			}

			RetornarJson(res);
		}

		[WebMethod]
		public static void PRUEBA_DEL(int kya)
		{
			RespuestaBackend res = new RespuestaBackend();

			// Llama al procedimiento
			res.AgregarMensajeExito("Prueba Éxito");

			RetornarJson(res);
		}

		[WebMethod]
		public static void PRUEBA_LEE(int ncorr)
		{
			RespuestaBackend res = new RespuestaBackend();

			try
			{
				//throw new Exception("Prueba error");

				res.objeto = new
				{
					codigo = ncorr,
					nombre = $"Prueba {ncorr}",
					descripcion = $"Prueba {ncorr}",
					estado = (ncorr % 2 == 0) ? "A" : "I",
					fechaEfectiva = "25-7-2022"
				};
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