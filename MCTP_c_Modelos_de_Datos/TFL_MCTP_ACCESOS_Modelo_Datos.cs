using System;
using Oracle.DataAccess.Client;
using System.Data;
using MCTP_c_Modelos_de_Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using System.Collections.Generic;
using System.Linq;
using Inacap.Common.Helpers.Datos;

namespace MCTP_c_Modelos_de_Datos
{
    public class TFL_MCTP_ACCESOS_Modelo_Datos : Inacap.Common.Dal.Oracle
    {
        private int _msg_Size;
        private int _sts_Size;
        private int _tbl_Size;
        private int _pkgp_Size;

        public TFL_MCTP_ACCESOS_Modelo_Datos()
        {
            _msg_Size = Output_Size.Msg;
            _sts_Size = Output_Size.Sts;
            _tbl_Size = Output_Size.Tbl;
            _pkgp_Size = Output_Size.Pkgp;
        }

        private TFL_MCTP_ACCESOS_ENT ObtenerObjeto(DataRow x)
        {
            return new TFL_MCTP_ACCESOS_ENT
            {
                grupo_orden = int.Parse(x["grupo_orden"].ToString()),
                grupo_menu = x["grupo_menu"].ToString(),
                nombre_app = x["nombre_app"].ToString(),
                ubicacion = x["ubicacion"].ToString(),
                menu_orden = int.Parse(x["menu_orden"].ToString())
            };
        }

        public RespuestaSP TFL_MCTP_ACCESOS(int p_rut_usuario, string p_apli_caplicacion, string p_rol_crol, out List<TFL_MCTP_ACCESOS_ENT> outcur)
        {
            outcur = new List<TFL_MCTP_ACCESOS_ENT>();

            try
            {
                /*
                TFL_MCTP_ACCESOS_PKG.TFL_MCTP_ACCESOS(
                    p_rut_usuario => 17083122,
                    p_apli_caplicacion => 'TFL.MCTP',
                    p_rol_crol => 'GST',
                    outcur => outcur,
                    p_swt => swt,
                    p_msg => msg,
                    p_sts => sts,
                    p_tbl => tbl,
                    p_pkgp => pkgp
                ); 
                */

                IDataParameter[] param = new IDataParameter[9];

                param[0] = new OracleParameter("p_rut_usuario", OracleDbType.Int32);
                param[1] = new OracleParameter("p_apli_caplicacion", OracleDbType.Varchar2);
                param[2] = new OracleParameter("p_rol_crol", OracleDbType.Varchar2);
                param[3] = new OracleParameter("outcur", OracleDbType.RefCursor);
                param[4] = new OracleParameter("p_swt", OracleDbType.Int32);
                param[5] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
                param[6] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
                param[7] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
                param[8] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

                param[0].Value = p_rut_usuario;
                param[1].Value = p_apli_caplicacion;
                param[2].Value = p_rol_crol;
                param[3].Direction = ParameterDirection.Output;
                param[4].Direction = ParameterDirection.Output;
                param[5].Direction = ParameterDirection.Output;
                param[6].Direction = ParameterDirection.Output;
                param[7].Direction = ParameterDirection.Output;
                param[8].Direction = ParameterDirection.Output;

                DataTable dt = null;
                ExecuteStoredProcedure("TFL_MCTP_ACCESOS_PKG.TFL_MCTP_ACCESOS", ref param, ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    outcur = (from DataRow x in dt.Rows select ObtenerObjeto(x)).ToList();
                }

                return new RespuestaSP()
                {
                    swt = int.Parse(param[4].Value.ToString()),
                    msg = param[5].Value.ToString(),
                    sts = param[6].Value.ToString(),
                    tbl = param[7].Value.ToString(),
                    pkgp = param[8].Value.ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string TRAER_FTERMINO_SESION(int i_pers_nrut)
        {
            string ftermino = string.Empty;

            try
            {
				IDataParameter[] param = new IDataParameter[2]; 

				param[0] = new OracleParameter("i_pers_nrut", OracleDbType.Int32); 
				param[1] = new OracleParameter("outcur", OracleDbType.RefCursor); 

				param[0].Value = i_pers_nrut; 
				param[1].Direction = ParameterDirection.Output; 
			
                DataTable dt = null;
                ExecuteStoredProcedure("PKG_TFL_GENERICOS.TRAER_FTERMINO_SESION", ref param, ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ftermino = dt.Rows[0]["ftermino"].ToString();
                }

                return ftermino;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ACTUALIZA_SESION(int i_pers_nrut)
        {
            try
            {
				IDataParameter[] param = new IDataParameter[2]; 

				param[0] = new OracleParameter("i_pers_nrut", OracleDbType.Int32); 
				param[1] = new OracleParameter("outcur", OracleDbType.RefCursor); 

				param[0].Value = i_pers_nrut; 
				param[1].Direction = ParameterDirection.Output; 


                DataTable dt = null;
                ExecuteStoredProcedure("PKG_TFL_GENERICOS.ACTUALIZA_SESION", ref param, ref dt);
                return dt;

                // if (dt != null && dt.Rows.Count > 0) { }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
