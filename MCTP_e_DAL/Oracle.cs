using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Inacap.Common.DAL;
using Oracle.DataAccess.Client;

namespace Inacap.Common.Dal
{
    public class Oracle : DataGenericReader
    {
        private string _stringAccess { get; set; }

        public Oracle() { }

        public Oracle(string key)
        {
            _stringAccess = key;
        }

        protected string ObtenerCadena(string nameStoredProcedure, IDataParameter[] param)
        {
            // string cadena = ObtenerCadena("DEF_TNMCTP_PKG.def_tnmctp_sel", param); 

            Func<IDataParameter, string> keyValue = (x) =>
            {
                string valor = "NULL";

                if (x.Value != null)
                {
                    if (x.Value is string)
                        valor = $"'{x.Value.ToString()}'";
                    else
                        valor = x.Value.ToString();
                }

                if (valor == "") valor = "''";
                if (x.ParameterName.ToLower().Contains("swt")) valor = "swt";
                if (x.ParameterName.ToLower().Contains("msg")) valor = "msg";
                if (x.ParameterName.ToLower().Contains("sts")) valor = "sts";
                if (x.ParameterName.ToLower().Contains("tbl")) valor = "tbl";
                if (x.ParameterName.ToLower().Contains("pkgp")) valor = "pkgp";
                if (x.ParameterName.ToLower().Contains("outcur")) valor = "outcur";

                return $"        {x.ParameterName} => {valor}";
            };

            string cadena = "";
            cadena += "SET SERVEROUTPUT ON; \n\n";

            cadena += "DECLARE \n";
            cadena += "    swt number; \n";
            cadena += "    msg varchar2(200); \n";
            cadena += "    sts varchar2(200); \n";
            cadena += "    tbl varchar2(200); \n";
            cadena += "    pkgp varchar2(200); \n";

            if (param.Any(x => x.ParameterName.ToLower().Contains("outcur")))
                cadena += "    outcur SYS_REFCURSOR; \n";

            cadena += "BEGIN \n";
            cadena += $"    {nameStoredProcedure}( \n";
            cadena += string.Join(", \n", param.Select(x => keyValue(x)));

            cadena += "\n";
            cadena += "    ); \n";

            if (param.Any(x => x.ParameterName.ToLower().Contains("outcur")))
            {
                cadena += " \n";
                cadena += "    IF outcur IS NOT NULL THEN \n";
                cadena += "        DBMS_SQL.RETURN_RESULT(outcur); \n";
                cadena += "    ELSE \n";
                cadena += "        dbms_output.put_line('El cursor outcur no trae información'); \n";
                cadena += "        dbms_output.put_line('swt: ' || swt); \n";
                cadena += "        dbms_output.put_line('msg: ' || msg); \n";
                cadena += "        dbms_output.put_line('sts: ' || sts); \n";
                cadena += "        dbms_output.put_line('tbl: ' || tbl); \n";
                cadena += "        dbms_output.put_line('pkgp: ' || pkgp); \n";
                cadena += "    END IF; \n";
            }

            cadena += "END; \n";

            return cadena;
        }

