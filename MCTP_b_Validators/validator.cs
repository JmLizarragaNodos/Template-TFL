using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Owasp.Esapi.ValidationRules;
using Owasp.Esapi.Interfaces;
using Owasp.Esapi;
using System.Dynamic;


namespace Inacap.Validators
{
    public static class validator
    {
        public enum typeValidator
        {
            valTypeNombreApellido = 0,
            valTypeUserName = 1,
            valTypeRut = 2,
            valTypeNumber = 3,
            valTypeDate = 4,
            valTypeDescriptors = 5,
            valTypePaths = 6,
            valTypeUrlParameters = 7,
            valTypesUrl = 8,
            valTypesNemoTecnics = 9,
            valTypesBoolean = 10,
            valTypesDns = 11,
            valTyvalTypeDescriptions = 12,
            valreCAPTCHA = 13,
            valTypesFileName = 14,
            valTypesHour = 15,
            valTypesJson = 16,
            //valTypeDescriptors_TFL = 17   // Creada para el proyecto TFL.MCTP
        }

        public enum typeRequired
        {
            valTypeRequired = 0,
            valTypeUnRequired = 1
        }

        public static OutValidator validaExpr(typeValidator typeVal, string value, typeRequired typeReq, int maxLen = 0, int minLen = 0, string textControl = null, string paramName = null)
        {
            OutValidator varOut = new OutValidator();
            string pattern = "";
            string length = "";
            string reCAPTCHAPrivateKey = string.Empty;
            bool _res_rExpr = false;
            bool _res_rExprLenght = false;
            ValidatorCaseErrorLength _objCaseErrorLength = new ValidatorCaseErrorLength();
            IValidator validator = Esapi.Validator;
            try
            {
                try
                {
                    reCAPTCHAPrivateKey = ConfigurationManager.AppSettings["reCAPTCHAPrivateKey"].ToString();
                }
                catch
                {
                    reCAPTCHAPrivateKey = string.Empty;
                }
                if ((maxLen > 0) && (minLen > 0))
                {
                    length = "^.{" + minLen + "," + maxLen + "}$";
                    _objCaseErrorLength.Min = minLen;
                    _objCaseErrorLength.Max = maxLen;
                }
                else
                {
                    if ((maxLen > 0) && (minLen == 0))
                    {
                        length = "^.{1," + maxLen + "}$";
                        _objCaseErrorLength.Max = maxLen;
                    }
                    else
                    {
                        if ((maxLen == 0) && (minLen > 0))
                        {
                            length = "^.{" + minLen + ",30}$";
                            _objCaseErrorLength.Min = minLen;
                        }
                    }
                }

                switch ((int)typeVal)
                {
                    case 0:
                        pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú ]+$";
                        break;
                    case 1:
                        pattern = "^[a-zA-Z0-9-_.]+$";
                        break;
                    case 2:
                        if (typeRequired.valTypeRequired == typeReq || value.Trim().Length > 0)
                        {
                            varOut.OutMethod = isValidRut(value.Trim());
                            varOut.OutMessage = "El RUT ingresado no es válido";
                            varOut.ParamName = paramName;
                            varOut.ParamValue = value;
                            return varOut;
                        }
                        break;
                    case 3:
                        pattern = "^[0-9]+$";
                        break;
                    case 4:
                        pattern = "^[0-9]{2}[/]{1}[0-9]{2}[/]{1}[0-9]{4}$";
                        break;
                    case 5:
                        pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_. ]$";
                        break;
                    case 6:
                        pattern = "^[A-Za-z0-9-_./]";
                        break;
                    case 7:
                        pattern = "^[(&|?|)][A-Za-z0-9-_]";
                        break;
                    case 8:
                        pattern = "^(http|https)://[^ \"]";
                        break;
                    case 9:
                        pattern = "^[A-Z0-9]+$";
                        break;
                    case 10:
                        pattern = "^(?:tru|fals)e$";
                        break;
                    case 11:
                        pattern = "^(?![0-9]+$)(?!.*-$)(?!-)[a-zA-Z0-9-]";
                        break;
                    case 12:

                        // patter-zñáéíóú0-9-_.,;!¡¿?:()@$%\r\n ]+$";         // Original

                        //=================================>>>>>
                        // Acepta tabulados con iconos de Word
                        // pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,/&*=#;!¡¿?:()@$%+•°\r\n\t ]+$";   // El caracter ' no lo acepta (Por seguridad)

                        //=================================>>>>>

                        pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,/&*=#;!¡¿?:()@$%+•°\r\n\t ]+$";

                        //=================================>>>>>

                        // pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,';!¡¿?:()@$%\r\n ]+$";    // Acepta el '
                        // pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,*;!¡¿?:()@$%\r\n ]+$";    // Acepta el *
                        // pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,=;!¡¿?:()@$%\r\n ]+$";    // Acepta el =  
                        // pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,&;!¡¿?:()@$%\r\n ]+$";    // Acepta el &
                        // pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,/;!¡¿?:()@$%\r\n ]+$";    // Acepta el /
                        // pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,#;!¡¿?:()@$%\r\n ]+$";    // Acepta el #
                        // El carácter \t representa un tabulador en las cadenas de texto.

                        break;
                    case 13:
                        varOut.OutMethod = _IsReCaptchValid(value);
                        varOut.OutMessage = "El Validación de Captcha no es correcta";
                        varOut.ParamName = paramName;
                        varOut.ParamValue = value;
                        return varOut;
                    case 14:
                        if (value.Length >= 5 && value.Length <= 255)
                        {
                            varOut.OutMethod = (value.IndexOfAny(Path.GetInvalidFileNameChars()) != -1) ? false : true;
                            varOut.OutMessage = "El nombre de archivo contiene caracteres inválidos";
                        }
                        else
                        {
                            varOut.OutMethod = false;
                            varOut.OutMessage = "El largo del nombre de archivo no es válido";
                        }
                        varOut.ParamName = paramName;
                        varOut.ParamValue = value;
                        return varOut;
                    case 15:
                        pattern = "^(((([0-1][0-9])|(2[0-3])):?[0-5][0-9])|(24:?00))";
                        break;
                    case 16:
                        varOut.OutMethod = _IsJson(value);
                        varOut.OutMessage = "El formato del valor Json no es válido";
                        varOut.ParamName = paramName;
                        varOut.ParamValue = value;
                        return varOut;

                    //case 17:  // Creada para el proyecto TFL.MCTP

                    //    pattern = "^[A-ZÑÁÉÍÓÚa-zñáéíóú0-9-_.,/&*=#;!¡¿?:()@$%\r\n ]+$";   // Acepta los caracteres    / & * # =
                    //                                                                       // El caracter ' no lo acepta
                    //    break;

                    default:
                        varOut.OutMethod = false;
                        varOut.OutMessage = "No se encontró el método de validación";
                        varOut.ParamName = paramName;
                        varOut.ParamValue = value;
                        return varOut;
                }
                if (!string.IsNullOrWhiteSpace(pattern))
                {
                    if (validator.GetRule(typeVal.ToString()) == null)
                    {
                        try { validator.AddRule(typeVal.ToString(), new RegexValidationRule(pattern)); } catch { }
                    }
                }
                //Regex rExpr = new Regex(@pattern, RegexOptions.Singleline);
                Regex rExprLenght = new Regex(@length, RegexOptions.Singleline);
                //_res_rExpr = rExpr.IsMatch(value);
                _res_rExpr = (string.IsNullOrWhiteSpace(pattern)) ? true : validator.IsValid(typeVal.ToString(), (string.IsNullOrWhiteSpace(value)) ? "" : value);
                _res_rExprLenght = rExprLenght.IsMatch((string.IsNullOrWhiteSpace(value)) ? "" : value);
                if (!_res_rExprLenght)
                {
                    if (_objCaseErrorLength.Min != 0 && _objCaseErrorLength.Max != 0)
                    {
                        varOut.OutMessage = "El largo requerido " + ((textControl != null) ? "para el control " + textControl : "") + " debe estar entre " + _objCaseErrorLength.Min + " y " + _objCaseErrorLength.Max + " caracteres.";
                    }
                    else
                    {
                        if (_objCaseErrorLength.Min != 0 && _objCaseErrorLength.Max == 0)
                        {
                            varOut.OutMessage = "El largo mínimo " + ((textControl != null) ? "para el control " + textControl : "") + " debe ser mayor o igual a " + _objCaseErrorLength.Min + " caracteres.";
                        }
                        else
                        {
                            if (_objCaseErrorLength.Min == 0 && _objCaseErrorLength.Max != 0)
                            {
                                varOut.OutMessage = "El largo máximo " + ((textControl != null) ? "para el control " + textControl : "") + " debe ser menor o igual a " + _objCaseErrorLength.Max + " caracteres.";
                            }
                        }
                    }
                }
                else
                {
                    if (!_res_rExpr)
                    {
                        varOut.OutMessage = "El campo '" + textControl + "' posee caracteres no permitidos.";
                        //varOut.OutMessage = "El valor ingresado " + ((textControl != null) ? "para el control " + textControl + "," : "") + " no posee el formato requerido.";
                    }
                }

