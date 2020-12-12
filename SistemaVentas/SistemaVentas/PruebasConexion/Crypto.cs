using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace SistemaVentas.PruebasConexion
{
    public static class Crypto
    {
        private static string key = "04DsGt1$sNxxyO2012";

        public static string Desencriptar(string textoEncriptado)
        {
            return Desencriptar(textoEncriptado, key);
        }


        public static string Desencriptar(string textoEncriptado, string key)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);


            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

            tdes.Clear();
            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        public static StreamReader DecryptFile(string fromFileNamePath, string keyFileNamePath)
        {
            // The encrypted file
            var fsFileIn = File.OpenRead(fromFileNamePath);
            // The key
            var fsKeyFile = File.OpenRead(keyFileNamePath);

            // Prepare the encryption algorithm and read the key from the key file
            var cryptAlgorithm = new TripleDESCryptoServiceProvider();
            var brFile = new BinaryReader(fsKeyFile);
            cryptAlgorithm.Key = brFile.ReadBytes(24);
            cryptAlgorithm.IV = brFile.ReadBytes(8);

            // The cryptographic stream takes in the unecrypted file
            var csEncrypt = new CryptoStream(fsFileIn, cryptAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);

            // Write the new unecrypted file
            var srCleanStream = new StreamReader(csEncrypt);
            return srCleanStream;
        }
    }
}
