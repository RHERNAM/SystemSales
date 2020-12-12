using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SistemaVentas.PruebasConexion
{
    public class Conexion
    {
        [XmlAttribute("Id")]
        public int ConnectionId { get; set; }
        [XmlAttribute("Type")]
        public TipoConexion tipoConexion { get; set; }

        [XmlAttribute("Name")]
        public string ConnectionName { get; set; }

        [XmlAttribute]
        public string DataSource { get; set; }

        [XmlAttribute]
        public string User { get; set; }

        [XmlAttribute]
        public string Password { get; set; }

        [XmlAttribute]
        public string DatabaseName { get; set; }

        [XmlAttribute]
        public string Provider { get; set; }

        [XmlAttribute]
        public bool Pooling { get; set; }

        private short _connectTimeout;
        [XmlAttribute]
        public short ConnectTimeout
        {
            get
            {
                return _connectTimeout == Convert.ToInt16(0) ? Convert.ToInt16(10) : _connectTimeout;
            }
            set { _connectTimeout = value; }
        }

        [XmlAttribute]
        public short Encryption { get; set; }


        public string GetConnectionString()
        {
            var cadena = "server=" + DataSource + ";" + " User Id=" + User + ";" +
                "Password=" + ((Encryption == 1) ? Crypto.Desencriptar(Password) : Password) +
                ";pooling=" + Pooling + ";" + "database=" + DatabaseName + ";" + "Connect Timeout=" + ConnectTimeout + "; " + ((tipoConexion == TipoConexion.MySql) ? /*"use procedure bodies=false"*/ "" : "");
            return cadena;
        }
    }
}
