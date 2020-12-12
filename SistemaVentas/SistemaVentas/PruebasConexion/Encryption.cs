using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;


namespace SistemaVentas.PruebasConexion
{
    public class Encryption
    {

        private static byte[] sharedkey = {0x0B, 0x02, 0x03, 0x05, 0x07, 0x0B, 0x0D, 0x11,
                                                0x0B, 0x11, 0x0D, 0x0B, 0x07, 0x02, 0x04, 0x11,
                                                0x0B, 0x02, 0x03, 0x05, 0x07, 0x0B, 0x0D, 0x11};

        private static byte[] sharedvector = { 0x01, 0x02, 0x03, 0x05, 0x07, 0x0B, 0x0D, 0x11 };

        public static String Encrypt(String val)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            byte[] toEncrypt = Encoding.UTF8.GetBytes(val);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, tdes.CreateEncryptor(sharedkey, sharedvector), CryptoStreamMode.Write);
            cs.Write(toEncrypt, 0, toEncrypt.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
