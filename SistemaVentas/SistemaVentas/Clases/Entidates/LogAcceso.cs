using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Clases.Entidates
{
    public class LogAcceso
    {
       
        private string _NombrePC;
        private string _IP;
        private string _Serial_PC;
        private string _MAC;
        private string _Acceso;

        public string NombrePC { get => _NombrePC; set => _NombrePC = value; }
        public string IP { get => _IP; set => _IP = value; }
        public string Serial_PC { get => _Serial_PC; set => _Serial_PC = value; }
        public string MAC { get => _MAC; set => _MAC = value; }
        public string Acceso { get => _Acceso; set => _Acceso = value; }
    }
}
