using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TFL_x_WEB.Helpers
{
    public static class ExtensionHelper
    {
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