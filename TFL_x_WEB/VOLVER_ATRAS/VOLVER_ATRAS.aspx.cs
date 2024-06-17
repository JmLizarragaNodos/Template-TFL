using Inacap.LogginException;
using Inacap.Validators;
using MCTP_a_Exception;
using MCTP_c_Modelos_de_Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFL_x_WEB.Dto;
using TFL_x_WEB.Helpers;

namespace TFL_x_WEB.VOLVER_ATRAS
{
    public partial class VOLVER_ATRAS : Page
    {
        private static VOLVER_ATRAS_Modelo_Datos _dataAccess;
        protected static DEF_DIR_SEC_VRA_Modelo_Datos _dataAccess_DEF_DIR_SEC_VRA;
        protected static USUARIO_ENT usuario { get; set; } = new USUARIO_ENT();
        protected static List<COMBOBOX_ENT> periodosVigenciaTFL { get; set; }
        protected static List<COMBOBOX_ENT> dir_sec_vra { get; set; }
        protected static string errorCarga { get; set; } = string.Empty;

        public VOLVER_ATRAS()
        {
            _dataAccess = new VOLVER_ATRAS_Modelo_Datos();
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

        [WebMethod]
        public static void GetCboDefArea(int direccionSectorialNcorr)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                var dataAccess = new DEF_AREA_Modelo_Datos();
                RespuestaSP resSP = dataAccess.GetCombobox(direccionSectorialNcorr, out List<COMBOBOX_ENT> lista);

                if (resSP.swt == 0 || resSP.swt == 1)
                    res.objeto = new { cboCboDefArea = lista };
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
        public static void GetCboDefTFL(int defAreaNcorr, int defPvigenciaTFL_ncorr)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                var dataAccess = new DEF_TFL_Modelo_Datos();
                RespuestaSP resSP = dataAccess.GetCombobox(defAreaNcorr, defPvigenciaTFL_ncorr, out List<COMBOBOX_ENT> lista);

                if (resSP.swt == 0 || resSP.swt == 1)
                    res.objeto = new { cboDefTFL = lista };
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

        /*
        [WebMethod]
        public static void GetDatosDefTFL(int? def_tfl_ncorr)  // Obtener datos de la tfl seleccionada en el filtro
        {
            RespuestaBackend res = new RespuestaBackend();
            var dataAccess = new DEF_TFL_Modelo_Datos();

            try
            {
                RespuestaSP resSP = dataAccess.GetDatosDefTFL(
                    def_tfl_ncorr.Value,
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

        public class Card
        {
            public string titulo { get; set; }
            public List<CardItem> items { get; set; } = new List<CardItem>();
        }

        public class CardItem
        {
            public int publicada { get; set; } 
            public string titulo { get; set; }
            public string apli_caplicacion { get; set; }
        }

        [WebMethod]
        public static void TRAE_ESTADO_TFL(int p_def_tfl_ncorr)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                //res.objeto = ObtenerDatosDummy();

                RespuestaSP resSP = _dataAccess.TRAE_ESTADO_TFL(
                    p_def_tfl_ncorr,   // p_def_tfl_ncorr 
                    out DataTable dt
                );

                //===================>>>>
                // Sección solo para pruebas

                int probar = 0;

                if (probar == 1)
                {
                    resSP.swt = 2;
                    resSP.msg = "Probando mensaje";
                }

                //===================>>>>

                var primeraFila = dt.Rows[0];

                object infoTFL = new {
                    nombre_tfl = primeraFila.GetDataRowValue<string>("nombre_tfl"),
                    estado_tfl = primeraFila.GetDataRowValue<string>("estado_tfl"),
                    fecha_efectiva = primeraFila.GetDataRowValue<string>("fecha_efectiva")
                };

                var listaObjetos = new List<Card>();
                List<string> codigosSistemas = dt.ToListDistinct<string>("modulos_sist_ccod");  // ["ETAPA1", "ETAPA2", "ETAPA3", "ETAPA4"]

                foreach (string codigoSistema in codigosSistemas)
                {
                    var dataTableItems = dt.FindByField(nombreCampo: "modulos_sist_ccod", valorCampo: codigoSistema);

                    var card = new Card
                    {
                        titulo = dataTableItems.Rows[0].GetDataRowValue<string>("smenu_modulos_nombre")
                    };

                    foreach (DataRow y in dataTableItems.Rows) 
                    {
                        card.items.Add(new CardItem
                        {
                            publicada = y.GetDataRowValue<int>("publicada"),
                            titulo = y.GetDataRowValue<string>("app_descrip"),
                            apli_caplicacion = y.GetDataRowValue<string>("apli_caplicacion"),
                        });
                    }

                    listaObjetos.Add(card);
                }

                res.objeto = new { listaObjetos, infoTFL, resSP };
            }
            catch (Exception ex)
            {
                string msnError = LogException.WriteToEventLog(ex);
                res.AgregarInternalServerError(msnError);
            }

            RetornarJson(res);
        }

        [WebMethod]
        public static void VOLVER_ATRAS_BACKEND(int p_def_tfl_ncorr, string p_apli_caplicacion)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                //throw new Exception("PRUEBA DETENER");

                string p_audi_tusuario = usuario.rutNumero.ToString();

                RespuestaSP resSP = _dataAccess.VOLVER_ATRAS(
                    p_def_tfl_ncorr,        // p_def_tfl_ncorr 
                    p_apli_caplicacion,     // p_apli_caplicacion
                    p_audi_tusuario         // p_audi_tusuario 
                );

                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(resSP);

                if (resSP.swt == 0 || resSP.swt == 1)
                    res.AgregarMensajeExito(resSP.msg);
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
        public static void ObtenerMensajeVolverAtras(string p_apli_caplicacion)
        {
            RespuestaBackend res = new RespuestaBackend();

            try
            {
                RespuestaSP resSP = _dataAccess.MSG_VOLVER_ATRAS(p_apli_caplicacion);
                res.objeto = new { mensaje = resSP.msg };
            }
            catch (Exception ex)
            {
                string msnError = LogException.WriteToEventLog(ex);
                res.AgregarInternalServerError(msnError);
            }

            RetornarJson(res);
        }

        /*
        private static List<Card> ObtenerDatosDummy()
        {
            var listaObjetos = new List<Card>();

            foreach (int x in new List<int>() { 1, 2, 3, 4 })
            {
                var items = new List<CardItem>();

                foreach (var y in new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 })
                {
                    bool estaPublicada = (y <= 7);

                    items.Add(new CardItem {
                        estaPublicada = estaPublicada,
                        puedeVolverAtras = estaPublicada,
                        titulo = $"Pantalla {x}{y}" 
                    });
                }

                listaObjetos.Add(new Card { titulo = $"Etapa {x}", items = items });
            }

            return listaObjetos;
        } 
        */

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