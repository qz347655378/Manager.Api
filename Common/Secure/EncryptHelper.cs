using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Secure
{
    public static class EncryptHelper
    {
        /// <summary>
        /// Hash256+base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Hash256Encrypt(string str)
        {
            var b = Encoding.UTF8.GetBytes(str);
            var sha256 = new SHA256Managed();
            var data = sha256.ComputeHash(b);
            return Convert.ToBase64String(data);
        }
    }
}
