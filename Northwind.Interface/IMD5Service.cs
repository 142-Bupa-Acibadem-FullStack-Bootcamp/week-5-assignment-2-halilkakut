using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Interface
{
    public interface IMD5Service
    {
        string ConvertTextToMD5(string text);
        string Encrypt(string text);
        string Decrypt(string encryptValue);
    }
}
