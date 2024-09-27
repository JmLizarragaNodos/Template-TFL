using MCTP_a_Exception;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using TFL_x_WEB.Auth;
using TFL_x_WEB.Helpers;

namespace TFL_x_WEB
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            bool esAjax = false;

            try
            {
                string validarAutorizacion = ConfigurationManager.AppSettings["validarAutorizacion"];

                if (!string.IsNullOrEmpty(validarAutorizacion))
                {
                    string nombreMetodo = Request.Path.Remove(0, Request.Path.LastIndexOf("/") + 1);

                    string nombreClase = Path.GetFileName(Request.PhysicalPath)?
                        .Replace(".aspx", "")
                        .Replace(".asmx", "")
                        .Replace("_Service", "");

                    bool autorizado = false;

                    if (IsAjaxRequest(HttpContext.Current.Request))
                    {
                        esAjax = true;

                        if (nombreClase == "Login")  // Permitir renovar sesión
                            return;

                        string tipo = ObtenerUltimosCaracteres(nombreMetodo.ToUpper(), 4);

                        if (nombreMetodo == "TFL_PROCESAR_APLICACION")   // Si es para saltar de una aplicación a otra
                        {
                            autorizado = true;
                        }
                        else if (
                            nombreMetodo.Contains("_GRABAR") || nombreMetodo.Contains("_EDITAR") || nombreMetodo.Contains("_COPIAR") ||
                            tipo == "_GRA" || tipo == "_UPD" || tipo == "_DEL"
                        )
                        {
                            autorizado = SesionHelper.EstaAutorizado(nombreClase, Permisos.UPDATE);
                        }
                        else if (nombreMetodo.Contains("_BUSCAR") || tipo == "_SEL" || tipo == "_LEE")
                        {
                            autorizado = SesionHelper.EstaAutorizado(nombreClase, Permisos.SELECT, Permisos.UPDATE);
                        }
                        else
                        {
                            autorizado = SesionHelper.EstaAutorizado(nombreClase, Permisos.SELECT, Permisos.UPDATE);
                        }

                        if (!autorizado)
                        {
                            RespuestaBackend resError = new RespuestaBackend();
                            resError.NoAutorizado();

                            string json = JsonConvert.SerializeObject(resError);
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                            HttpContext.Current.Response.Write(json);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.End();
                        }
                    }
                    else  // Si no es una llamada por Ajax
                    {
                        if (
                            nombreClase != "ADMINISTRACION_TFL" &&
                            nombreClase != "Menu_Definiciones" && 
                            nombreClase != "Menu_Operacional"
                        )
                        {
                            if (!SesionHelper.EstaAutorizado(nombreClase, Permisos.SELECT, Permisos.UPDATE))
                            {
                                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                                    Request.ApplicationPath.TrimEnd('/');

                                Response.Redirect($"{baseUrl}/ADMINISTRACION_TFL/ADMINISTRACION_TFL.aspx");

                                // Response.Redirect("https://localhost:44300/Menu_Definiciones/Menu_Definiciones.aspx");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorCarga = ExceptionHelper.ToErrorPersonalizado(ex);

                RespuestaBackend res = new RespuestaBackend();
                res.AgregarInternalServerError(errorCarga);

                string json = JsonConvert.SerializeObject(res);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                HttpContext.Current.Response.Write(json);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        private bool IsAjaxRequest(HttpRequest request)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest" || (request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }

        public string ObtenerUltimosCaracteres(string cadena, int cantidadCaracteres)
        {
            if (cantidadCaracteres >= cadena.Length)
                return cadena;

            return cadena.Substring(cadena.Length - cantidadCaracteres);
        }

    }
}