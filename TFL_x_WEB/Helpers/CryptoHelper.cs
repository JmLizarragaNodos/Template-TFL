using MCTP_c_Modelos_de_Datos.Entity;
using System;
using System.Security.Cryptography;

namespace TFL_x_WEB.Helpers
{
    public class EjemploAuth
    {
        public string nombre { get; set; }
        public int rutNumero { get; set; }
        public string sesi_ccod { get; set; }
        public int pers_ncorr { get; set; }
        public PermisosAuth permisosAuth { get; set; }
    }

    public class InfoTFL 
    {
        public USUARIO_ENT usuario { get; set; }
        public PermisosAuth permisosAuth { get; set; }
    }

    public class PermisosAuth
    {
        public bool SELECT { get; set; }
        public bool UPDATE { get; set; }
    }

    public static class CryptoHelper
    {
        private static readonly string _SECRET_KEY;

        static CryptoHelper()
        {
            _SECRET_KEY = "HolaMundo123";
        }

        public static string Encriptar(string textoPlano)
        {
            string llave = _SECRET_KEY;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = DerivarClave(llave, aesAlg.KeySize / 8);
                aesAlg.IV = new byte[16]; // IV debe tener 16 bytes

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(textoPlano);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Desencriptar(string textoEncriptado)
        {
            string llave = _SECRET_KEY;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = DerivarClave(llave, aesAlg.KeySize / 8);
                aesAlg.IV = new byte[16]; // IV debe tener 16 bytes

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(textoEncriptado)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private static byte[] DerivarClave(string llave, int longitud)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(llave, new byte[16], 10000))
            {
                return pbkdf2.GetBytes(longitud);
            }
        }

        public static bool TryDesencriptar(string textoEncriptado, out string resultadoDesencriptado)
        {
            resultadoDesencriptado = null;

            try
            {
                string llave = _SECRET_KEY;

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = DerivarClave(llave, aesAlg.KeySize / 8);
                    aesAlg.IV = new byte[16]; // IV debe tener 16 bytes

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(textoEncriptado)))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        resultadoDesencriptado = srDecrypt.ReadToEnd();
                    }
                }

                // Si llega aquí, la desencriptación fue exitosa
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}