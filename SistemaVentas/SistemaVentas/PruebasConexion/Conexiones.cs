using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xml.Serialization;

namespace SistemaVentas.PruebasConexion
{
    public static class Conexiones
    {

        private static string _xmlFile;

        public static string XmlFile
        {
            get { return string.IsNullOrEmpty(_xmlFile) ? "connections.xml" : _xmlFile; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _xmlFile = "connections.xml";
                else _xmlFile = value;
            }
        }

        private static string _encriptedXmlFile;

        public static string EncryptedXmlFile
        {
            get { return string.IsNullOrEmpty(_encriptedXmlFile) ? "connections" : _encriptedXmlFile; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _encriptedXmlFile = "connections";
                else _encriptedXmlFile = value;
            }
        }
        private static string _key;
        public static string Key
        {
            get { return string.IsNullOrEmpty(_key) ? "key.key" : _key; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _key = "key.key";
                else _key = value;
            }
        }
        private static List<Conexion> _runTimeList = new List<Conexion>();

        public static List<Conexion> RunTimeList
        {
            get { return _runTimeList; }
            set { _runTimeList = value; }
        }

        private static List<Conexion> _fileList = new List<Conexion>();
        public static List<Conexion> EntireList
        {
            get
            {
                List<Conexion> list;
                if (_fileList.Count == 0)
                {
                    var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\";
                    try
                    {
                        _fileList = Serialization.DeserializeXml<Conexion>(Crypto.DecryptFile(path + EncryptedXmlFile, path + Key));
                    }
                    catch (Exception)
                    {
                        _fileList = Serialization.DeserializeXml<Conexion>(path + XmlFile);
                    }
                }
                list = _fileList;
                list.AddRange(RunTimeList);
                return list;
            }
        }

        public static Conexion FindConnection(int id)
        {
            var conn = EntireList.Find(
                delegate (Conexion cnn)
                {
                    return cnn.ConnectionId == id;
                });
            //var conn = (from connection in EntireList where connection.ConnectionId == id select connection).SingleOrDefault();
            return conn;
        }

    }
}
