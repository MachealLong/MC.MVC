using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MC.MVC.Framework.Framework.Encrypt
{
    public class MD5Encryptor
    {
        public string Encrypt(string source)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(source);
            byte[] targetData = md5.ComputeHash(fromData);
            md5.Clear();
            StringBuilder byte2String = new StringBuilder();
            foreach (byte b in targetData)
            {
                byte2String.AppendFormat("{0:X2}", b);
            }

            return byte2String.ToString();
        }
    }
}
