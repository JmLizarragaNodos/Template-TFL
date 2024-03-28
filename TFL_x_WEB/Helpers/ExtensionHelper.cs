using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TFL_x_WEB.Helpers
{
    public static class ExtensionHelper
    {
        public static List<T> ToListDistinct<T>(this DataTable dt, string campo)
        {
            var lista = new List<T>();

            if (dt.Columns.Contains(campo))
            {
                lista = dt.AsEnumerable()
                    .Select(x => x[campo] != DBNull.Value ? (T)Convert.ChangeType(x[campo], typeof(T)) : default(T))
                    .Distinct()
                    .ToList();
            }

            return lista;
        }

        public static DataTable FindByField(this DataTable dt, string nombreCampo, object valorCampo)
        {
            var filasFiltradas = dt.AsEnumerable()
                .Where(x => x[nombreCampo] != DBNull.Value && x[nombreCampo].Equals(valorCampo))
                .CopyToDataTable();

            return filasFiltradas;
        }

        public static T GetDataRowValue<T>(this DataRow x, string nombreColumna)
        {
            try
            {
                if (x.Table.Columns.Contains(nombreColumna))
                {
                    if (typeof(T) == typeof(int))
                    {
                        var valor = x[nombreColumna].ToString();
                        int res = (valor != string.Empty) ? int.Parse(valor) : 0;
                        return (T)(object)res;
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        string res = x[nombreColumna].ToString();
                        return (T)(object)res;
                    }
                    else
                    {
                        return x.Field<T>(nombreColumna);
                    }
                }
                else
                {
                    if (typeof(T) == typeof(int))
                    {
                        return (T)(object)0;
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        return default(T);
                    }
                    else
                    {
                        throw new ArgumentException("La columna especificada no existe en el DataRow.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static object ToObject(this DataTable dt)
        {
            var lista = new List<object>();

            foreach (DataRow row in dt.Rows)
            {
                var objeto = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    objeto[col.ColumnName.ToLower()] = row[col];
                }

                lista.Add(objeto);
            }

            if (lista.Count > 0)
                return lista.FirstOrDefault();

            return null;
        }

        public static List<object> ToObjectList(this DataTable dt)
        {
            var lista = new List<object>();

            foreach (DataRow row in dt.Rows)
            {
                var objeto = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    objeto[col.ColumnName.ToLower()] = row[col];
                }

                lista.Add(objeto);
            }

            return lista;
        }
    }
}