                varOut.OutMethod = (_res_rExpr && _res_rExprLenght);
                if (typeRequired.valTypeUnRequired == typeReq && ((string.IsNullOrWhiteSpace(value)) ? "" : value).Trim().Length == 0)
                {
                    varOut.OutMethod = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            varOut.ParamName = paramName;
            varOut.ParamValue = value;
            return varOut;
        }
        public static bool existValue(string fieldname, string value, List<object> list, typeRequired typeReq = typeRequired.valTypeRequired)
        {
            bool varOut = false;
            try
            {
                if (typeRequired.valTypeRequired == typeReq || value.Trim().Length > 0)
                {
                    varOut = list.Exists(x => ((Dictionary<string, object>)x).ContainsValue(value) && ((Dictionary<string, object>)x).ContainsKey(fieldname));
                }
                else
                {
                    varOut = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return varOut;
        }

        public static dynamic GetFilteredParams(List<OutValidator> outValidators)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic dataParams = serializer.Deserialize<dynamic>("{}");
            try
            {
                foreach (OutValidator o in outValidators.FindAll(x => (x.OutMethod && !string.IsNullOrWhiteSpace(x.ParamName))))
                {
                    dataParams[o.ParamName] = o.ParamValue;
                }
            }
            catch (Exception ex)
            {
                dataParams = null;
            }
            return dataParams;
        }

        #region métodos privados
        private static bool isValidRut(string rut)
        {
            bool varOut = false;
            try
            {
                rut = rut.Replace(".", "").ToUpper();
                Regex expresion = new Regex("^([0-9]+-[0-9K])$");
                string dv = rut.Substring(rut.Length - 1, 1);
                if (!expresion.IsMatch(rut))
                {
                    return false;
                }

                varOut = (dv == Digito(int.Parse(rut.Split('-')[0])));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return varOut;
        }
        private static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }
        private static bool _IsReCaptchValid(string gRecaptchaResponse)
        {
            bool result = false;
            string captchaResponse = gRecaptchaResponse;
            string secretKey = ConfigurationManager.AppSettings["reCAPTCHAPrivateKey"];
            string apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic jsonObject = serializer.Deserialize<dynamic>(stream.ReadToEnd());
                    if (jsonObject != null)
                    {
                        try { result = (bool)jsonObject["success"]; } catch { }
                    }
                }
            }
            return result;
        }
        private static bool _IsJson(string json)
        {
            bool varOut = false;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                dynamic dataParams = serializer.Deserialize<dynamic>(json);
                varOut = true;
            }
            catch { }
            return varOut;
        }
        #endregion
    }

    public class OutValidator
    {
        public OutValidator()
        {
            this.OutMethod = false;
            this.OutMessage = string.Empty;
            this.ParamName = string.Empty;
            this.ParamValue = string.Empty;
        }
        public OutValidator(bool OutMethod, string OutMessage)
        {
            this.OutMethod = OutMethod;
            this.OutMessage = OutMessage;
        }
        public OutValidator(bool OutMethod, string OutMessage, string ParamName, string ParamValue)
        {
            this.OutMethod = OutMethod;
            this.OutMessage = OutMessage;
            this.ParamName = ParamName;
            this.ParamValue = ParamValue;
        }
        public bool OutMethod { get; set; }
        public string OutMessage { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
    }
    public class ValidatorCaseErrorLength
    {
        public ValidatorCaseErrorLength()
        {
            Min = 0;
            Max = 0;
        }
        public int Min { get; set; }
        public int Max { get; set; }
    }

}
