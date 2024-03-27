using Inacap.Common.Helpers.Datos;
using MCTP_c_Modelos_de_Datos.Entity;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace MCTP_c_Modelos_de_Datos
{
    public class DEF_TFL_Modelo_Datos : Inacap.Common.Dal.Oracle
    {
        private int _msg_Size;
        private int _sts_Size;
        private int _tbl_Size;
        private int _pkgp_Size;

        public DEF_TFL_Modelo_Datos()
        {
            _msg_Size = Output_Size.Msg;
            _sts_Size = Output_Size.Sts;
            _tbl_Size = Output_Size.Tbl;
            _pkgp_Size = Output_Size.Pkgp;
        }

        private DEF_TFL_ENT ObtenerObjeto(DataRow x)
        {
            return new DEF_TFL_ENT
            {
                def_tfl_ncorr = int.Parse(x["def_tfl_ncorr"].ToString()),
                def_tfl_nombre = x["def_tfl_nombre"].ToString(),
                def_tfl_descrip = x["def_tfl_descrip"].ToString(),
                def_tfl_version = int.Parse(x["def_tfl_version"].ToString()),
                def_tfl_ncualficaciones = int.Parse(x["def_tfl_ncualficaciones"].ToString()),
                def_tfl_nucl = int.Parse(x["def_tfl_nucl"].ToString()),
                def_tfl_fefect = x["def_tfl_fefect"].ToString(),
                def_tfl_estado = x["def_tfl_estado"].ToString(),
                def_pvigencia_tfl_ncorr = int.Parse(x["def_pvigencia_tfl_ncorr"].ToString()),
                def_pvigencia_tfl_nombre = x["def_pvigencia_tfl_nombre"].ToString(),
                def_dir_sec_vra_ncorr = int.Parse(x["def_dir_sec_vra_ncorr"].ToString()),
                def_dir_sec_vra_ccod = x["def_dir_sec_vra_ccod"].ToString(),
                def_dir_sec_vra_nombre = x["def_dir_sec_vra_nombre"].ToString(),
                def_dir_sec_vra_cnombre = x["def_dir_sec_vra_cnombre"].ToString(),
                def_area_ncorr = int.Parse(x["def_area_ncorr"].ToString()),
                def_area_ccod = x["def_area_ccod"].ToString(),
                def_area_nombre = x["def_area_nombre"].ToString(),
                def_area_cnombre = x["def_area_cnombre"].ToString(),

                def_tfl_nversion = x.Table.Columns.Contains("def_tfl_nversion") ? x["def_tfl_nversion"].ToString() : null,
                def_tfl_nversion_fefect = x.Table.Columns.Contains("def_tfl_nversion_fefect") ? x["def_tfl_nversion_fefect"].ToString() : null,
                hoy = x.Table.Columns.Contains("hoy") ? x["hoy"].ToString() : null,
                fecha_termino = x.Table.Columns.Contains("fecha_termino") ? x["fecha_termino"].ToString() : null,

                def_tfl_imae = x.GetDataRowValue<string>("def_tfl_imae")
            };
        }

        public RespuestaSP DEF_TFL_SEL
        (
            int? p_def_tfl_ncorr,
            string p_def_tfl_nombre,
            string p_def_tfl_descrip,
            int? p_def_tfl_version,
            int? p_def_tfl_ncualficaciones,
            int? p_def_tfl_nucl,
            string p_def_tfl_fefect,
            string p_def_tfl_estado,
            string p_audi_tusuario,
            int? p_def_pvigencia_tfl_ncorr,
            int? p_def_dir_sec_vra_ncorr,       // (Este es nuevo 4-5-2023)
            int? p_def_area_ncorr,
            int p_cantidad,
            out List<DEF_TFL_ENT> outcur
        )
        {
            outcur = new List<DEF_TFL_ENT>();

            try
            {
                IDataParameter[] param = new
                {
                    p_def_tfl_ncorr,
                    p_def_tfl_nombre,
                    p_def_tfl_descrip,
                    p_def_tfl_version,
                    p_def_tfl_ncualficaciones,
                    p_def_tfl_nucl,
                    p_def_tfl_fefect,
                    p_def_tfl_estado,
                    p_audi_tusuario,
                    p_def_pvigencia_tfl_ncorr,
                    p_def_dir_sec_vra_ncorr,
                    p_def_area_ncorr,
                    p_cantidad
                }.ToDataParameters(true);

                param.AddOutput("outcur", OracleDbType.RefCursor);
                param.AddOutput("p_swt", OracleDbType.Int32);
                param.AddOutput("p_msg", OracleDbType.Varchar2, _msg_Size);
                param.AddOutput("p_sts", OracleDbType.Varchar2, _sts_Size);
                param.AddOutput("p_tbl", OracleDbType.Varchar2, _tbl_Size);
                param.AddOutput("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

                DataTable dt = null;
                ExecuteStoredProcedure("DEF_TFL_PKG.DEF_TFL_SEL", ref param, ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    outcur = (from DataRow x in dt.Rows select ObtenerObjeto(x)).ToList();
                }

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

        public RespuestaSP DEF_TFL_LEE(int p_kya, out DEF_TFL_ENT outcur)
        {
            outcur = new DEF_TFL_ENT();

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
                ExecuteStoredProcedure("DEF_TFL_PKG.def_tfl_lee", ref param, ref dt);

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

        //=======================================================================>>>>>
        // 13-10-2023   |   DEF_TFL_IMAE

        public RespuestaSP DEF_TFL_GRA
        (
            string p_def_tfl_nombre,
            string p_def_tfl_descrip,
            int p_def_tfl_version,
            int p_def_tfl_ncualficaciones,
            int p_def_tfl_nucl,
            string p_def_tfl_fefect,
            string p_def_tfl_estado,
            string p_audi_tusuario,
            int p_def_pvigencia_tfl_ncorr,
            int p_def_area_ncorr,
            string p_def_tfl_imae   // Este es nuevo
        )
        {
            try
            {
                IDataParameter[] param = new
                {
                    p_def_tfl_nombre,
                    p_def_tfl_descrip,
                    p_def_tfl_version,
                    p_def_tfl_ncualficaciones,
                    p_def_tfl_nucl,
                    p_def_tfl_fefect,
                    p_def_tfl_estado,
                    p_audi_tusuario,
                    p_def_pvigencia_tfl_ncorr,
                    p_def_area_ncorr,
                    p_def_tfl_imae
                }.ToDataParameters(false);

                param.AddOutput("p_swt", OracleDbType.Int32);
                param.AddOutput("p_msg", OracleDbType.Varchar2, _msg_Size);
                param.AddOutput("p_sts", OracleDbType.Varchar2, _sts_Size);
                param.AddOutput("p_tbl", OracleDbType.Varchar2, _tbl_Size);
                param.AddOutput("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

                DataTable dt = null;
                ExecuteStoredProcedure("DEF_TFL_PKG.DEF_TFL_GRA", ref param, ref dt);

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

        /*
        public RespuestaSP DEF_TFL_GRA
        (
            string p_def_tfl_nombre,
            string p_def_tfl_descrip,
            int p_def_tfl_version,
            int p_def_tfl_ncualficaciones,
            int p_def_tfl_nucl,
            string p_def_tfl_fefect,
            string p_def_tfl_estado,
            string p_audi_tusuario,
            int p_def_pvigencia_tfl_ncorr,
            int p_def_area_ncorr
        )
        {
            try
            {
                IDataParameter[] param = new IDataParameter[15];

                param[0] = new OracleParameter("p_def_tfl_nombre", OracleDbType.Varchar2);
                param[1] = new OracleParameter("p_def_tfl_descrip", OracleDbType.Varchar2);
                param[2] = new OracleParameter("p_def_tfl_version", OracleDbType.Int32);
                param[3] = new OracleParameter("p_def_tfl_ncualficaciones", OracleDbType.Int32);
                param[4] = new OracleParameter("p_def_tfl_nucl", OracleDbType.Int32);
                param[5] = new OracleParameter("p_def_tfl_fefect", OracleDbType.Varchar2);
                param[6] = new OracleParameter("p_def_tfl_estado", OracleDbType.Varchar2);
                param[7] = new OracleParameter("p_audi_tusuario", OracleDbType.Varchar2);
                param[8] = new OracleParameter("p_def_pvigencia_tfl_ncorr", OracleDbType.Int32);
                param[9] = new OracleParameter("p_def_area_ncorr", OracleDbType.Int32);
                param[10] = new OracleParameter("p_swt", OracleDbType.Int32);
                param[11] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
                param[12] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
                param[13] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
                param[14] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

                param[0].Value = p_def_tfl_nombre;
                param[1].Value = p_def_tfl_descrip;
                param[2].Value = p_def_tfl_version;
                param[3].Value = p_def_tfl_ncualficaciones;
                param[4].Value = p_def_tfl_nucl;
                param[5].Value = p_def_tfl_fefect;
                param[6].Value = p_def_tfl_estado;
                param[7].Value = p_audi_tusuario;
                param[8].Value = p_def_pvigencia_tfl_ncorr;
                param[9].Value = p_def_area_ncorr;
                param[10].Direction = ParameterDirection.Output;
                param[11].Direction = ParameterDirection.Output;
                param[12].Direction = ParameterDirection.Output;
                param[13].Direction = ParameterDirection.Output;
                param[14].Direction = ParameterDirection.Output;

                DataTable dt = null;
                ExecuteStoredProcedure("DEF_TFL_PKG.def_tfl_gra", ref param, ref dt);

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
        */

        public RespuestaSP DEF_TFL_UPD
        (
            int p_kya,
            string p_def_tfl_nombre,
            string p_def_tfl_descrip,
            int p_def_tfl_version,
            int p_def_tfl_ncualficaciones,
            int p_def_tfl_nucl,
            string p_def_tfl_fefect,
            string p_def_tfl_estado,
            string p_audi_tusuario,
            int p_def_pvigencia_tfl_ncorr,
            int p_def_area_ncorr,
            string p_def_tfl_imae   // Este es nuevo
        )
        {
            try
            {
                IDataParameter[] param = new
                {
                    p_kya,
                    p_def_tfl_nombre,
                    p_def_tfl_descrip,
                    p_def_tfl_version,
                    p_def_tfl_ncualficaciones,
                    p_def_tfl_nucl,
                    p_def_tfl_fefect,
                    p_def_tfl_estado,
                    p_audi_tusuario,
                    p_def_pvigencia_tfl_ncorr,
                    p_def_area_ncorr,
                    p_def_tfl_imae
                }.ToDataParameters(false);

                param.AddOutput("p_swt", OracleDbType.Int32);
                param.AddOutput("p_msg", OracleDbType.Varchar2, _msg_Size);
                param.AddOutput("p_sts", OracleDbType.Varchar2, _sts_Size);
                param.AddOutput("p_tbl", OracleDbType.Varchar2, _tbl_Size);
                param.AddOutput("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

                DataTable dt = null;
                ExecuteStoredProcedure("DEF_TFL_PKG.DEF_TFL_UPD", ref param, ref dt);

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

        /*
        public RespuestaSP DEF_TFL_UPD
        (
            int p_kya,
            string p_def_tfl_nombre,
            string p_def_tfl_descrip,
            int p_def_tfl_version,
            int p_def_tfl_ncualficaciones,
            int p_def_tfl_nucl,
            string p_def_tfl_fefect,
            string p_def_tfl_estado,
            string p_audi_tusuario,
            int p_def_pvigencia_tfl_ncorr,
            int p_def_area_ncorr
        )
        {
            try
            {
                IDataParameter[] param = new IDataParameter[16];

                param[0] = new OracleParameter("p_kya", OracleDbType.Int32);
                param[1] = new OracleParameter("p_def_tfl_nombre", OracleDbType.Varchar2);
                param[2] = new OracleParameter("p_def_tfl_descrip", OracleDbType.Varchar2);
                param[3] = new OracleParameter("p_def_tfl_version", OracleDbType.Int32);
                param[4] = new OracleParameter("p_def_tfl_ncualficaciones", OracleDbType.Int32);
                param[5] = new OracleParameter("p_def_tfl_nucl", OracleDbType.Int32);
                param[6] = new OracleParameter("p_def_tfl_fefect", OracleDbType.Varchar2);
                param[7] = new OracleParameter("p_def_tfl_estado", OracleDbType.Varchar2);
                param[8] = new OracleParameter("p_audi_tusuario", OracleDbType.Varchar2);
                param[9] = new OracleParameter("p_def_pvigencia_tfl_ncorr", OracleDbType.Int32);
                param[10] = new OracleParameter("p_def_area_ncorr", OracleDbType.Int32);
                param[11] = new OracleParameter("p_swt", OracleDbType.Int32);
                param[12] = new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size);
                param[13] = new OracleParameter("p_sts", OracleDbType.Varchar2, _sts_Size);
                param[14] = new OracleParameter("p_tbl", OracleDbType.Varchar2, _tbl_Size);
                param[15] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, _pkgp_Size);

                param[0].Value = p_kya;
                param[1].Value = p_def_tfl_nombre;
                param[2].Value = p_def_tfl_descrip;
                param[3].Value = p_def_tfl_version;
                param[4].Value = p_def_tfl_ncualficaciones;
                param[5].Value = p_def_tfl_nucl;
                param[6].Value = p_def_tfl_fefect;
                param[7].Value = p_def_tfl_estado;
                param[8].Value = p_audi_tusuario;
                param[9].Value = p_def_pvigencia_tfl_ncorr;
                param[10].Value = p_def_area_ncorr;
                param[11].Direction = ParameterDirection.Output;
                param[12].Direction = ParameterDirection.Output;
                param[13].Direction = ParameterDirection.Output;
                param[14].Direction = ParameterDirection.Output;
                param[15].Direction = ParameterDirection.Output;

                DataTable dt = null;
                ExecuteStoredProcedure("DEF_TFL_PKG.def_tfl_upd", ref param, ref dt);

                return new RespuestaSP()
                {
                    swt = int.Parse(param[11].Value.ToString()),
                    msg = param[12].Value.ToString(),
                    sts = param[13].Value.ToString(),
                    tbl = param[14].Value.ToString(),
                    pkgp = param[15].Value.ToString(),
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        */

        public RespuestaSP DEF_TFL_DEL(int p_kya, string p_audi_tusuario)
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
                ExecuteStoredProcedure("DEF_TFL_PKG.def_tfl_del", ref param, ref dt);

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

        public RespuestaSP DEF_TFL_ENTRADAS(out List<ENTRADAS_ENT> lista)
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
                ExecuteStoredProcedure("DEF_TFL_PKG.def_tfl_entradas", ref param, ref dt);

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

        //================================================================================>>>>>

        public RespuestaSP GetDatosDefTFL(
            int p_kya,
            out string def_tfl_nombre,
            out string def_tfl_descrip,
            out string def_tfl_version,
            out string def_tfl_fefect
        )
        {
            def_tfl_nombre = string.Empty;
            def_tfl_descrip = string.Empty;
            def_tfl_version = string.Empty;
            def_tfl_fefect = string.Empty;

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
                ExecuteStoredProcedure("DEF_TFL_PKG.DEF_TFL_LEE", ref param, ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    def_tfl_nombre = dt.Rows[0]["def_tfl_nombre"].ToString();
                    def_tfl_descrip = dt.Rows[0]["def_tfl_descrip"].ToString();
                    def_tfl_version = dt.Rows[0]["def_tfl_version"].ToString();
                    def_tfl_fefect = dt.Rows[0]["def_tfl_fefect"].ToString();
                }

                return new RespuestaSP()
                {
                    swt = int.Parse(param[2].Value.ToString()),
                    msg = param[3].Value.ToString(),
                    sts = param[4].Value.ToString(),
                    tbl = param[5].Value.ToString(),
                    pkgp = param[6].Value.ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RespuestaSP GetCombobox(int def_area_ncorr, int def_pvigencia_tfl_ncorr, out List<COMBOBOX_ENT> salida)
        {
            salida = new List<COMBOBOX_ENT>();

            try
            {
                RespuestaSP resSP = this.DEF_TFL_SEL(
                    null,                           // p_def_tfl_ncorr 
                    null,                           // p_def_tfl_nombre 
                    null,                           // p_def_tfl_descrip 
                    null,                           // p_def_tfl_version 
                    null,                           // p_def_tfl_ncualficaciones 
                    null,                           // p_def_tfl_nucl 
                    null,                           // p_def_tfl_fefect 
                    null,                           // p_def_tfl_estado 
                    null,                           // p_audi_tusuario 
                    def_pvigencia_tfl_ncorr,        // p_def_pvigencia_tfl_ncorr 
                    null,                           // p_def_dir_sec_vra_ncorr    (Este es nuevo 4-5-2023)
                    def_area_ncorr,                 // p_def_area_ncorr 
                    9998,                           // p_cantidad 
                    out List<DEF_TFL_ENT> lista
                );

                if (resSP.swt == 0 || resSP.swt == 1)
                {
                    salida = lista.Select(x => new COMBOBOX_ENT
                    {
                        codigo = x.def_tfl_ncorr.ToString(),
                        descripcion = x.def_tfl_nversion
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
