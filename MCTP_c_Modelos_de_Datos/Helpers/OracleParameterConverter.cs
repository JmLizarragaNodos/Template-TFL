using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Oracle.DataAccess.Client;

namespace Inacap.Common.Helpers.Datos
{
    //public class OutputParameter   // ACA
    //{
    //    public string parameterName { get; set; }
    //    public OracleDbType type { get; set; }
    //    public int size { get; set; }
    //}

    public static class HelperExtensions
    {
        /// <summary>
        /// Convierte las propiedades de un objeto en una colección de IDataParameter.
        /// </summary>
        /// <example>
        /// // Objeto anónimo:
        /// var param = new { p_nombre = "Pablo", p_edad = 25 }.ToDataParameters();
        /// var param = new { pNombre = "Pablo", pEdad = 25 }.ToDataParameters();
        ///
        /// // Es equivalente a:
        /// var param = new IDataParameter[]
        /// {
        ///     new OracleParameter
        ///     {
        ///         ParameterName = "p_nombre",
        ///         OracleDbType = OracleDbType.Varchar2,
        ///         Value = "Pablo"
        ///     },
        ///     new OracleParameter
        ///     {
        ///         ParameterName = "p_edad",
        ///         OracleDbType = OracleDbType.Int32,
        ///         Value = 25
        ///     }
        /// };
        /// </example>
        /// <param name="container">Objeto a convertir en IDataParameter[]</param>
        /// <param name="includeCursor">Indica si se incluye un OracleParameter de tipo "RefCursor" con nombre "o_cursor".</param>
        public static IDataParameter[] ToDataParameters<T>(this T container, bool includeCursor = false)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            return OracleParameterConverter<T>.ToDataParameters(container, includeCursor);
        }

        // new OracleParameter("p_msg", OracleDbType.Varchar2, _msg_Size),
        // public OracleParameter(string parameterName, OracleDbType type, int size);

        public static void AddOutput(this IDataParameter[] param, string parameterName, OracleDbType type, int size = 0)
        {
            int index = param.ToList().FindIndex(x => x == null);

            if (size == 0)
                param[index] = new OracleParameter(parameterName, type, null, ParameterDirection.Output);
            else
                param[index] = new OracleParameter(parameterName, type, size, null, ParameterDirection.Output);
        }

        public static void AddOutputParameters(this IDataParameter[] param, List<OracleParameter> lista)
        {
            // int index = param.ToList().FindIndex(x => x.Value != null) + 2;  // + 1;
            int index = param.ToList().FindIndex(x => x == null);

            //int index = 0;
            //param.Where(x => x != null && x.Direction == ParameterDirection.Input).ToList().ForEach(x => index ++);

            foreach (OracleParameter x in lista)
            {
                param[index++] = new OracleParameter(x.ParameterName, x.OracleDbType, x.Size, null, ParameterDirection.Output);
            }

            /*
            // Funciona
            param[index++] = new OracleParameter("outcur", OracleDbType.RefCursor, null, ParameterDirection.Output);
            param[index++] = new OracleParameter("p_swt", OracleDbType.Int32, 4000, null, ParameterDirection.Output);
            param[index++] = new OracleParameter("p_msg", OracleDbType.Varchar2, 4000, null, ParameterDirection.Output);
            param[index++] = new OracleParameter("p_sts", OracleDbType.Varchar2, 4000, null, ParameterDirection.Output);
            param[index++] = new OracleParameter("p_tbl", OracleDbType.Varchar2, 100, null, ParameterDirection.Output);
            param[index++] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, 100, null, ParameterDirection.Output);
            */

            /*
            param[12] = new OracleParameter("outcur", OracleDbType.RefCursor, null, ParameterDirection.Output);
            param[13] = new OracleParameter("p_swt", OracleDbType.Int32, 4000, null, ParameterDirection.Output);
            param[14] = new OracleParameter("p_msg", OracleDbType.Varchar2, 4000, null, ParameterDirection.Output);
            param[15] = new OracleParameter("p_sts", OracleDbType.Varchar2, 4000, null, ParameterDirection.Output);
            param[16] = new OracleParameter("p_tbl", OracleDbType.Varchar2, 100, null, ParameterDirection.Output);
            param[17] = new OracleParameter("p_pkgp", OracleDbType.Varchar2, 100, null, ParameterDirection.Output);
            */

            //return param;
        }