        public void ExecuteStoredProcedure(string NameStoredProcedure)
        {
            try
            {
                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(NameStoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void ExecuteStoredProcedure(string NameStoredProcedure, ref IDataParameter[] Params)
        {
            try
            {
                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(NameStoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    TransferParameters(cmd, Params);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public List<T> _ExecuteStoredProcedure<T>(string NameStoredProcedure, ref IDataParameter[] Params)
        {
            try
            {
                List<T> lstObjeto;
                using (OracleConnection conn = GetConexion())
                {
                    var cmd = new OracleCommand(NameStoredProcedure, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    TransferParameters(cmd, Params);
                    conn.Open();
                    OracleDataReader DataResult = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    //lstObjeto = ListaObjeto(DataResult);

                    //=============================>>>

                    //T objeto = new T();
                    //lstObjeto = PopulateObjectsFromReader(objeto, DataResult);

                    List<T> list = new List<T>();
                    while (DataResult.Read())
                    {
                        T objeto = Activator.CreateInstance<T>();
                        PopulateObjectFromReader(ref objeto, DataResult);
                        list.Add(objeto);
                    }
                    DataResult.Close();
                    DataResult.Dispose();

                    lstObjeto = list;

                    //=============================>>>
                }

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> PopulateObjectsFromReader<T>(T obj, OracleDataReader rdr)
        {
            List<T> list = new List<T>();
            while (rdr.Read())
            {
                T objeto = Activator.CreateInstance<T>();
                PopulateObjectFromReader(ref objeto, rdr);
                list.Add(objeto);
            }
            rdr.Close();
            rdr.Dispose();

            return list;
        }

        //public List<T> ListaObjeto<T>(OracleDataReader odr) where T : new()
        //{
        //    T objeto = new T();
        //    var lstObjeto = PopulateObjectsFromReader(objeto, odr);

        //    return lstObjeto;
        //}


        public void ExecuteStoredProcedure(string NameStoredProcedure, ref IDataParameter[] Params, ref DataTable DataResult)
        {
            try
            {
                DataResult = new DataTable();

                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(NameStoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    TransferParameters(cmd, Params);
                    var DataAdap = new OracleDataAdapter(cmd);

                    conn.Open();
                    DataAdap.Fill(DataResult);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void ExecuteStoredProcedure1(string NameStoredProcedure, ref IDataParameter[] Params, ref DataTable DataResult)
        {
            try
            {
                DataResult = new DataTable();

                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(NameStoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    TransferParameters(cmd, Params);
                    var DataAdap = new OracleDataAdapter(cmd);

                    conn.Open();
                    DataAdap.Fill(DataResult);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void ExecuteSQL(string SQLString)
        {
            try
            {
                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(SQLString, conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void ExecuteSQL(string SQLString, ref DataTable DataResult)
        {
            try
            {
                DataResult = new DataTable();

                using (OracleConnection conn = GetConexion())
                {
                    var DataAdap = new OracleDataAdapter(SQLString, conn);

                    conn.Open();
                    DataAdap.Fill(DataResult);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void ExecuteSQL(string SQLString, ref IDataParameter[] Params)
        {
            try
            {
                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(SQLString, conn);
                    cmd.CommandType = CommandType.Text;
                    TransferParameters(cmd, Params);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void ExecuteSQL(string SQLString, ref IDataParameter[] Params, ref DataTable DataResult)
        {
            try
            {
                DataResult = new DataTable();

                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(SQLString, conn);
                    TransferParameters(cmd, Params);
                    var DataAdap = new OracleDataAdapter(cmd);

                    conn.Open();
                    DataAdap.Fill(DataResult);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #region Privados
        private string StringConexion
        {
            get
            {
                if (string.IsNullOrEmpty(_stringAccess))
                    return ConfigurationManager.AppSettings["AKORACLE"];
                else
                    return ConfigurationManager.AppSettings[_stringAccess];
            }
        }
        private string GetStringConexion()
        {
            string NameKey = StringConexion;
            //TODO: Cambiar mensaje de error 1
            if (NameKey == null) throw new Exception("Clave de conexión no encontrada (NULL), revisar ubicación de string de conexión.");
            string StrConn = ConfigurationManager.AppSettings[NameKey];
            if (StrConn == null) throw new Exception("Clave de conexión no encontrada (NULL), revisar ubicación de string de conexión.");
            return StrConn;
        }
        private OracleConnection GetConexion()
        {
            string str = GetStringConexion();
            OracleConnection conn = new OracleConnection(str);

            return conn;
        }
        private void TransferParameters(OracleCommand cm, IDataParameter[] Params)
        {
            for (int i = 0; i < Params.Length; i++)
            {
                if (((OracleParameter)Params[i]).OracleDbType == OracleDbType.RefCursor)
                {
                    Params[i].Direction = ParameterDirection.Output;
                }

                cm.Parameters.Add(Params[i]);
            }
        }
        private void WriteToEventLog(string message)
        {
            string cs = ConfigurationManager.AppSettings["KEYLOG"];

            if (!string.IsNullOrEmpty(cs))
            {
                try
                {
                    EventLog elog = new EventLog();

                    if (!EventLog.SourceExists(cs))
                    {
                        EventLog.CreateEventSource(cs, cs);
                    }

                    elog.Source = cs;
                    elog.EnableRaisingEvents = true;
                    elog.WriteEntry(message, EventLogEntryType.Error, 7637);
                }
                catch { }
            }
        }
        #endregion
    }
}



