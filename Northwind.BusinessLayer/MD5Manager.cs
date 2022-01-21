using Northwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.BusinessLayer
{
    public class MD5Manager : IMD5Service
    {
        public string ConvertTextToMD5(string text)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string encryptValue)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string text)
        {
            using (MD5 mD5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(text);
                byte[] hashBytes = mD5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
