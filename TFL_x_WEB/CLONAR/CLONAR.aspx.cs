using Inacap.LogginException;
using MCTP_a_Exception;
using MCTP_c_Modelos_de_Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using TFL_x_WEB.Helpers;

namespace TFL_x_WEB.CLONAR
{
    public partial class CLONAR : Page
    {
        private static CLONAR_Modelo_Datos _dataAccess;
        protected static DEF_DIR_SEC_VRA_Modelo_Datos _dataAccess_DEF_DIR_SEC_VRA;
        protected static USUARIO_ENT usuario { get; set; } = new USUARIO_ENT();
        protected static List<COMBOBOX_ENT> periodosVigenciaTFL { get; set; }
        protected static List<COMBOBOX_ENT> dir_sec_vra { get; set; }
        protected static string errorCarga { get; set; } = string.Empty;

        public CLONAR()
        {
            _dataAccess = new CLONAR_Modelo_Datos();
            _dataAccess_DEF_DIR_SEC_VRA = new DEF_DIR_SEC_VRA_Modelo_Datos();
            periodosVigenciaTFL = new List<COMBOBOX_ENT>();
            dir_sec_vra = new List<COMBOBOX_ENT>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = SesionHelper.GetUsuario();

                var dataAccessDefPvigencia = new DEF_PVIGENCIA_TFL_Modelo_Datos();
                RespuestaSP resSP_DefPvigenciaTfl = dataAccessDefPvigencia.GetCombobox(out List<COMBOBOX_ENT> listaPeriodosVigenciaTFL);

                if (resSP_DefPvigenciaTfl.swt == 0)
                    periodosVigenciaTFL = listaPeriodosVigenciaTFL;

                RespuestaSP resSP_DefDirSecVra = _dataAccess_DEF_DIR_SEC_VRA.GetCombobox(out List<COMBOBOX_ENT> listaDefDirSecVra);

                if (resSP_DefDirSecVra.swt == 0 || resSP_DefDirSecVra.swt == 1)
                    dir_sec_vra = listaDefDirSecVra;

                // No leer cache del navegador web
                Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate");
                Response.AppendHeader("Pragma", "no-cache");
                Response.AppendHeader("Expires", "0");
            }
            catch (Exception ex)
            {
                errorCarga = ExceptionHelper.ToErrorPersonalizado(ex);
            }
        }

        /*
        [WebMethod]
        public static void DEF_TFL_LEE(int def_tfl_ncorr)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                var dataAccess = new DEF_TFL_Modelo_Datos();

                RespuestaSP resSP = dataAccess.GetDatosDefTFL__INCORRECTO(
                    def_tfl_ncorr,
                    out string def_tfl_nombre,
                    out string def_tfl_descrip,
                    out string def_tfl_version,
                    out string def_tfl_fefect
                );

                if (resSP.swt == 0 || resSP.swt == 1)
                    res.objeto = new { def_tfl_nombre, def_tfl_descrip, def_tfl_version, def_tfl_fefect };
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
        */

        [WebMethod]
        public static void DEF_AREA_SEL(int direccionSectorialNcorr)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                var dataAccess = new DEF_AREA_Modelo_Datos();
                RespuestaSP resSP = dataAccess.GetCombobox(direccionSectorialNcorr, out List<COMBOBOX_ENT> lista);

                if (resSP.swt == 0 || resSP.swt == 1)
                    res.objeto = lista;
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

        [WebMethod]
        public static void DEF_TFL_SEL(int defAreaNcorr, int defPvigenciaTFL_ncorr)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                var dataAccess = new DEF_TFL_Modelo_Datos();
                RespuestaSP resSP = dataAccess.GetCombobox(defAreaNcorr, defPvigenciaTFL_ncorr, out List<COMBOBOX_ENT> lista);

                if (resSP.swt == 0 || resSP.swt == 1)
                    res.objeto = lista;
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

        [WebMethod]
        public static void TRAE_ESTADO_TFL(int p_def_tfl_ncorr, int p_def_tfl_version)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                VOLVER_ATRAS_Modelo_Datos dataAccess = new VOLVER_ATRAS_Modelo_Datos();

                RespuestaSP resSP = dataAccess.TRAE_ESTADO_TFL(
                    p_def_tfl_ncorr,    // p_def_tfl_ncorr 
                    p_def_tfl_version,  // p_def_tfl_version
                    out DataTable dt
                );

                var primeraFila = dt.Rows[0];

                object infoTFL = new
                {
                    nombre_tfl = primeraFila.GetDataRowValue<string>("nombre_tfl"),
                    estado_tfl = primeraFila.GetDataRowValue<string>("estado_tfl"),
                    fecha_efectiva = primeraFila.GetDataRowValue<string>("fecha_efectiva")
                };

                res.objeto = new { infoTFL, resSP };
            }
            catch (Exception ex)
            {
                string msnError = LogException.WriteToEventLog(ex);
                res.AgregarInternalServerError(msnError);
            }

            RetornarJson(res);
        }

        [WebMethod] 
        public static void CLONAR_GRABAR(
            int p_def_tfl_ncorr, 
            int p_def_tfl_version,
            int p_nperiodo, 
            string p_def_tfl_nombre, 
            int p_def_tfl_ncualficaciones, 
            int p_def_tfl_nucl,
            string p_def_tfl_fefect,
            string p_def_tfl_descrip
        ) 
        { 
            RespuestaBackend res = new RespuestaBackend();

            try 
            {
                // res.AgregarMensajeExito("PRUEBA ÉXITO");

                //====================>>>>
         
                string p_audi_tusuario = usuario.rutNumero.ToString();

                RespuestaSP resSP = _dataAccess.CLONAR(
                    p_def_tfl_ncorr,                // p_def_tfl_ncorr 
                    p_def_tfl_version,              // p_def_tfl_version
                    p_nperiodo,                     // p_nperiodo 
                    p_def_tfl_nombre,               // p_def_tfl_nombre 
                    p_def_tfl_ncualficaciones,      // p_def_tfl_ncualficaciones 
                    p_def_tfl_nucl,                 // p_def_tfl_nucl 
                    p_def_tfl_fefect,               // p_def_tfl_fefect 
                    p_def_tfl_descrip,              // p_def_tfl_descrip 
                    p_audi_tusuario                 // p_audi_tusuario 
                );

                if (resSP.swt == 0 || resSP.swt == 1)
                    res.AgregarMensajeExito(resSP.msg);
                else
                {
                    string msnError = LogException.LogException_pkg(resSP.swt, resSP.msg, resSP.sts, resSP.tbl, resSP.pkgp);
                    res.AgregarInternalServerError(msnError);
                }

                //====================>>>>
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