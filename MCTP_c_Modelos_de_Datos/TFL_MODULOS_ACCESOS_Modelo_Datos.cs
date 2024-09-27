using System;
using Oracle.DataAccess.Client;
using System.Data;
using MCTP_c_Modelos_de_Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using System.Collections.Generic;
using System.Linq;

namespace MCTP_c_Modelos_de_Datos
{
	public class TFL_MODULOS_ACCESOS_Modelo_Datos : Inacap.Common.Dal.Oracle
	{
		private int _msg_Size;
		private int _sts_Size;
		private int _tbl_Size;
		private int _pkgp_Size;

		public TFL_MODULOS_ACCESOS_Modelo_Datos()
		{
			_msg_Size = Output_Size.Msg;
			_sts_Size = Output_Size.Sts;
			_tbl_Size = Output_Size.Tbl;
			_pkgp_Size = Output_Size.Pkgp;
		}

		private TFL_MODULOS_ACCESOS_ENT ObtenerObjeto(DataRow x)
		{
			return new TFL_MODULOS_ACCESOS_ENT
			{
				menu_modulos_nombre = x["menu_modulos_nombre"].ToString(),
				menu_modulos_prelacion = int.Parse(x["menu_modulos_prelacion"].ToString()),
				menu_modulos_url = x["menu_modulos_url"].ToString(),
				smenu_modulos_nombre = x["smenu_modulos_nombre"].ToString(),
				smenu_modulos_prelacion = int.Parse(x["smenu_modulos_prelacion"].ToString()),
				apli_caplicacion = x["apli_caplicacion"].ToString(),
				app_descrip = x["app_descrip"].ToString(),
				app_prelacion = int.Parse(x["app_prelacion"].ToString()),
				apli_tubicacion = x["apli_tubicacion"].ToString(),
				permiso = x["permiso"].ToString(),
				habilitado = int.Parse(x["habilitado"].ToString()),
				modulos_sist_ccod = x["modulos_sist_ccod"].ToString(),
				sist_csistema = x["sist_csistema"].ToString()
			};
		}

		public RespuestaSP TFL_MODULOS_ACCESOS
		(
			string p_sist_csistema, 
			string p_modulos_sist_ccod,
			int p_rut_usuario,
			string p_cacplicacion, 
			out List<TFL_MODULOS_ACCESOS_ENT> outcur
		)
		{
			outcur = new List<TFL_MODULOS_ACCESOS_ENT>();

			try
			{
				IDataParameter[] param = new IDataParameter[10];

				param[0] = new OracleParameter("p_sist_csistema", OracleDbType.Varchar2);
				param[1] = new OracleParameter("p_modulos_sist_ccod", OracleDbType.Varchar2);
				param[2] = new OracleParameter("p_rut_usuario", OracleDbType.Int32);
				param[3] = new OracleParameter("p_cacplicacion", OracleDbType.Varchar2);
				param[4] = new OracleParameter("outcur", OracleDbType.RefCursor);
				param[5] = new OracleParameter("p_swt", OracleDbType.Int32);
				param[6] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
				param[7] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
				param[8] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param[9] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				param[0].Value = p_sist_csistema;
				param[1].Value = p_modulos_sist_ccod;
				param[2].Value = p_rut_usuario;
				param[3].Value = p_cacplicacion;
				param[4].Direction = ParameterDirection.Output;
				param[5].Direction = ParameterDirection.Output;
				param[6].Direction = ParameterDirection.Output;
				param[7].Direction = ParameterDirection.Output;
				param[8].Direction = ParameterDirection.Output;
				param[9].Direction = ParameterDirection.Output;

				DataTable dt = null;
				ExecuteStoredProcedure("TFL_MODULOS_ACCESOS_PKG.TFL_MODULOS_ACCESOS", ref param, ref dt);

				if (dt != null && dt.Rows.Count > 0)
				{
					outcur = (from DataRow x in dt.Rows select ObtenerObjeto(x)).ToList();
				}

				/*
				
				*/

				return new RespuestaSP()
				{
					swt = int.Parse(param[5].Value.ToString()),
					msg = param[6].Value.ToString(),
					sts = param[7].Value.ToString(),
					tbl = param[8].Value.ToString(),
					pkgp = param[9].Value.ToString(),
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP TFL_PROCESAR_APLICACION
		(
			string p_cacplicacion,
			string p_pers_nrut,
			string p_sesi_ccod,
			out string urlout,
			out string err_msg
		)
		{
			urlout = string.Empty;
			err_msg = string.Empty;

			try
			{
				IDataParameter[] param = new IDataParameter[9];

				param[0] = new OracleParameter("p_cacplicacion", OracleDbType.Varchar2);
				param[1] = new OracleParameter("p_pers_nrut", OracleDbType.Varchar2);
				param[2] = new OracleParameter("p_sesi_ccod", OracleDbType.Varchar2);
				param[3] = new OracleParameter("outcur", OracleDbType.RefCursor);
				param[4] = new OracleParameter("p_swt", OracleDbType.Int32);
				param[5] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
				param[6] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
				param[7] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param[8] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				param[0].Value = p_cacplicacion;
				param[1].Value = p_pers_nrut;
				param[2].Value = p_sesi_ccod;
				param[3].Direction = ParameterDirection.Output;
				param[4].Direction = ParameterDirection.Output;
				param[5].Direction = ParameterDirection.Output;
				param[6].Direction = ParameterDirection.Output;
				param[7].Direction = ParameterDirection.Output;
				param[8].Direction = ParameterDirection.Output;

				DataTable dt = null;
				ExecuteStoredProcedure("TFL_MODULOS_ACCESOS_PKG.TFL_PROCESAR_APLICACION", ref param, ref dt);

				if (dt != null && dt.Rows.Count > 0)
				{
					urlout = dt.Rows[0]["urlout"].ToString();
					err_msg = dt.Rows[0]["err_msg"].ToString();
				}

				/*
				SET SERVEROUTPUT ON; 

				DECLARE 
					outcur SYS_REFCURSOR; 
				BEGIN 
					SECURITY.pkg_app_externa.ProcesarAplicacionSiga( 
						p_nAplicacion => 46271492,   -- 46271492 = 'TFL_ETAPA1'     |       97607061 = 'TFL_ETAPA2'
						p_sesi_ccod => '168204134L262AAAI5ZAAAAABFTpAAAJ25232522023', 
						outcur => outcur
					); 
					DBMS_SQL.RETURN_RESULT(outcur); 
				END;  
				*/

				return new RespuestaSP()
				{
					swt = int.Parse(param[4].Value.ToString()),
					msg = param[5].Value.ToString(),
					sts = param[6].Value.ToString(),
					tbl = param[7].Value.ToString(),
					pkgp = param[8].Value.ToString(),
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}
