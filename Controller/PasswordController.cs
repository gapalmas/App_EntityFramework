using System;
using System.Security.Cryptography;
using System.Text;

namespace Controller
{
    /// <summary>
    ///  Clase para codificar un password.
    /// </summary>
    public class PasswordController
    {
        /// <summary>
        ///  Clase para codificar un password en hash.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Retorna un password encryptado.</returns>
        public static string EncodePassword(string password)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            var inputBytes = new UnicodeEncoding().GetBytes(password);
            var hash = sha1.ComputeHash(inputBytes);
            return Convert.ToBase64String(hash);
        }
    }
}
