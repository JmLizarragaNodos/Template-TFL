using Inacap.Security.Access;
using MCTP_c_Modelos_de_Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TFL_x_WEB.Auth;
using TFL_x_WEB.Dto;

namespace TFL_x_WEB.Helpers
{
    public static class SesionHelper
    {

        public static USUARIO_ENT GetUsuario()
        {
            try
            {
                userData objUserData = new userData();

                string nombre = UppercaseWords(objUserData.dataUser.user_tnombres.ToLower());
                string apepat = UppercaseWords(objUserData.dataUser.user_tape_paterno.ToLower());
                string apemat = UppercaseWords(objUserData.dataUser.user_tape_materno.ToLower());

                string rutNumero_String = objUserData.dataUser.user_nrut;
                int v_pers_ncorr = Convert.ToInt32(objUserData.dataUser.pers_ncorr);
                string sesi_ccod = objUserData.dataUser.sesi_ccod;

                USUARIO_ENT usuario = new USUARIO_ENT()
                {
                    nombre = $"{nombre} {apepat} {apemat}",
                    pers_ncorr = v_pers_ncorr,
                    rutNumero = Int32.Parse(rutNumero_String),
                    sesi_ccod = sesi_ccod
                };

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //public static USUARIO_ENT GetUsuario()
        //{
        //    return new USUARIO_ENT()
        //    {
        //        nombre = "José Miguel (CAMBIAR ESTO)",
        //        pers_ncorr = 3270864,
        //        rutNumero = 17083122
        //        //sesi_ccod = "168204134L262AAAI5ZAAAAABFTpAAAJ25232522023"
        //    };
        //}

        //public static USUARIO_ENT GetUsuario()
        //{
        //    return new USUARIO_ENT()
        //    {
        //        nombre = "Pamela Lara",
        //        pers_ncorr = 1870996,
        //        rutNumero = 16420925
        //    };
        //}

        public static List<MenuAcceso> ObtenerInfoMenuAcceso(
            string ETAPA,           // "ETAPA1"
            string nombreMenu,
            string BASE_URL,
            out RespuestaSP resError
        )
        {
            resError = null;

            //resError = new RespuestaSP { swt = 2, msg = "Probando Error del SP", tbl = "Ejemplo Tabla", pkgp = "Ejemplo PKG" };
            //return null;

            int rut = GetUsuario().rutNumero;

            //===============================>>>>>
            /*
            RespuestaSP resSP = new RespuestaSP();
            resSP.swt = 0;
            List <TFL_MODULOS_ACCESOS_ENT> datosDB = ObtenerListaProbando();
            */
            //===============================>>>>>>
            
            TFL_MODULOS_ACCESOS_Modelo_Datos dataAccess = new TFL_MODULOS_ACCESOS_Modelo_Datos();

            RespuestaSP resSP = dataAccess.TFL_MODULOS_ACCESOS(
                "TFL",          // p_sist_csistema
                ETAPA,          // p_modulos_sist_ccod      "ETAPA1"
                rut,            // p_rut_usuario
                null,           // p_cacplicacion
                out List<TFL_MODULOS_ACCESOS_ENT> datosDB
            );
            
            //===============================>>>>>>

            if (resSP.swt == 0 || resSP.swt == 1)
            {
                List<MenuAcceso> listaMenu = datosDB.OrderBy(x => x.menu_modulos_prelacion).DistinctBy(p => p.menu_modulos_nombre)
                .Select(x => new MenuAcceso()
                {
                    nombre = x.menu_modulos_nombre,
                    esPaginaActual = (x.menu_modulos_nombre == nombreMenu),
                    url = x.menu_modulos_url,
                    listaSubMenu = new List<SubMenu>()
                }).ToList();

                foreach (MenuAcceso menu in listaMenu)
                {
                    List<SubMenu> listaSubMenu = datosDB.Where(x => x.menu_modulos_nombre == menu.nombre)
                    .OrderBy(x => x.smenu_modulos_prelacion)
                    .DistinctBy(p => p.smenu_modulos_nombre)
                    .Select(x => new SubMenu()
                    {
                        nombre = x.smenu_modulos_nombre,
                        codigoSistema = x.modulos_sist_ccod,
                        listaItemSubMenu = new List<ItemSubMenu>()
                    }).ToList();

                    foreach (SubMenu subMenu in listaSubMenu)
                    {
                        List<ItemSubMenu> listaItemSubMenu = datosDB.Where(x => x.menu_modulos_nombre == menu.nombre && x.smenu_modulos_nombre == subMenu.nombre)
                        .OrderBy(x => x.app_prelacion)
                        .Select(x => 
                        {
                            bool esOtraAplicacion = false;
                            string url = x.apli_tubicacion;
                            
                            if (subMenu.codigoSistema != ETAPA && !string.IsNullOrEmpty(x.apli_tubicacion))
                            {
                                esOtraAplicacion = true;

                                if (x.sist_csistema == "TFL")
                                { 
                                    url = $"{BASE_URL}/TFL.{subMenu.codigoSistema}/{x.apli_tubicacion.Replace("../", "")}";
                                }
                                else
                                {
                                    url = $"{BASE_URL}/{x.apli_tubicacion}"; 
                                }

                                url = url.Replace("http://", "https://");           // Coloca el https
                            }

                            return new ItemSubMenu()
                            {
                                nombre = x.app_descrip,
                                url = url,      // x.apli_tubicacion,
                                autorizado = (x.habilitado == 1),
                                esOtraAplicacion = esOtraAplicacion
                            };
                        })
                        .ToList();

                        subMenu.listaItemSubMenu = listaItemSubMenu;
                    }

                    menu.listaSubMenu = listaSubMenu;
                }

                return listaMenu;
            }
            else
            {
                resError = resSP;
            }

            return new List<MenuAcceso>();
        }

        private static List<TFL_MODULOS_ACCESOS_ENT> ObtenerListaProbando()
        {
            return new List<TFL_MODULOS_ACCESOS_ENT>()
            {
                new TFL_MODULOS_ACCESOS_ENT {
                    menu_modulos_nombre = "Menú de Definiciones",
                    menu_modulos_url = "../Menu_Definiciones/Menu_Definiciones.aspx",
                    smenu_modulos_nombre = "Cualificaciones",
                    app_descrip = "Definición Sectores Productivos MCTP",
                    apli_tubicacion = "../DEF_SPRODUCTIVOS/DEF_SPRODUCTIVOS.aspx",
                    habilitado = 1
                },
                new TFL_MODULOS_ACCESOS_ENT {
                    menu_modulos_nombre = "Menú de Definiciones",
                    menu_modulos_url = "../Menu_Definiciones/Menu_Definiciones.aspx",
                    smenu_modulos_nombre = "Cualificaciones",
                    app_descrip = "Definición Sub Sectores Productivos MCTP",
                    apli_tubicacion = "../DEF_SSPRODUCTIVOS/DEF_SSPRODUCTIVOS.aspx",
                    habilitado = 0
                },
                new TFL_MODULOS_ACCESOS_ENT {
                    menu_modulos_nombre = "Menú de Definiciones",
                    menu_modulos_url = "../Menu_Definiciones/Menu_Definiciones.aspx",
                    smenu_modulos_nombre = "Rutas",
                    app_descrip = "Definición Rutas",
                    apli_tubicacion = "../DEF_RUTAS/DEF_RUTAS.aspx",
                    habilitado = 1
                },
                new TFL_MODULOS_ACCESOS_ENT {
                    menu_modulos_nombre = "Menú de Definiciones",
                    menu_modulos_url = "../Menu_Definiciones/Menu_Definiciones.aspx",
                    smenu_modulos_nombre = "Rutas",
                    app_descrip = "Periodos de Poblamiento",
                    apli_tubicacion = "../DEF_PPOBLAMIENTO/DEF_PPOBLAMIENTO.aspx",
                    habilitado = 0
                },
                new TFL_MODULOS_ACCESOS_ENT {
                    menu_modulos_nombre = "Menú Operacional",
                    menu_modulos_url = "../Menu_Operacional/Menu_Operacional.aspx",
                    smenu_modulos_nombre = "Cualificaciones",
                    app_descrip = "Mantención Cualificaciones",
                    apli_tubicacion = "../CUALIFICACIONES/CUALIFICACIONES.aspx",
                    habilitado = 1
                }
            };
        }

        public static bool EstaAutorizado(
            string nombreTabla,      // "DEF_SPRODUCTIVOS"
            params Permisos[] permisos
        )
        {
            /*
            // Si en el Web.config está comentado el validarAutorizacion, entonces no valida permisos de usuario

            string validarAutorizacion = ConfigurationManager.AppSettings["validarAutorizacion"];

            if (string.IsNullOrEmpty(validarAutorizacion)) {
                return true;
            }
            */

            int rut = GetUsuario().rutNumero;
            string p_cacplicacion = $"TFL_{nombreTabla}";     // "TFL_DEF_SPRODUCTIVOS"

            TFL_MODULOS_ACCESOS_Modelo_Datos dataAccess = new TFL_MODULOS_ACCESOS_Modelo_Datos();

            RespuestaSP resSP = dataAccess.TFL_MODULOS_ACCESOS(
                "TFL",
                "TFL",      // "ETAPA1",     // Aca va el código de la Etapa
                rut, 
                p_cacplicacion, 
                out List<TFL_MODULOS_ACCESOS_ENT> datosDB
            );

            if (resSP.swt == 0 || resSP.swt == 1)
            {
                bool autorizado = false;

                foreach (Permisos permiso in permisos)
                {
                    if (permiso == Permisos.SELECT && datosDB.Any(x => x.permiso == "SELECT"))
                        autorizado = true;

                    if (permiso == Permisos.UPDATE && datosDB.Any(x => x.permiso == "UPDATE"))
                        autorizado = true;
                }

                return autorizado;
            }

            return false;
        }

        public static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}