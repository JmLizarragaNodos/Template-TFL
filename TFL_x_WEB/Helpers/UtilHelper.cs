using System;
using System.Text.RegularExpressions;

namespace TFL_x_WEB.Helpers
{
    public static class UtilHelper
    {
        public static string DesencriptarHtml(string cadena)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                cadena = cadena.Replace("/amp/", "&");
                cadena = cadena.Replace("/lt/", "<");
                cadena = cadena.Replace("/gt/", ">");

                //cadena = cadena.Replace("|amp;", "&");
                //cadena = cadena.Replace("|lt;", "<");
                //cadena = cadena.Replace("|gt;", ">");

                cadena = Regex.Replace(cadena, "<!--.*?-->", String.Empty, RegexOptions.Singleline);   // Remover comentarios <!-- -->     
            }

            return cadena;
        }
    }
}