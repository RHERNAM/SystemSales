using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Clases.Entidates
{
    public class ListaInventario
    {
        private int _Idarticulo;
        private string _Codigo;      
        private decimal _PrecioCompra;
        private decimal _PrecioVenta;
        private decimal _GananciaObtener;
        //private decimal _TotalInventario;
       // private decimal _CantidadVendido;
        private decimal _CantidadMinimo;
        private decimal _CantidadMaximo;
        private DateTime _FechaVencimiento;

        private decimal _IvaUnitario;
        private decimal _Cantidad;
        private decimal _Contenido;
        private decimal _Total;
        private int _NumComprobante;
        private int _Idproveedor;

        private string _Descripcion;

       
      
        private int _TipoComprobante;
        private string _Comprobante;
        private decimal _Total_Articulos;
        private decimal _MontoTotal;
        private decimal _IvaTotal;
        private DateTime _FechaComprobante;

        private int _IdMotivoMov;
        private string _IdFolioTipoEntrada;
        private int _IdSucursal; 
        private int _IdEmpresa;
        private string _MotivoMov, _Sucursal, _Empresa, _Proveedor;

        public ListaInventario(int idarticulo, string codigo, string descripcion, decimal preciocompra, decimal gananciaobtener, decimal precioventa,
           decimal cantidad, decimal contenido, decimal total, /*decimal totalinventario,*/ /*decimal cantidadvendido,*/ decimal cantidadminimo, decimal cantidadmaximo, decimal ivaunitario,
             int proveedor, int numcomprobante, int tipoComprobante,string comprobante,decimal totalArticulo, decimal montototal, decimal ivatotal, DateTime fechacomprobante, DateTime fechavencimiento,
             int idMotivoMov,string idfoliotipoEntrada, int idSucursal, int idEmpresa,string motivoMov, string sucursal,string empresa,string sproveedor )
        {
            Idarticulo = idarticulo;
            Codigo   = codigo;
            Descripcion = descripcion;
            Precio_Compra = preciocompra;
            Ganancia_Obtener = gananciaobtener;
            Precio_Venta = precioventa;
            Cantidad = cantidad;
            //Total_Inventario = totalinventario;
            //Cantidad_Vendido = cantidadvendido;
            Cantidad_Minimo = cantidadminimo;
            Cantidad_Maximo = cantidadmaximo;
            IvaUnitario = ivaunitario;
            Contenido = contenido;
            Total = total;
            Idproveedor = proveedor;
            Proveedor = sproveedor;
            Numero_Comprobante = numcomprobante;
            Tipo_Comprobante = tipoComprobante;
            Comprobante = comprobante;
            Total_Articulos = totalArticulo;
            Monto_Total = montototal;
            IvaTotal = ivatotal;
            Fecha_Comprobante = fechacomprobante;
            Fecha_Vencimiento = fechavencimiento;
            IdMotivoMov = idMotivoMov;
            Folio_Entrada = idfoliotipoEntrada;
            IdSucursal = idSucursal;
            IdEmpresa = idEmpresa;
            MotivoMov = motivoMov;
            Sucursal = sucursal;
            Empresa = empresa;

        }

        public int Idarticulo { get => _Idarticulo; set => _Idarticulo = value; }
        public string Codigo { get => _Codigo; set => _Codigo = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public decimal Precio_Compra { get => _PrecioCompra; set => _PrecioCompra = value; }
        public decimal Ganancia_Obtener { get => _GananciaObtener; set => _GananciaObtener = value; }
        public decimal Precio_Venta { get => _PrecioVenta; set => _PrecioVenta = value; }
        public decimal Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public decimal Contenido { get => _Contenido; set => _Contenido = value; }
        public decimal Total { get => _Total; set => _Total = value; }
        //public decimal Total_Inventario { get => _TotalInventario; set => _TotalInventario = value; }
        //public decimal Cantidad_Vendido { get => _CantidadVendido; set => _CantidadVendido = value; }
        public decimal Cantidad_Minimo { get => _CantidadMinimo; set => _CantidadMinimo = value; }
        public decimal Cantidad_Maximo { get => _CantidadMaximo; set => _CantidadMaximo = value; }
        public decimal IvaUnitario { get => _IvaUnitario; set => _IvaUnitario = value; }
        public int Idproveedor { get => _Idproveedor; set => _Idproveedor = value; }
        public string Comprobante { get => _Comprobante; set => _Comprobante = value; }
        public int Numero_Comprobante { get => _NumComprobante; set => _NumComprobante = value; }
        public decimal Total_Articulos { get => _Total_Articulos; set => _Total_Articulos = value; }
        public decimal Monto_Total { get => _MontoTotal; set => _MontoTotal = value; }
        public decimal IvaTotal { get => _IvaTotal; set => _IvaTotal = value; }
        public DateTime Fecha_Comprobante { get => _FechaComprobante; set => _FechaComprobante = value; }
        public DateTime Fecha_Vencimiento { get => _FechaVencimiento; set => _FechaVencimiento = value; }
        public int Tipo_Comprobante { get => _TipoComprobante; set => _TipoComprobante = value; }
        public int IdMotivoMov { get => _IdMotivoMov; set => _IdMotivoMov = value; }
        public int IdSucursal { get => _IdSucursal; set => _IdSucursal = value; }
        public int IdEmpresa { get => _IdEmpresa; set => _IdEmpresa = value; }
        public string MotivoMov { get => _MotivoMov; set => _MotivoMov = value; }
        public string Sucursal { get => _Sucursal; set => _Sucursal = value; }
        public string Empresa { get => _Empresa; set => _Empresa = value; }
        public string Folio_Entrada { get => _IdFolioTipoEntrada; set => _IdFolioTipoEntrada = value; }
        public string Proveedor { get => _Proveedor; set => _Proveedor = value; }
    }
}