        public static int ToIntValue(this IDataParameter[] param, string parameterName)
        {
            object value = param.FirstOrDefault(x => x.ParameterName == parameterName).Value;
            return int.Parse(value.ToString());
        }

        public static string ToStringValue(this IDataParameter[] param, string parameterName)
        {
            object value = param.FirstOrDefault(x => x.ParameterName == parameterName).Value;
            return value.ToString();
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

        private static class OracleParameterConverter<T>
        {
            // Lista de lambdas que convierten 1 de las propiedades de la clase en su respectivo IDataParameter.
            private static List<Func<T, IDataParameter>> converters;

            /// <summary>
            /// Constructor estático.
            /// La operación de reflection se realiza solo al inicializar la clase por primera vez.
            /// </summary>
            static OracleParameterConverter()
            {
                converters = new List<Func<T, IDataParameter>>();
                var dbTypes = new Dictionary<Type, OracleDbType>
                {
                    { typeof(int), OracleDbType.Int32 },
                    { typeof(int?), OracleDbType.Int32 },
                    { typeof(long), OracleDbType.Int64 },
                    { typeof(long?), OracleDbType.Int64 },
                    { typeof(decimal), OracleDbType.Decimal },
                    { typeof(decimal?), OracleDbType.Decimal },
                    { typeof(DateTime), OracleDbType.Date },
                    { typeof(DateTime?), OracleDbType.Date },
                    { typeof(string), OracleDbType.Varchar2 },
                    { typeof(bool), OracleDbType.Int32 },
                    { typeof(bool?),OracleDbType.Int32 },
                    { typeof(byte[]), OracleDbType.Blob }
                };

                // El objetivo es crear un lambda para cada propiedad de la clase, que convierta el valor
                // de la propiedad en un OracleParameter.
                // Por ejemplo, para la propiedad item.Nombre, de tipo string, se crea el lambda:
                // (t) => new OracleParameter("nombre", OracleDbType.Varchar2, t.Nombre, ParameterDirection.Input)
                foreach (var propertyInfo in typeof(T).GetProperties())
                {
                    // Para los objetos de tipo array (int[], string[]) obtenemos el tipo real
                    var propertyType = (propertyInfo.PropertyType.IsArray && propertyInfo.PropertyType != typeof(byte[]))
                        ? propertyInfo.PropertyType.GetElementType()
                        : propertyInfo.PropertyType;
                    var underlyingType = Nullable.GetUnderlyingType(propertyType);
                    bool isEnum = propertyType.IsEnum || (underlyingType != null && underlyingType.IsEnum);
                    bool isBoolean = propertyInfo.PropertyType == (typeof(bool));
                    bool isString = propertyInfo.PropertyType == (typeof(string));
                    if (!dbTypes.ContainsKey(propertyType) && !isEnum)
                        throw new ArgumentException(
                            String.Format("Propiedad {0} de tipo {1} no tiene conversión a Oracle.", propertyInfo.Name,
                                propertyType));

                    var parameterExpression = Expression.Parameter(typeof(T), "item");
                    Expression valueExpression = Expression.Property(parameterExpression, propertyInfo);
                    // Si es un enum, casteamos a int?
                    if (isEnum)
                        valueExpression = Expression.Convert(valueExpression, typeof(int?));
                    // Los bools los convertimos a int
                    else if (isBoolean)
                    {
                        valueExpression = Expression.Condition(
                            Expression.Equal(Expression.Constant(true), valueExpression),
                            Expression.Constant(1),
                            Expression.Constant(0)
                            );
                    }
                    //A los strings hay que sacarles el \r
                    else if (isString)
                    {
                        valueExpression = Expression.Call(null, typeof(HelperExtensions.OracleParameterConverter<T>).GetMethod("RemoveBackSlashR"), valueExpression);
                    }
                    // Hay que hacer explicito el boxing para tipos por valor
                    else if (propertyInfo.PropertyType.IsValueType)
                        valueExpression = Expression.Convert(valueExpression, typeof(object));

                    // Constructor:
                    NewExpression newExpression;
                    if (isEnum || isBoolean)
                    {
                        // new OracleParameter(string parameterName, OracleDbType type, object obj, ParameterDirection direction)
                        newExpression = Expression.New(
                            typeof(OracleParameter).GetConstructor(
                                new[] { typeof(string), typeof(OracleDbType), typeof(object), typeof(ParameterDirection) }),
                            Expression.Constant(CamelCaseToUnderscore(propertyInfo.Name)), // parameterName
                            Expression.Constant(dbTypes[typeof(int)]), // OracleDbType
                            Expression.Convert(valueExpression, typeof(object)), // Pasamos el int a object
                            Expression.Constant(ParameterDirection.Input) // ParameterDirection
                            );
                    }
                    else
                    {
                        newExpression = Expression.New(
                            typeof(OracleParameter).GetConstructor(
                                new[] { typeof(string), typeof(OracleDbType), typeof(object), typeof(ParameterDirection) }),
                            Expression.Constant(CamelCaseToUnderscore(propertyInfo.Name)), // parameterName
                            Expression.Constant(dbTypes[propertyType]), // OracleDbType.Int32
                            valueExpression, // obj
                            Expression.Constant(ParameterDirection.Input) // ParameterDirection
                            );
                    }

                    // Si el objeto es un array, seteamos las propiedades:
                    // { CollectionType = OracleCollectionType.PLSQLAssociativeArray, Size = item.Length }
                    Expression initExpression;
                    if (propertyInfo.PropertyType.IsArray && propertyInfo.PropertyType != typeof(byte[]))
                    {
                        var collectionTypeBinding = Expression.Bind(typeof(OracleParameter).GetMember("CollectionType")[0],
                            Expression.Constant(OracleCollectionType.PLSQLAssociativeArray));
                        var sizeBinding = Expression.Bind(typeof(OracleParameter).GetMember("Size")[0],
                            Expression.ArrayLength(valueExpression));

                        initExpression = Expression.MemberInit(newExpression,
                            collectionTypeBinding, sizeBinding);
                    }
                    else
                        initExpression = newExpression;

                    var lamda = Expression.Lambda<Func<T, IDataParameter>>(initExpression, parameterExpression).Compile();
                    converters.Add(lamda);
                }
            }

            /// <summary>
            /// Convierte string CamelCase a underscore_case.
            /// </summary>
            private static string CamelCaseToUnderscore(string str)
            {
                if (str.Contains('_'))
                    return str;

                return
                    string.Concat(
                        str.Select(
                            (x, i) =>
                                i > 0 && char.IsUpper(x) ? "_" + char.ToLower(x).ToString() : char.ToLower(x).ToString()));
            }

            public static string RemoveBackSlashR(string str)
            {
                return str != null ? str.Replace("\r\n", "\n") : str;
            }

            /// <summary>
            /// Aplica todos los convertidores sobre el objeto, para crear la lista de IDataParameter.
            /// </summary>
            internal static IDataParameter[] ToDataParameters(T item, bool includeCursor)
            {
                int cantidadPosiciones = converters.Count + (includeCursor ? 1 : 0);
                cantidadPosiciones = cantidadPosiciones + 5;    // Solo para TFL. Todos los SP tienen 5 outputs

                var param = new IDataParameter[cantidadPosiciones];

                for (var i = 0; i < converters.Count; i++)
                    param[i] = converters[i](item);

                //if (includeCursor)
                //    param[converters.Count] = new OracleParameter("o_cursor", OracleDbType.RefCursor);

                return param;
            }
        }
    }
}
