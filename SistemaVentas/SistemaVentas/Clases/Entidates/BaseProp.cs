using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Clases.Entidates
{
    public static class BaseProp
    {
        
        private static int _IdLog;      
        private static string Modulo;
        private static int _Nomina;
        public  static int IdLog { get => _IdLog; set => _IdLog = value; }
        public static string Modulo1 { get => Modulo; set => Modulo = value; }
        public static int Nomina { get => _Nomina; set => _Nomina = value; }
    }
}
