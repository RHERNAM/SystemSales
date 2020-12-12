using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Clases.Entidates
{
    public class ListaVenta
    {
        private int _Id_Inventario;
        private int _Id_Articulo;
        private string _Descripcion;
        private decimal _Cantidad;
        private decimal _Precio;
        private decimal _Total;
        private string _Codigo;
        private byte[] _Foto;

        //Constructor Obligatorio 
        public ListaVenta(string codigo, int id_iventario, int id_articulo, string descripcion, decimal cantidad, decimal precio, decimal total, byte[] foto)
        {
            Codigo = codigo;
            Id_Inventario = id_iventario;
            Id_Articulo = id_articulo;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
            Total = total;
            Foto = foto;
        }

        public string Codigo { get => _Codigo; set => _Codigo = value; }
        public int Id_Inventario { get => _Id_Inventario; set => _Id_Inventario = value; }
        public int Id_Articulo { get => _Id_Articulo; set => _Id_Articulo = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public decimal Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public decimal Precio { get => _Precio; set => _Precio = value; }
        public decimal Total { get => _Total; set => _Total = value; }
        public byte[] Foto { get => _Foto; set => _Foto = value; }
    }
}
