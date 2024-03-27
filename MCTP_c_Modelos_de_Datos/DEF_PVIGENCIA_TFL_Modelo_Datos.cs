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
    public class DEF_PVIGENCIA_TFL_Modelo_Datos : Inacap.Common.Dal.Oracle
	{
        private int _msg_Size;
        private int _sts_Size;
        private int _tbl_Size;
        private int _pkgp_Size;

        public DEF_PVIGENCIA_TFL_Modelo_Datos()
        {
            _msg_Size = Output_Size.Msg;
            _sts_Size = Output_Size.Sts;
            _tbl_Size = Output_Size.Tbl;
            _pkgp_Size = Output_Size.Pkgp;
        }

        private DEF_PVIGENCIA_TFL_ENT ObtenerObjeto(DataRow x)
        {
            return new DEF_PVIGENCIA_TFL_ENT
            {
				def_pvigencia_tfl_ncorr = int.Parse(x["def_pvigencia_tfl_ncorr"].ToString()),
				def_pvigencia_tfl_nombre = x["def_pvigencia_tfl_nombre"].ToString(),
				def_pvigencia_tfl_descrip = x["def_pvigencia_tfl_descrip"].ToString(),
				def_pvigencia_tfl_finicio = x["def_pvigencia_tfl_finicio"].ToString(),
				def_pvigencia_tfl_ftermino = x["def_pvigencia_tfl_ftermino"].ToString(),
				def_pvigencia_tfl_estado = x["def_pvigencia_tfl_estado"].ToString()
			};
        }

		public RespuestaSP DEF_PVIGENCIA_TFL_SEL
		(
			int? p_def_pvigencia_tfl_ncorr,
			string p_def_pvigencia_tfl_nombre,
			string p_def_pvigencia_tfl_descrip,
			string p_def_pvigencia_tfl_estado,
			string p_def_pvigencia_tfl_finicio,
			string p_def_pvigencia_tfl_ftermino,
			int p_cantidad,
			out List<DEF_PVIGENCIA_TFL_ENT> outcur
		)
		{
			outcur = new List<DEF_PVIGENCIA_TFL_ENT>();

			try
			{
				IDataParameter[] param = new IDataParameter[13];

				param[0] = new OracleParameter("p_def_pvigencia_tfl_ncorr", OracleDbType.Int32);
				param[1] = new OracleParameter("p_def_pvigencia_tfl_nombre", OracleDbType.Varchar2);
				param[2] = new OracleParameter("p_def_pvigencia_tfl_descrip", OracleDbType.Varchar2);
				param[3] = new OracleParameter("p_def_pvigencia_tfl_estado", OracleDbType.Varchar2);
				param[4] = new OracleParameter("p_def_pvigencia_tfl_finicio", OracleDbType.Varchar2);
				param[5] = new OracleParameter("p_def_pvigencia_tfl_ftermino", OracleDbType.Varchar2);
				param[6] = new OracleParameter("p_cantidad", OracleDbType.Int32);
				param[7] = new OracleParameter("outcur", OracleDbType.RefCursor);
				param[8] = new OracleParameter("p_swt", OracleDbType.Int32);
				param[9] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
				param[10] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
				param[11] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param[12] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				param[0].Value = p_def_pvigencia_tfl_ncorr;
				param[1].Value = p_def_pvigencia_tfl_nombre;
				param[2].Value = p_def_pvigencia_tfl_descrip;
				param[3].Value = p_def_pvigencia_tfl_estado;
				param[4].Value = p_def_pvigencia_tfl_finicio;
				param[5].Value = p_def_pvigencia_tfl_ftermino;
				param[6].Value = p_cantidad;
				param[7].Direction = ParameterDirection.Output;
				param[8].Direction = ParameterDirection.Output;
				param[9].Direction = ParameterDirection.Output;
				param[10].Direction = ParameterDirection.Output;
				param[11].Direction = ParameterDirection.Output;
				param[12].Direction = ParameterDirection.Output;

				DataTable dt = null;
				ExecuteStoredProcedure("DEF_PVIGENCIA_TFL_PKG.DEF_PVIGENCIA_TFL_SEL", ref param, ref dt);

				if (dt != null && dt.Rows.Count > 0)
				{
					outcur = (from DataRow x in dt.Rows select ObtenerObjeto(x)).ToList();
				}

				return new RespuestaSP()
				{
					swt = int.Parse(param[8].Value.ToString()),
					msg = param[9].Value.ToString(),
					sts = param[10].Value.ToString(),
					tbl = param[11].Value.ToString(),
					pkgp = param[12].Value.ToString(),
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP DEF_PVIGENCIA_TFL_LEE(int p_kya, out DEF_PVIGENCIA_TFL_ENT outcur)
		{
			outcur = new DEF_PVIGENCIA_TFL_ENT();

			try
			{
				IDataParameter[] param = new IDataParameter[7];

				param[0] = new OracleParameter("p_kya", OracleDbType.Int32);
				param[1] = new OracleParameter("outcur", OracleDbType.RefCursor);
				param[2] = new OracleParameter("p_swt", OracleDbType.Int32);
				param[3] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
				param[4] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
				param[5] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param[6] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				param[0].Value = p_kya;
				param[1].Direction = ParameterDirection.Output;
				param[2].Direction = ParameterDirection.Output;
				param[3].Direction = ParameterDirection.Output;
				param[4].Direction = ParameterDirection.Output;
				param[5].Direction = ParameterDirection.Output;
				param[6].Direction = ParameterDirection.Output;

				DataTable dt = null;
				ExecuteStoredProcedure("DEF_PVIGENCIA_TFL_PKG.def_pvigencia_tfl_lee", ref param, ref dt);

				if (dt != null && dt.Rows.Count > 0)
				{
					outcur = ObtenerObjeto(dt.Rows[0]);
				}

				return new RespuestaSP()
				{
					swt = int.Parse(param[2].Value.ToString()),
					msg = param[3].Value.ToString(),
					sts = param[4].Value.ToString(),
					tbl = param[5].Value.ToString(),
					pkgp = param[6].Value.ToString(),
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP DEF_PVIGENCIA_TFL_GRA
		(
			string p_def_pvigencia_tfl_nombre,
			string p_def_pvigencia_tfl_descrip,
			string p_def_pvigencia_tfl_estado,
			string p_def_pvigencia_tfl_finicio,
			string p_def_pvigencia_tfl_ftermino,
			string p_audi_tusuario
		)
		{
			try
			{
				IDataParameter[] param = new IDataParameter[11];

				param[0] = new OracleParameter("p_def_pvigencia_tfl_nombre", OracleDbType.Varchar2);
				param[1] = new OracleParameter("p_def_pvigencia_tfl_descrip", OracleDbType.Varchar2);
				param[2] = new OracleParameter("p_def_pvigencia_tfl_estado", OracleDbType.Varchar2);
				param[3] = new OracleParameter("p_def_pvigencia_tfl_finicio", OracleDbType.Varchar2);
				param[4] = new OracleParameter("p_def_pvigencia_tfl_ftermino", OracleDbType.Varchar2);
				param[5] = new OracleParameter("p_audi_tusuario", OracleDbType.Varchar2);
				param[6] = new OracleParameter("p_swt", OracleDbType.Int32);
				param[7] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
				param[8] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
				param[9] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param[10] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				param[0].Value = p_def_pvigencia_tfl_nombre;
				param[1].Value = p_def_pvigencia_tfl_descrip;
				param[2].Value = p_def_pvigencia_tfl_estado;
				param[3].Value = p_def_pvigencia_tfl_finicio;
				param[4].Value = p_def_pvigencia_tfl_ftermino;
				param[5].Value = p_audi_tusuario;
				param[6].Direction = ParameterDirection.Output;
				param[7].Direction = ParameterDirection.Output;
				param[8].Direction = ParameterDirection.Output;
				param[9].Direction = ParameterDirection.Output;
				param[10].Direction = ParameterDirection.Output;

				DataTable dt = null;
				ExecuteStoredProcedure("DEF_PVIGENCIA_TFL_PKG.def_pvigencia_tfl_gra", ref param, ref dt);

				return new RespuestaSP()
				{
					swt = int.Parse(param[6].Value.ToString()),
					msg = param[7].Value.ToString(),
					sts = param[8].Value.ToString(),
					tbl = param[9].Value.ToString(),
					pkgp = param[10].Value.ToString(),
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP DEF_PVIGENCIA_TFL_UPD
		(
			int p_kya,
			string p_def_pvigencia_tfl_nombre,
			string p_def_pvigencia_tfl_descrip,
			string p_def_pvigencia_tfl_estado,
			string p_def_pvigencia_tfl_finicio,
			string p_def_pvigencia_tfl_ftermino,
			string p_audi_tusuario
		)
		{
			try
			{
				IDataParameter[] param = new IDataParameter[12];

				param[0] = new OracleParameter("p_kya", OracleDbType.Int32);
				param[1] = new OracleParameter("p_def_pvigencia_tfl_nombre", OracleDbType.Varchar2);
				param[2] = new OracleParameter("p_def_pvigencia_tfl_descrip", OracleDbType.Varchar2);
				param[3] = new OracleParameter("p_def_pvigencia_tfl_estado", OracleDbType.Varchar2);
				param[4] = new OracleParameter("p_def_pvigencia_tfl_finicio", OracleDbType.Varchar2);
				param[5] = new OracleParameter("p_def_pvigencia_tfl_ftermino", OracleDbType.Varchar2);
				param[6] = new OracleParameter("p_audi_tusuario", OracleDbType.Varchar2);
				param[7] = new OracleParameter("p_swt", OracleDbType.Int32);
				param[8] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
				param[9] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
				param[10] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param[11] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				param[0].Value = p_kya;
				param[1].Value = p_def_pvigencia_tfl_nombre;
				param[2].Value = p_def_pvigencia_tfl_descrip;
				param[3].Value = p_def_pvigencia_tfl_estado;
				param[4].Value = p_def_pvigencia_tfl_finicio;
				param[5].Value = p_def_pvigencia_tfl_ftermino;
				param[6].Value = p_audi_tusuario;
				param[7].Direction = ParameterDirection.Output;
				param[8].Direction = ParameterDirection.Output;
				param[9].Direction = ParameterDirection.Output;
				param[10].Direction = ParameterDirection.Output;
				param[11].Direction = ParameterDirection.Output;

				DataTable dt = null;
				ExecuteStoredProcedure("DEF_PVIGENCIA_TFL_PKG.def_pvigencia_tfl_upd", ref param, ref dt);

				return new RespuestaSP()
				{
					swt = int.Parse(param[7].Value.ToString()),
					msg = param[8].Value.ToString(),
					sts = param[9].Value.ToString(),
					tbl = param[10].Value.ToString(),
					pkgp = param[11].Value.ToString(),
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP DEF_PVIGENCIA_TFL_DEL(int p_kya, string p_audi_tusuario)
		{
			try
			{
				IDataParameter[] param = new IDataParameter[7];

				param[0] = new OracleParameter("p_kya", OracleDbType.Int32);
				param[1] = new OracleParameter("p_audi_tusuario", OracleDbType.Varchar2);
				param[2] = new OracleParameter("p_swt", OracleDbType.Int32);
				param[3] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
				param[4] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
				param[5] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param[6] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				param[0].Value = p_kya;
				param[1].Value = p_audi_tusuario;
				param[2].Direction = ParameterDirection.Output;
				param[3].Direction = ParameterDirection.Output;
				param[4].Direction = ParameterDirection.Output;
				param[5].Direction = ParameterDirection.Output;
				param[6].Direction = ParameterDirection.Output;

				DataTable dt = null;
				ExecuteStoredProcedure("DEF_PVIGENCIA_TFL_PKG.def_pvigencia_tfl_del", ref param, ref dt);

				return new RespuestaSP()
				{
					swt = int.Parse(param[2].Value.ToString()),
					msg = param[3].Value.ToString(),
					sts = param[4].Value.ToString(),
					tbl = param[5].Value.ToString(),
					pkgp = param[6].Value.ToString(),
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP DEF_PVIGENCIA_TFL_ENTRADAS(out List<ENTRADAS_ENT> lista)
		{
			try
			{
				IDataParameter[] param = new IDataParameter[6];

				param[0] = new OracleParameter("outcur", OracleDbType.RefCursor);
				param[1] = new OracleParameter("p_swt", OracleDbType.Int32);
				param[2] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
				param[3] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
				param[4] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
				param[5] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

				param[0].Direction = ParameterDirection.Output;
				param[1].Direction = ParameterDirection.Output;
				param[2].Direction = ParameterDirection.Output;
				param[3].Direction = ParameterDirection.Output;
				param[4].Direction = ParameterDirection.Output;
				param[5].Direction = ParameterDirection.Output;

				DataTable dt = null;
				ExecuteStoredProcedure("DEF_PVIGENCIA_TFL_PKG.def_pvigencia_tfl_entradas", ref param, ref dt);

				lista = (from DataRow x in dt.Rows
						 select new ENTRADAS_ENT()
						 {
							 paap_ccod = int.Parse(x["paap_ccod"].ToString()),
							 paap_tvalor = x["paap_tvalor"].ToString()
						 })
				.ToList();

				return new RespuestaSP()
				{
					swt = int.Parse(param[1].Value.ToString()),
					msg = param[2].Value.ToString(),
					sts = param[3].Value.ToString(),
					tbl = param[4].Value.ToString(),
					pkgp = param[5].Value.ToString(),
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public RespuestaSP GetCombobox(out List<COMBOBOX_ENT> salida)
		{
			salida = new List<COMBOBOX_ENT>();

			try
			{
				RespuestaSP resSP = this.DEF_PVIGENCIA_TFL_SEL(
					null,           // p_def_pvigencia_tfl_ncorr 
					null,           // p_def_pvigencia_tfl_nombre 
					null,           // p_def_pvigencia_tfl_descrip 
					null,           // p_def_pvigencia_tfl_estado 
					null,           // p_def_pvigencia_tfl_finicio 
					null,           // p_def_pvigencia_tfl_ftermino 
					9998,           // p_cantidad 
					out List<DEF_PVIGENCIA_TFL_ENT> lista
				);

				if (resSP.swt == 0 || resSP.swt == 1)
				{
					salida = lista.Select(x => new COMBOBOX_ENT
					{
						codigo = x.def_pvigencia_tfl_ncorr.ToString(),
						descripcion = x.def_pvigencia_tfl_nombre
					}).ToList();
				}

				return resSP;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}
