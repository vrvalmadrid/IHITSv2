using inHealthTechnicalExam.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace inHealthTechnicalExam.Utilities
{
    public class Utility
    {
        public bool IsUserHasAdminRole(string roleCode)
        {
            bool returnValue;
            if (string.Compare(roleCode, Constant.ADMIN) == 0)
                returnValue = true;
            else
                returnValue = false;
            return returnValue;
        }
        //Encoding
        public string Encrypt(string strData)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));
        }
        //Decoding
        public string Decrypt(string strData)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strData)));
        }
        public byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Constant.strPermutation,
            new byte[] { Constant.bytePermutation1,
                         Constant.bytePermutation2,
                         Constant.bytePermutation3,
                         Constant.bytePermutation4
            });
            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);
            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
        public byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(Constant.strPermutation,
            new byte[] { Constant.bytePermutation1,
                         Constant.bytePermutation2,
                         Constant.bytePermutation3,
                         Constant.bytePermutation4
            });
            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);
            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}
