using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Clases.Entidates
{
    public static class MemoriaCache
    {
        public static string Ip { get; set; }
        public static string Mac { get; set; }
        public static string NombrePC { get; set; }
        public static string SerialPC { get; set; }
        public static int IdCaja { get; set; }
        public static int IdApertura { get; set; }
        public static int IdEstadoCaja { get; set; }        
        public static int OpcionFormulario { get; set; }
        public static int NumVenta { get; set; }        
        public static string ValidarVenta { get; set; }
        public static string Mensaje { get; set; }

        public static int IdSucursal { get; set; }
        public static int IdEmpresa { get; set; }
        public static string Empresa { get; set; }
        public static string Sucursal { get; set; }
    }
}
