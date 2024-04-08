using Inacap.Common.Helpers.Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MCTP_c_Modelos_de_Datos
{
    public class VOLVER_ATRAS_Modelo_Datos : Inacap.Common.Dal.Oracle
    {
        private int _msg_Size;
        private int _sts_Size;
        private int _tbl_Size;
        private int _pkgp_Size;

        public VOLVER_ATRAS_Modelo_Datos()
        {
            _msg_Size = Output_Size.Msg;
            _sts_Size = Output_Size.Sts;
            _tbl_Size = Output_Size.Tbl;
            _pkgp_Size = Output_Size.Pkgp;
        }

		private TRAE_ESTADO_TFL_ENT ObtenerObjeto(DataRow x)
		{
			return new TRAE_ESTADO_TFL_ENT
			{

			};
		}

		public RespuestaSP TRAE_ESTADO_TFL(int p_def_tfl_ncorr, out DataTable dt)
		{
			dt = null;

			try
			{
				IDataParameter[] param = new
				{
					p_def_tfl_ncorr
				}.ToDataParameters(true);

				param.AddOutput("outcur", OracleDbType.RefCursor);
				param.AddOutput("p_swt", OracleDbType.Int32);
				param.AddOutput("p_msg", OracleDbType.Varchar2, _msg_Size);
				param.AddOutput("p_sts", OracleDbType.Varchar2, _sts_Size);
				param.AddOutput("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param.AddOutput("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				ExecuteStoredProcedure("VOLVER_ATRAS_PKG.TRAE_ESTADO_TFL", ref param, ref dt);

				return new RespuestaSP()
				{
					swt = param.ToIntValue("p_swt"),
					msg = param.ToStringValue("p_msg"),
					sts = param.ToStringValue("p_sts"),
					tbl = param.ToStringValue("p_tbl"),
					pkgp = param.ToStringValue("p_pkgp")
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP VOLVER_ATRAS(int p_def_tfl_ncorr, string p_apli_caplicacion, string p_audi_tusuario)
		{
			try
			{
				IDataParameter[] param = new
				{
					p_def_tfl_ncorr,
					p_apli_caplicacion,
					p_audi_tusuario
				}.ToDataParameters(true);

				param.AddOutput("outcur", OracleDbType.RefCursor);
				param.AddOutput("p_swt", OracleDbType.Int32);
				param.AddOutput("p_msg", OracleDbType.Varchar2, _msg_Size);
				param.AddOutput("p_sts", OracleDbType.Varchar2, _sts_Size);
				param.AddOutput("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param.AddOutput("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				DataTable dt = null;
				ExecuteStoredProcedure("VOLVER_ATRAS_PKG.VOLVER_ATRAS", ref param, ref dt);

				return new RespuestaSP()
				{
					swt = param.ToIntValue("p_swt"),
					msg = param.ToStringValue("p_msg"),
					sts = param.ToStringValue("p_sts"),
					tbl = param.ToStringValue("p_tbl"),
					pkgp = param.ToStringValue("p_pkgp")
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP MSG_VOLVER_ATRAS(string p_apli_caplicacion)
		{
			try
			{ 
				IDataParameter[] param = new IDataParameter[6]; 

				param[0] = new OracleParameter("p_apli_caplicacion", OracleDbType.Varchar2); 
				param[1] = new OracleParameter("p_swt", OracleDbType.Int32); 
				param[2] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size); 
				param[3] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size); 
				param[4] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size); 
				param[5] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size); 

				param[0].Value = p_apli_caplicacion; 
				param[1].Direction = ParameterDirection.Output; 
				param[2].Direction = ParameterDirection.Output; 
				param[3].Direction = ParameterDirection.Output; 
				param[4].Direction = ParameterDirection.Output; 
				param[5].Direction = ParameterDirection.Output; 

				DataTable dt = null;
				ExecuteStoredProcedure("VOLVER_ATRAS_PKG.MSG_VOLVER_ATRAS", ref param, ref dt);

				return new RespuestaSP()
				{
					swt = param.ToIntValue("p_swt"),
					msg = param.ToStringValue("p_msg"),
					sts = param.ToStringValue("p_sts"),
					tbl = param.ToStringValue("p_tbl"),
					pkgp = param.ToStringValue("p_pkgp")
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
