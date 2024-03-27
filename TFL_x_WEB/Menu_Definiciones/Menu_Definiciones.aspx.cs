using Inacap.LogginException;
using MCTP_a_Exception;
using MCTP_c_Modelos_de_Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using TFL_x_WEB.Dto;
using TFL_x_WEB.Helpers;

namespace TFL_x_WEB.Menu_Definiciones
{
    public partial class Menu_Definiciones : Page
    {
        protected static USUARIO_ENT usuario { get; set; }
        protected static string cadenaMenu { get; set; }
        protected static string cadenaSubMenu { get; set; }
        protected string errorCarga { get; set; }

        public Menu_Definiciones()
        {
            //==============================================>>>>>
            /*
            var objeto = new EjemploAuth
            {
                nombre = "Jose",
                rutNumero = 12345678,
                sesi_ccod = "1111",
                pers_ncorr = 2222,
                permisosAuth = new PermisosAuth { SELECT = true, UPDATE = true }
            };

            var jsonString = JsonConvert.SerializeObject(objeto);

            // Encriptar
            var jsonStringEncriptado = CryptoHelper.Encriptar(jsonString);

            // Validar si está encriptado
            bool estaEncriptado = CryptoHelper.TryDesencriptar(jsonStringEncriptado, out var jsonStringDesencriptado);

            if (estaEncriptado)
            {
                // Convertir de nuevo a objeto
                var objetoObtenido = JsonConvert.DeserializeObject<EjemploAuth>(jsonStringDesencriptado);

                // Hacer algo con el objeto obtenido
                Console.WriteLine(objetoObtenido.nombre);
            }
            else
            {
                Console.WriteLine("jsonStringEncriptado no tiene el formato correcto");
            }
            */
            //==============================================>>>>>

            usuario = SesionHelper.GetUsuario();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string BASE_URL = Request.Url.GetLeftPart(UriPartial.Authority);

            try
            {
                List<MenuAcceso> listaPrincipal = SesionHelper.ObtenerInfoMenuAcceso(
                    "ETAPA1",
                    "Menú de Definiciones",
                    BASE_URL,
                    out RespuestaSP resError
                );

                if (resError != null)
                {
                    errorCarga = LogException.LogException_pkg(resError.swt, resError.msg, resError.sts, resError.tbl, resError.pkgp);
                    return;
                }

                cadenaMenu = string.Join("", listaPrincipal.Select(x => {
                    return $"<li class='nav-item'> " +
                        $"<a href='{x.url}' class='nav-link waves-effect {(x.esPaginaActual ? "active" : "")}'> {x.nombre} </a> " +
                    $"</li>";
                }));

                string validarAutorizacion = ConfigurationManager.AppSettings["validarAutorizacion"];
                bool validar = (!string.IsNullOrEmpty(validarAutorizacion));

                List<SubMenu> listaSubMenu = listaPrincipal.FirstOrDefault(x => x.esPaginaActual).listaSubMenu;

                //======================>>>>>
                // Agregar Otras Etapas
                /*
                listaSubMenu.Add(new SubMenu { nombre = "Etapa 2" });
                listaSubMenu.Add(new SubMenu { nombre = "Etapa 3" });
                listaSubMenu.Add(new SubMenu { nombre = "Etapa 4" });
                listaSubMenu.Add(new SubMenu { nombre = "Etapa 5" });
                */
                //======================>>>>>

                cadenaSubMenu = string.Join("", listaSubMenu
                    .Select(x =>
                    {
                        return @"
                        <div class='col mb-4'>
                            <div class='card card-home'>
                                <div class='card-header'>
                                    <h3 class='h3-responsive'>" + x.nombre + @"</h3>
                                </div>
                                <div class='card-body'>
                                    <ul class='list-card' class='mb-4'>
                                        " +
                                        string.Join("", x.listaItemSubMenu.Select(y =>
                                        {
                                            bool autorizado = (validar) ? y.autorizado : true;

                                            return $"<li> " +
                                                $"<a href='{(autorizado ? y.url : "#")}' " +

                                                $"{(y.esOtraAplicacion ? $"onclick=\"tflProcesarAplicacion(event, 'TFL_{x.codigoSistema}')\" " : "")}" +
                                                //$"{(y.esOtraAplicacion ? "target='_blank' " : "")}" +

                                                $"style='{(!autorizado ? "color: #a09f9f; cursor: auto" : "")}'> " +
                                                $"{y.nombre} > </a> " +
                                            $"</li>";
                                        }))
                                        + @"
                                    </ul>
                                </div>
                            </div>
                        </div>
                        ";
                    })
                );
            }
            catch (Exception ex)
            {
                errorCarga = LogException.WriteToEventLog(ex);
            }


            /*
            string descripcion;

            // descripcion = "<script>window.location.reload()</script>";  // No pasa la prueba 
            // descripcion = "'; exec sp_MSforeachtable 'DROP TABLE ?';--";     // No pasa la prueba 
            descripcion = "Existe Poblamiento en el Sector (nulo=0; inicial=1; avanzado=2)";  // Si pasa la prueba

            List<OutValidator> outValidators = new List<OutValidator>();

            outValidators.Add(validator.validaExpr(
                validator.typeValidator.valTyvalTypeDescriptions, 
                descripcion, 
                validator.typeRequired.valTypeRequired, 
                100, 
                1, 
                "descripción"
            ));

            List<string> errores = outValidators.FindAll(x => x.OutMethod == false).Select(p => p.OutMessage).ToList();

            foreach (string error in errores)
            {
                Console.WriteLine(error);  // El valor ingresado para el control descripción, no posee el formato requerido.
            }
            */
        }

        [WebMethod]
        public static void TFL_PROCESAR_APLICACION(string apli_caplicacion)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                var dataAccess = new TFL_MODULOS_ACCESOS_Modelo_Datos();
                string p_pers_nrut = usuario.rutNumero.ToString();
                string sesi_ccod = usuario.sesi_ccod;

                RespuestaSP resSP = dataAccess.TFL_PROCESAR_APLICACION(
                    apli_caplicacion,   // p_cacplicacion
                    p_pers_nrut,        // p_pers_nrut
                    sesi_ccod,          // p_sesi_ccod
                    out string urlout,
                    out string err_msg
                );

                object paramsEnviados = new
                {
                    sp = "TFL_MODULOS_ACCESOS_PKG.TFL_PROCESAR_APLICACION",
                    p_cacplicacion = apli_caplicacion,
                    p_pers_nrut,
                    p_sesi_ccod = sesi_ccod
                };

                res.objeto = new { paramsEnviados };

                if (resSP.swt == 0)
                {
                    res.objeto = new
                    {
                        urlout,
                        err_msg,
                        paramsEnviados
                    };
                }
                //else if (resSP.swt == 1)
                //{
                //    res.AgregarBadRequest(err_msg);
                //}
                else
                {
                    string msnError = LogException.LogException_pkg(resSP.swt, resSP.msg, resSP.sts, resSP.tbl, resSP.pkgp);
                    res.AgregarInternalServerError(msnError);
                }
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