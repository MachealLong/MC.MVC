using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MC.MVC.Framework.Encrypt
{
    public class DesEncryptor
    {
        private string desKey = "HHSoft";

        public DesEncryptor()
        {

        }

        public DesEncryptor(string desKey)
        {
            this.desKey = desKey;
        }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string DesKey
        {
            get { return this.desKey; }
            set { this.desKey = value; }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">要加密的字符串</param>
        /// <returns></returns>
        public string Encrypt(string source)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StringBuilder ret = new StringBuilder();
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] inputByteArray = Encoding.Default.GetBytes(source);
                byte[] keyByteArray = Encoding.Default.GetBytes(this.desKey);
                SHA1 ha = new SHA1Managed();
                byte[] hb = ha.ComputeHash(keyByteArray);

                byte[] sKey = new byte[8];
                byte[] sIV = new byte[8];

                for (int i = 0; i < 8; i++)
                {
                    sKey[i] = hb[i];
                }

                for (int i = 8; i < 16; i++)
                {
                    sIV[i - 8] = hb[i];
                }
                des.Key = sKey;
                des.IV = sIV;

                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            finally
            {
                if (null != cs) { cs.Close(); }
                if (null != ms) { ms.Close(); }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="source">要解密的字符串</param>
        /// <returns></returns>
        public string Decrypt(string source)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] sKey = new byte[8];
            byte[] sIV = new byte[8];
            string values = "";

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[source.Length / 2];
            for (int x = 0; x < source.Length / 2; x++)
            {
                int i = (Convert.ToInt32(source.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            try
            {
                byte[] keyByteArray = Encoding.Default.GetBytes(this.desKey);
                SHA1 ha = new SHA1Managed();
                byte[] hb = ha.ComputeHash(keyByteArray);

                for (int i = 0; i < 8; i++)
                {
                    sKey[i] = hb[i];
                }
                for (int i = 8; i < 16; i++)
                {
                    sIV[i - 8] = hb[i];
                }

                des.Key = sKey;
                des.IV = sIV;

                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            finally
            {
                if (null != cs) { cs.Close(); }
                if (null != ms) { ms.Close(); }
            }
        }
    }
}
