using MCTP_a_Exception;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Inacap.LogginException
{
    public static class LogException  
    {
        public delegate void WriteToEventLogDB(string description, string message);

        public static void WriteToEventLog(string message, WriteToEventLogDB writeToEventLogDB = null)
        {
            try
            {
                _writeToEventLog(message, EventLogEntryType.Information, writeToEventLogDB);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        
        public static string WriteToEventLog(Exception ex, WriteToEventLogDB writeToEventLogDB = null)
        {
            if (ex != null)
            {
                StackFrame stackFrame = (new StackTrace(ex, true)).GetFrame(0);

                return "<b>Origen:</b> <br/>" +
                $"{stackFrame.GetMethod()} en la línea {stackFrame.GetFileLineNumber()} <br/><br/>" +

                $"<b>Mensaje:</b> <br/>" +
                $"{ex.Message} <br/>";
            }

            return "Error desconocido";
        }
        
        private static void _writeToEventLog(string message, EventLogEntryType eventLogEntryType, WriteToEventLogDB writeToEventLogDB = null)
        {
            try
            {
                int code = 101;
                string cs = ConfigurationManager.AppSettings["KEYLOG"];
                if (string.IsNullOrEmpty(cs))
                {
                    cs = ConfigurationManager.AppSettings["APLI_CAPLICACION"];
                }
                if (!string.IsNullOrEmpty(cs))
                {
                    if (writeToEventLogDB == null)
                    {
                        EventLog elog = new EventLog();
                        if (!EventLog.SourceExists(cs))
                        {
                            EventLog.CreateEventSource(cs, cs);
                        }
                        elog.Source = cs;
                        elog.EnableRaisingEvents = true;
                        elog.WriteEntry(message, eventLogEntryType, code);
                        elog = null;
                    }
                    else
                    {
                        writeToEventLogDB(cs, message);
                    }
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message, exc.InnerException);
            }
        }

        public static string LogException_pkg
        (
            int swt,        // Es un switch. Resultados:  0 (Encontró el registro) | 1 (No encontró el registro) | 2 (Encontró un error)
            string msg,     // Mensaje
            string sts,     // Status
            string tbl,     // Trae el nombre de la tabla
            string pkgp,    // Trae el PKG que se usó para leer esa tabla 
            string p_kya = null     // Valor de la llave que se pidió leer
        )
        {
            string res = $"<b>Mensaje:</b> <br/>" +
            $"{msg} <br/><br/>";

            if (!string.IsNullOrEmpty(sts))
            {
                res += $"<b>Status:</b> <br/>" +
                $"{sts} <br/><br/>";
            }

            if (!string.IsNullOrEmpty(p_kya))
            {
                res += $"<b>Llave:</b> <br/>" +
                $"{p_kya} <br/><br/>";
            }

            res += $"<b>Tabla:</b> <br/>" +
            $"{tbl} <br/><br/>";

            res += $"<b>Origen:</b> <br/>" +
            $"{pkgp} <br/>";

            return res;
        }

        public static bool EsErrorDeCambioPackage(Exception ex)
        {
            /*
            ORA-04068: se ha anulado el estado existente de los paquetes 
            ORA-04061: el estado existente de package "TFL.CUALIFICACIONES_PKG" ha sido invalidado
            ORA-04065: package "TFL.CUALIFICACIONES_PKG" no se ha ejecutado porque se ha modificado o borrado
            ORA-06508: PL/SQL: no se ha encontrado la unidad de programa llamada : "TFL.CUALIFICACIONES_PKG"
            ORA-06512: en línea 1 
            */

            if (ex != null && !string.IsNullOrEmpty(ex.Message))
            {
                string msn = ex.Message;

                if (msn.Contains("ORA-04068") && msn.Contains("ORA-04061") && msn.Contains("ORA-04065"))
                    return true;
            }

            return false;
        }

    }
}

