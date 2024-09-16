using Inacap.Common.Helpers.Datos;
using Oracle.DataAccess.Client;
using System;
using System.Data;

namespace MCTP_c_Modelos_de_Datos
{
    public class CLONAR_Modelo_Datos : Inacap.Common.Dal.Oracle
    {
        private int _msg_Size;
        private int _sts_Size;
        private int _tbl_Size;
        private int _pkgp_Size;

        public CLONAR_Modelo_Datos()
        {
            _msg_Size = Output_Size.Msg;
            _sts_Size = Output_Size.Sts;
            _tbl_Size = Output_Size.Tbl;
            _pkgp_Size = Output_Size.Pkgp;
        }

        public RespuestaSP CLONAR
        (
            int p_def_tfl_ncorr,
            int p_def_tfl_version,
            int p_nperiodo,
            string p_def_tfl_nombre,
            int p_def_tfl_ncualficaciones,
            int p_def_tfl_nucl,
            string p_def_tfl_fefect,
            string p_def_tfl_descrip,
            string p_audi_tusuario
        )
        {
            try
            {
                IDataParameter[] param = new IDataParameter[16];

                param[0] = new OracleParameter("p_def_tfl_ncorr", OracleDbType.Int32);
                param[1] = new OracleParameter("p_def_tfl_version", OracleDbType.Int32);
                param[2] = new OracleParameter("p_nperiodo", OracleDbType.Int32);
                param[3] = new OracleParameter("p_def_tfl_nombre", OracleDbType.Varchar2);
                param[4] = new OracleParameter("p_def_tfl_ncualficaciones", OracleDbType.Int32);
                param[5] = new OracleParameter("p_def_tfl_nucl", OracleDbType.Int32);
                param[6] = new OracleParameter("p_def_tfl_fefect", OracleDbType.Varchar2);
                param[7] = new OracleParameter("p_def_tfl_descrip", OracleDbType.Varchar2);
                param[8] = new OracleParameter("p_audi_tusuario", OracleDbType.Varchar2);
                param[9] = new OracleParameter("outcur", OracleDbType.RefCursor);
                param[10] = new OracleParameter("p_swt", OracleDbType.Int32);
                param[11] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
                param[12] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
                param[13] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
                param[14] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);
                param[15] = new OracleParameter("p_def_tfl_version_n", OracleDbType.Int32);

                param[0].Value = p_def_tfl_ncorr;
                param[1].Value = p_def_tfl_version;
                param[2].Value = p_nperiodo;
                param[3].Value = p_def_tfl_nombre;
                param[4].Value = p_def_tfl_ncualficaciones;
                param[5].Value = p_def_tfl_nucl;
                param[6].Value = p_def_tfl_fefect;
                param[7].Value = p_def_tfl_descrip;
                param[8].Value = p_audi_tusuario;
                param[9].Direction = ParameterDirection.Output;
                param[10].Direction = ParameterDirection.Output;
                param[11].Direction = ParameterDirection.Output;
                param[12].Direction = ParameterDirection.Output;
                param[13].Direction = ParameterDirection.Output;
                param[14].Direction = ParameterDirection.Output;
                param[15].Direction = ParameterDirection.Output;

                DataTable dt = null;
                ExecuteStoredProcedure("CLONAR_PKG.CLONAR", ref param, ref dt);

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //	outcur = (from DataRow x in dt.Rows select ObtenerObjeto(x)).ToList();
                //}

                return new RespuestaSP()
                {
                    swt = int.Parse(param[10].Value.ToString()),
                    msg = param[11].Value.ToString(),
                    sts = param[12].Value.ToString(),
                    tbl = param[13].Value.ToString(),
                    pkgp = param[14].Value.ToString(),
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*
		public RespuestaSP CLONAR
		(
			int p_def_tfl_ncorr,
			int p_nperiodo,
			string p_def_tfl_nombre,
			int p_def_tfl_ncualficaciones,
			int p_def_tfl_nucl,
			string p_def_tfl_fefect,
			string p_def_tfl_descrip,
			string p_audi_tusuario
		)
		{
			try
			{
				IDataParameter[] param = new
				{
					p_def_tfl_ncorr,
					p_nperiodo,
					p_def_tfl_nombre,
					p_def_tfl_ncualficaciones,
					p_def_tfl_nucl,
					p_def_tfl_fefect,
					p_def_tfl_descrip,
					p_audi_tusuario
				}.ToDataParameters(false);

				param.AddOutput("outcur", OracleDbType.RefCursor);
				param.AddOutput("p_swt", OracleDbType.Int32);
				param.AddOutput("p_msg", OracleDbType.Varchar2, _msg_Size);
				param.AddOutput("p_sts", OracleDbType.Varchar2, _sts_Size);
				param.AddOutput("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param.AddOutput("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				DataTable dt = null;
				ExecuteStoredProcedure("CLONAR_PKG.CLONAR", ref param, ref dt);

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
		*/
    }
}
