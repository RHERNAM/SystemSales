using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCL;
using System.Data;
using System.Data.SqlClient;
using SistemaVentas.Clases.Entidates;
using AccesDLL;

namespace SistemaVentas.Clases.SQL.Transacciones
{
    public class ClsGuardar : SqlConexion
    {
        #region Sistema Log

        #region Guarda Log Acceso

        public static void LogAcceso(int Nomina, string NombrePc, string SerialPC, string Ip, string Mac, string Acceso)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Guardar_LogAccesos", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina),
                ComandoSQL.Parameters.AddWithValue("@NombrePC", NombrePc),
                ComandoSQL.Parameters.AddWithValue("@Serial_PC", SerialPC),
                ComandoSQL.Parameters.AddWithValue("@IP", Ip),
                ComandoSQL.Parameters.AddWithValue("@MAC", Mac),
                ComandoSQL.Parameters.AddWithValue("@Acceso", Acceso));
            //ComandoSQL.CommandType = CommandType.StoredProcedure;
            ComandoSQL.Parameters.Clear();

        }

        
        #endregion

        #region Salir del Sistema

        public static void SalirSistema(int IdLog, int Nomina)
        {
            AccesoDatos.ExecuteNonQuery("Sp_ActualizarLog", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Log", IdLog),
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina));

        }

        #endregion

        #region Guarda Log Detalle        
        public static void GuardarLogDetalle(int IdLog, string Modulo, string Accion)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Guarda_LogDetalle", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_LogAcceso", IdLog),
                 ComandoSQL.Parameters.AddWithValue("@Modulo", Modulo),
                  ComandoSQL.Parameters.AddWithValue("@Accion_Ejecutado", Accion));
        }
        #endregion

        #endregion

        #region Registro de Usuarios

        public static void RegistrarUsuario(int Nomina, string Nombre_Usuario, string Contraseña,
               string Email, int Pin, string Palabra_Seguridad)
        {
            AccesoDatos.ExecuteNonQuery("Sp_InsertarUserName", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina),
                ComandoSQL.Parameters.AddWithValue("@Usuario_Name", Nombre_Usuario),
                ComandoSQL.Parameters.AddWithValue("@Contraseña", Contraseña),
                ComandoSQL.Parameters.AddWithValue("@Correo", Email),
                ComandoSQL.Parameters.AddWithValue("@Pin", Pin),
                ComandoSQL.Parameters.AddWithValue("@Palabra_Seguridad", Palabra_Seguridad));
            ComandoSQL.Parameters.Clear();
        }
        #endregion

        #region Empleados        

        #region Guardar Empleado
        public static void GuardaEmpleado(int Nomina, string Nombre, string Apellido_Paterno, string Apellido_Materno, string CURP,
        string RFC, DateTime Fecha_Nacimiento, string Telefono, string Celular, string Email, int Puesto, string Calle, string Numero_Interno,
        string Numero_Exterior, string Entre_Calles, int Colonia, int Codigo_Postal, int Municipio, int Estado, int Pais, byte[] Foto, 
        int IdEstatus, int IdArea, int IdEpresa, int IdSucursal)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Insertar_Empleados", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina),
                ComandoSQL.Parameters.AddWithValue("@Nombre", Nombre),
                ComandoSQL.Parameters.AddWithValue("@Apellido_Paterno", Apellido_Paterno),
                ComandoSQL.Parameters.AddWithValue("@Apellido_Materno", Apellido_Materno),
                ComandoSQL.Parameters.AddWithValue("@CURP", CURP),
                ComandoSQL.Parameters.AddWithValue("@RFC", RFC),
                ComandoSQL.Parameters.AddWithValue("@Fecha_Nacimiento", Fecha_Nacimiento),
                ComandoSQL.Parameters.AddWithValue("@Telefono", Telefono),
                ComandoSQL.Parameters.AddWithValue("@Celular", Celular),
                ComandoSQL.Parameters.AddWithValue("@Email", Email),
                ComandoSQL.Parameters.AddWithValue("@Puesto", Puesto),
                ComandoSQL.Parameters.AddWithValue("@Calle", Calle),
                ComandoSQL.Parameters.AddWithValue("@Numero_Interno", Numero_Interno),
                ComandoSQL.Parameters.AddWithValue("@Numero_Exterior", Numero_Exterior),
                ComandoSQL.Parameters.AddWithValue("@Entre_Calles", Entre_Calles),
                ComandoSQL.Parameters.AddWithValue("@Colonia", Colonia),
                ComandoSQL.Parameters.AddWithValue("@Codigo_Postal", Codigo_Postal),
                ComandoSQL.Parameters.AddWithValue("@Municipio", Municipio),
                ComandoSQL.Parameters.AddWithValue("@Estado", Estado),
                ComandoSQL.Parameters.AddWithValue("@Pais", Pais),
                ComandoSQL.Parameters.AddWithValue("@Foto", Foto),
                ComandoSQL.Parameters.AddWithValue("@Estatus", IdEstatus),
                ComandoSQL.Parameters.AddWithValue("@Id_Area", IdArea),
                ComandoSQL.Parameters.AddWithValue("@Id_Empresa", IdEpresa),
                ComandoSQL.Parameters.AddWithValue("@Id_Sucursal", IdSucursal)
                );

            ComandoSQL.Parameters.Clear();
        }

        #endregion

        #region Guardar Actualizar
        public static void ActualizarEmpleado(int Id_Empleado, int Nomina, string Nombre, string Apellido_Paterno, string Apellido_Materno, string CURP,
        string RFC, DateTime Fecha_Nacimiento, string Telefono, string Celular, string Email, int Puesto, string Calle, string Numero_Interno,
        string Numero_Exterior, string Entre_Calles, int Colonia, int Codigo_Postal, int Municipio, int Estado, int Pais, byte[] Foto, 
        int IdEstatus, int IdArea, int IdEpresa, int IdSucursal)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Actualizar_Empleado", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Usuario", Id_Empleado),
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina),
                ComandoSQL.Parameters.AddWithValue("@Nombre", Nombre),
                ComandoSQL.Parameters.AddWithValue("@Apellido_Paterno", Apellido_Paterno),
                ComandoSQL.Parameters.AddWithValue("@Apellido_Materno", Apellido_Materno),
                ComandoSQL.Parameters.AddWithValue("@CURP", CURP),
                ComandoSQL.Parameters.AddWithValue("@RFC", RFC),
                ComandoSQL.Parameters.AddWithValue("@Fecha_Nacimiento", Fecha_Nacimiento),
                ComandoSQL.Parameters.AddWithValue("@Telefono", Telefono),
                ComandoSQL.Parameters.AddWithValue("@Celular", Celular),
                ComandoSQL.Parameters.AddWithValue("@Email", Email),
                ComandoSQL.Parameters.AddWithValue("@Puesto", Puesto),
                ComandoSQL.Parameters.AddWithValue("@Calle", Calle),
                ComandoSQL.Parameters.AddWithValue("@Numero_Interno", Numero_Interno),
                ComandoSQL.Parameters.AddWithValue("@Numero_Exterior", Numero_Exterior),
                ComandoSQL.Parameters.AddWithValue("@Entre_Calles", Entre_Calles),
                ComandoSQL.Parameters.AddWithValue("@Colonia", Colonia),
                ComandoSQL.Parameters.AddWithValue("@Codigo_Postal", Codigo_Postal),
                ComandoSQL.Parameters.AddWithValue("@Municipio", Municipio),
                ComandoSQL.Parameters.AddWithValue("@Estado", Estado),
                ComandoSQL.Parameters.AddWithValue("@Pais", Pais),
                ComandoSQL.Parameters.AddWithValue("@Foto", Foto),
                ComandoSQL.Parameters.AddWithValue("@Estatus", IdEstatus),
                ComandoSQL.Parameters.AddWithValue("@Id_Area", IdArea),
                ComandoSQL.Parameters.AddWithValue("@Id_Empresa", IdEpresa),
                ComandoSQL.Parameters.AddWithValue("@Id_Sucursal", IdSucursal)
                );

            ComandoSQL.Parameters.Clear();
        }

        #endregion

        #region Eliminar Empleado

        public static bool EliminarEmpleado(int Nomina)
        {
            int Resultado = AccesoDatos.ExecuteNonQuery("Sp_Eliminar_Empleado", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina));
            if (Resultado > 0)
            {
                return true;
            }
            return false;

        }

        #endregion

        #endregion

        #region Articulo

        #region Guardar Articulo
        public static void ArticuloGuardar(string Codigo, string Nombre, string Marca, string Descripcion, int ID_Presentacion_Art,
        int ID_Unidad_Medida, int Id_Fabricante, decimal Contenido_Neto, decimal Cantidad_Minimo, decimal Cantidad_Maximo, int Unidad_Venta,
        byte[] Foto, int Id_Estatus, int IdAgranel,  int IdSucursal, int IdEmpresa)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Insertar_Articulo", CommandType.StoredProcedure, ConexionString,
            ComandoSQL.Parameters.AddWithValue("@Codigo", Codigo),
            ComandoSQL.Parameters.AddWithValue("@Nombre", Nombre),
            ComandoSQL.Parameters.AddWithValue("@Marca", Marca),
            ComandoSQL.Parameters.AddWithValue("@Descripcion", Descripcion),
            ComandoSQL.Parameters.AddWithValue("@ID_Presentacion_Art", ID_Presentacion_Art),
            ComandoSQL.Parameters.AddWithValue("@ID_Unidad_Medida", ID_Unidad_Medida),
            ComandoSQL.Parameters.AddWithValue("@Id_Fabricante", Id_Fabricante),
            ComandoSQL.Parameters.AddWithValue("@Contenido_Neto", Contenido_Neto),
            ComandoSQL.Parameters.AddWithValue("@Cantidad_Minimo", Cantidad_Minimo),
            ComandoSQL.Parameters.AddWithValue("@Cantidad_Maximo", Cantidad_Maximo),
            ComandoSQL.Parameters.AddWithValue("@Unidad_Venta", Unidad_Venta),
            ComandoSQL.Parameters.AddWithValue("@Foto", Foto),
            ComandoSQL.Parameters.AddWithValue("@Id_Estatus", Id_Estatus),
            ComandoSQL.Parameters.AddWithValue("@IdAgranel", IdAgranel),            
            ComandoSQL.Parameters.AddWithValue("@Id_Sucursal", IdSucursal),
            ComandoSQL.Parameters.AddWithValue("@Id_Empresa", IdEmpresa));

            ComandoSQL.Parameters.Clear();
        }

        #endregion

        #region Actualizar Articulo
        public static void ArticuloActualizar(int IdArticulo, string Codigo, string Nombre, string Marca, string Descripcion, int ID_Presentacion_Art,
        int ID_Unidad_Medida, int Id_Fabricante, decimal Contenido_Neto, decimal Cantidad_Minimo, decimal Cantidad_Maximo, int Unidad_Venta,
        byte[] Foto, int Id_Estatus, int IdSucursal, int IdEmpresa)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Actualizar_Articulo", CommandType.StoredProcedure, ConexionString,
           ComandoSQL.Parameters.AddWithValue("@Id_Articulo", IdArticulo),
           ComandoSQL.Parameters.AddWithValue("@Codigo", Codigo),
           ComandoSQL.Parameters.AddWithValue("@Nombre", Nombre),
           ComandoSQL.Parameters.AddWithValue("@Marca", Marca),
           ComandoSQL.Parameters.AddWithValue("@Descripcion", Descripcion),
           ComandoSQL.Parameters.AddWithValue("@ID_Presentacion_Art", ID_Presentacion_Art),
           ComandoSQL.Parameters.AddWithValue("@ID_Unidad_Medida", ID_Unidad_Medida),
           ComandoSQL.Parameters.AddWithValue("@Id_Fabricante", Id_Fabricante),
           ComandoSQL.Parameters.AddWithValue("@Contenido_Neto", Contenido_Neto),
           ComandoSQL.Parameters.AddWithValue("@Cantidad_Minimo", Cantidad_Minimo),
           ComandoSQL.Parameters.AddWithValue("@Cantidad_Maximo", Cantidad_Maximo),
           ComandoSQL.Parameters.AddWithValue("@Unidad_Venta", Unidad_Venta),
           ComandoSQL.Parameters.AddWithValue("@Foto", Foto),
           ComandoSQL.Parameters.AddWithValue("@Id_Estatus", Id_Estatus),
           ComandoSQL.Parameters.AddWithValue("@Id_Sucursal", IdSucursal),
           ComandoSQL.Parameters.AddWithValue("@Id_Empresa", IdEmpresa)
           );

            ComandoSQL.Parameters.Clear();

        }

        #endregion

        #region Eliminar

        public static void ArticuloEliminar(int IdArticulo)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Eliminar_Articulo", CommandType.StoredProcedure, ConexionString,
            ComandoSQL.Parameters.AddWithValue("@IdArticulo", IdArticulo));
        }

        #endregion

        #endregion

        #region Entradas 

        #region Insertar

        public static void EntradasGuardar(int Id_Articulo, string Codigo, decimal Precio_Compra, decimal
            Precio_Venta, decimal Ganancia_Obtener, /*decimal Total_Inventario, */decimal Cantidad_Minimo, decimal Cantidad_Maximo,
           DateTime Fecha_Vencimiento, int Id_Usuario, decimal Iva_unitario, decimal Cantidad, decimal Contenido,
       /* decimal Cantidad_Total,*/ int Num_Comprobante, int Id_Proveedor,int IdTipoEntrada,int IdSucursal,int IdEmpresa, int IdTipoComprobante, string Folio_Tipo_Entrada)
        {
            AccesoDatos.ExecuteNonQuery("Sp_InsertUpdate_Inventario", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Articulo", Id_Articulo),
                ComandoSQL.Parameters.AddWithValue("@Codigo", Codigo),
                ComandoSQL.Parameters.AddWithValue("@Precio_Compra", Precio_Compra),
                ComandoSQL.Parameters.AddWithValue("@Precio_venta", Precio_Venta),
                ComandoSQL.Parameters.AddWithValue("@Ganancia_Obtener", Ganancia_Obtener),
                //ComandoSQL.Parameters.AddWithValue("@Total_Inventario", Total_Inventario),
                ComandoSQL.Parameters.AddWithValue("@Cantidad_Minimo", Cantidad_Minimo),
                ComandoSQL.Parameters.AddWithValue("@Cantidad_Maximo", Cantidad_Maximo),
                ComandoSQL.Parameters.AddWithValue("@Fecha_Vencimiento", Fecha_Vencimiento),
                ComandoSQL.Parameters.AddWithValue("@Id_Usuario", Id_Usuario),
                ComandoSQL.Parameters.AddWithValue("@Iva_unitario", Iva_unitario),
                ComandoSQL.Parameters.AddWithValue("@Cantidad", Cantidad),
                ComandoSQL.Parameters.AddWithValue("@Contenido", Contenido),
                //ComandoSQL.Parameters.AddWithValue("@Cantidad_Total", Cantidad_Total),
                ComandoSQL.Parameters.AddWithValue("@Num_Comprobante", Num_Comprobante),
                ComandoSQL.Parameters.AddWithValue("@Id_Proveedor", Id_Proveedor),
                ComandoSQL.Parameters.AddWithValue("@Tipo_Entrada", IdTipoEntrada),
                ComandoSQL.Parameters.AddWithValue("@IdEmpresa", IdEmpresa),
                ComandoSQL.Parameters.AddWithValue("@IdSucrusal", IdSucursal),
                ComandoSQL.Parameters.AddWithValue("@Tipo_Comprobante", IdTipoComprobante),
                ComandoSQL.Parameters.AddWithValue("@Folio_Tipo_Entrada", Folio_Tipo_Entrada));



            ComandoSQL.Parameters.Clear();

        }
        #endregion

        #endregion

        #region Ventas

        public static void VentaGuarda(int Id_Venta, int Usuario, int Comprobante, decimal Descuento, decimal Subtotal, decimal Iva, decimal Venta_Total, int Expedicion,decimal Efectivo,decimal Credito,int Total_Artiulos,int Estatus,string NombreVenta)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Guarda_Venta", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Venta", Id_Venta),
                ComandoSQL.Parameters.AddWithValue("@Usuario", Usuario),
                ComandoSQL.Parameters.AddWithValue("@Comprobante", Comprobante),
                ComandoSQL.Parameters.AddWithValue("@Descuento", Descuento),
                ComandoSQL.Parameters.AddWithValue("@Subtotal", Subtotal),
                ComandoSQL.Parameters.AddWithValue("@Iva", Iva),
                ComandoSQL.Parameters.AddWithValue("@Venta_Total", Venta_Total),
                ComandoSQL.Parameters.AddWithValue("@Expedicion", Expedicion),
                ComandoSQL.Parameters.AddWithValue("@Efectivo", Efectivo),
                ComandoSQL.Parameters.AddWithValue("@Credito", Credito),
                ComandoSQL.Parameters.AddWithValue("@TotalArticulos", Total_Artiulos),
                ComandoSQL.Parameters.AddWithValue("@Estatus", Estatus),
                ComandoSQL.Parameters.AddWithValue("@NombreVenta", NombreVenta)
                );
        }

        public static void VentaDetalleGuarda(int Id_Venta,int Id_Inventario,decimal Cantidad,decimal Precio_Compra,decimal Descuento,decimal Total, int Estatus)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Guarda_DetalleVenta", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Venta", Id_Venta),
                ComandoSQL.Parameters.AddWithValue("@Id_Inventario", Id_Inventario),
                ComandoSQL.Parameters.AddWithValue("@Cantidad", Cantidad),
                ComandoSQL.Parameters.AddWithValue("@Precio_Compra", Precio_Compra),
                ComandoSQL.Parameters.AddWithValue("@Descuento", Descuento),
                ComandoSQL.Parameters.AddWithValue("@Total", Total),
                ComandoSQL.Parameters.AddWithValue("@Estatus", Estatus));
        }

        public static void PagosVentaDetalle(int Id_Venta, decimal ventatotal, decimal Efectivo_Recibido, decimal SaldoActual, decimal Efectivo_Regresado, int Empleado)
        {
            AccesoDatos.ExecuteNonQuery("Sp_GuardaDetallePagosVenta", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Venta", Id_Venta),
                ComandoSQL.Parameters.AddWithValue("@Venta_Total", ventatotal),
                ComandoSQL.Parameters.AddWithValue("@Efectivo_Recibido", Efectivo_Recibido),
                ComandoSQL.Parameters.AddWithValue("@Saldo_Actual", SaldoActual),
                ComandoSQL.Parameters.AddWithValue("@Efectivo_Regresado", Efectivo_Regresado),
                ComandoSQL.Parameters.AddWithValue("@Empleado", Empleado));
                ComandoSQL.Parameters.Clear();
        }


        #endregion

        #region Credito

        public static void CreditoGuardar(int Id_Cliente, int Id_Venta,decimal Monto_Credito, int Id_Credito)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Guarda_Credito", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Cliente", Id_Cliente),                
                ComandoSQL.Parameters.AddWithValue("@Id_Venta", Id_Venta),                
                ComandoSQL.Parameters.AddWithValue("@Monto_Credito", Monto_Credito),
                ComandoSQL.Parameters.AddWithValue("@Id_Credito", Id_Credito));
        }

        #endregion

        
        #region Caja

        public static void CajaGuardaMovimientoCaja(decimal Ingresos, int Id_Empleado,int Id_Estado,int Id_AperturaCaja, int idCaja)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Guarda_MovimientoCaja", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Ingresos", Ingresos),
                ComandoSQL.Parameters.AddWithValue("@Id_Usuario", Id_Empleado),
                ComandoSQL.Parameters.AddWithValue("@Estado", Id_Estado),
                ComandoSQL.Parameters.AddWithValue("@Id_AperturaCaja", Id_AperturaCaja),
                ComandoSQL.Parameters.AddWithValue("@Id_Caja", idCaja));
        }


        public static void CajaGuardar(string Descripcion,string Tema, string Serial, string Impresora_Ticket, string Impresora_A4, int Id_Empleado, string Nombre_PC,string IP,string Tipo)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Guardar_Caja", CommandType.StoredProcedure, ConexionString, 
                ComandoSQL.Parameters.AddWithValue("@Descripcion", Descripcion),
                ComandoSQL.Parameters.AddWithValue("@Tema", Tema),
                ComandoSQL.Parameters.AddWithValue("@Serial", Serial),
                ComandoSQL.Parameters.AddWithValue("@Impresora_Ticket", Impresora_Ticket),
                ComandoSQL.Parameters.AddWithValue("@Impresora_A4", Impresora_A4),
                ComandoSQL.Parameters.AddWithValue("@Id_Empleado", Id_Empleado),          
                ComandoSQL.Parameters.AddWithValue("@Nombre_PC", Nombre_PC),
                ComandoSQL.Parameters.AddWithValue("@IP", IP),
                ComandoSQL.Parameters.AddWithValue("@Tipo", Tipo)
                );
        }

        public static void CajaApertura(string Serial, int Id_Empleado, int NumeroTurno, string Nombre_PC,string IP)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Guarda_AperturaCaja", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Serial", Serial),
                ComandoSQL.Parameters.AddWithValue("@Id_Empleado", Id_Empleado),
                ComandoSQL.Parameters.AddWithValue("@NumeroTurno", NumeroTurno),
                ComandoSQL.Parameters.AddWithValue("@Nombre_PC", Nombre_PC),
                ComandoSQL.Parameters.AddWithValue("@IP", IP)
                );
        }

        public static void CajaCerrarTurno(int Empleado, int idCaja,int idApertura )
        {
            AccesoDatos.ExecuteNonQuery("Sp_Actualiza_MovimientoCaja", CommandType.StoredProcedure, ConexionString,

                ComandoSQL.Parameters.AddWithValue("@IdEpmpleado", Empleado),
                ComandoSQL.Parameters.AddWithValue("@Id_Caja", idCaja),
                ComandoSQL.Parameters.AddWithValue("@Id_Apertura", idApertura));
        }

        #endregion


        #region EMPRESA

        public static void EmpresaGuardar(string Nombre, string RazonSocial, int Id_Impuesto, decimal Porcentaje_Impuesto,
            int Id_Moneda, string Trabaja_Impuesto, string Modo_Busqueda, string Carpeta_Copia_Seguridad, string Correo_EnvioReportes, string Ultima_Fecha_Copia_Seguridad,
            DateTime Ultima_Fecha_Copia_Date, int Frecuencia_Copias, int Id_Estatus, string Tipo_Empresa, string Redondeo_Total,byte[] Logo, string Calle, string Numero_Interno,
            string Numero_Externo, string Entre_Calles, int Id_Colonia, int Codigo_Postal, int Id_Municipio, int Id_Estado, int Id_Pais, string Direccion)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Insertar_Empresa", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nombre", Nombre),
                ComandoSQL.Parameters.AddWithValue("@RazonSocial", RazonSocial ),               
                ComandoSQL.Parameters.AddWithValue("@Id_Impuesto", Id_Impuesto),
                ComandoSQL.Parameters.AddWithValue("@Porcentaje_Impuesto", Porcentaje_Impuesto),
                ComandoSQL.Parameters.AddWithValue("@Id_Moneda", Id_Moneda),
                ComandoSQL.Parameters.AddWithValue("@Trabaja_Impuesto", Trabaja_Impuesto ),
                ComandoSQL.Parameters.AddWithValue("@Modo_Busqueda", Modo_Busqueda),
                ComandoSQL.Parameters.AddWithValue("@Carpeta_Copia_Seguridad", Carpeta_Copia_Seguridad),
                ComandoSQL.Parameters.AddWithValue("@Correo_EnvioReportes", Correo_EnvioReportes),
                ComandoSQL.Parameters.AddWithValue("@Ultima_Fecha_Copia_Seguridad", Ultima_Fecha_Copia_Seguridad),
                ComandoSQL.Parameters.AddWithValue("@Ultima_Fecha_Copia_Date", Ultima_Fecha_Copia_Date),
                ComandoSQL.Parameters.AddWithValue("@Frecuencia_Copias", Frecuencia_Copias),
                ComandoSQL.Parameters.AddWithValue("@Id_Estatus", Id_Estatus),
                ComandoSQL.Parameters.AddWithValue("@Tipo_Empresa", Tipo_Empresa),                
                ComandoSQL.Parameters.AddWithValue("@Redondeo_Total", Redondeo_Total),       
                ComandoSQL.Parameters.AddWithValue("@Logo", Logo),
                ComandoSQL.Parameters.AddWithValue("@Calle", Calle),
                ComandoSQL.Parameters.AddWithValue("@Numero_Interno", Numero_Interno),
                ComandoSQL.Parameters.AddWithValue("@Numero_Externo", Numero_Externo),
                ComandoSQL.Parameters.AddWithValue("@Entre_Calles", Entre_Calles),
                ComandoSQL.Parameters.AddWithValue("@Id_Colonia", Id_Colonia),
                ComandoSQL.Parameters.AddWithValue("@Codigo_Postal", Codigo_Postal),
                ComandoSQL.Parameters.AddWithValue("@Id_Municipio", Id_Municipio),
                ComandoSQL.Parameters.AddWithValue("@Id_Estado", Id_Estado),                
                ComandoSQL.Parameters.AddWithValue("@Id_Pais", Id_Pais),
                ComandoSQL.Parameters.AddWithValue("@Direccion", Direccion));
            ComandoSQL.Parameters.Clear();

        }
        #endregion

        private static string mensaje;
        public static string Sucursal(string Nombre,string Telefono,string Correo,int IdEmpresa,string Calle,string Numero_Interno,string Numero_Externo,string Entre_Calles,
                                    int IdColonia,int CodigoPostal,int IdMunicipio,int IdEstado,int IdPais, string Direccion)
        {
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Insertar_Sucursales", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@Nombre", Nombre),
                    ComandoSQL.Parameters.AddWithValue("@Telefono", Telefono),
                    ComandoSQL.Parameters.AddWithValue("@Correo", Correo),
                    ComandoSQL.Parameters.AddWithValue("@IdEmpresa", IdEmpresa),
                    ComandoSQL.Parameters.AddWithValue("@Calle", Calle),
                    ComandoSQL.Parameters.AddWithValue("@Numero_Interno", Numero_Interno),
                    ComandoSQL.Parameters.AddWithValue("@Numero_Externo", Numero_Externo),
                    ComandoSQL.Parameters.AddWithValue("@Entre_Calles", Entre_Calles),
                    ComandoSQL.Parameters.AddWithValue("@IdColonia", IdColonia),
                    ComandoSQL.Parameters.AddWithValue("@CodigoPostal", CodigoPostal),
                    ComandoSQL.Parameters.AddWithValue("@IdMunicipio", IdMunicipio),
                    ComandoSQL.Parameters.AddWithValue("@IdEstado", IdEstado),
                    ComandoSQL.Parameters.AddWithValue("@IdPais", IdPais),
                    ComandoSQL.Parameters.AddWithValue("@Direccion", Direccion));
                ComandoSQL.Parameters.Clear();
                return mensaje = "Insertado";
            }
            catch (Exception ex)
            {

                return mensaje=ex.Message;
            }
        }

        public static string PaisGuardar(string nompais, string descripcio)
        {
            string mensaje;
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_InsertarPais", CommandType.StoredProcedure, ConexionString,

                ComandoSQL.Parameters.AddWithValue("@NombrePais", nompais),
                ComandoSQL.Parameters.AddWithValue("@Descripcion", descripcio));
                return mensaje = "Insertado";
                
            }
            catch (Exception ex)
            {
              return  mensaje = ex.Message;
                
            }         

        }


        public static string EstadoInsertar(string estado, string Descripcion, int Pais)
        {
            string mensaje;
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Insertar_Estados", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@Nombre", estado),
                    ComandoSQL.Parameters.AddWithValue("@Descripcion", Descripcion),
                    ComandoSQL.Parameters.AddWithValue("@IdPais", Pais));
              return  mensaje = "Insertado";
            }
            catch (Exception ex)
            {

              return  mensaje=ex.Message;
            }
        }
        
        public static string MunicipioInsertar(string municipio, string descripcion, int idEstado)
        {
            string mensaje;
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Insertar_Municipio", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@Nombre", municipio),
                    ComandoSQL.Parameters.AddWithValue("@Descripcion", descripcion),
                    ComandoSQL.Parameters.AddWithValue("@IdEstado", idEstado));
                return mensaje = "Insertado";
            }
            catch (Exception ex)
            {

                return mensaje=ex.Message;
            }
        }


        public static string ColoniaGuardar(string colonia, string descripcion, int idmunicipio, int codigopostal)
        {
            string mensaje;
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Insertar_Colonia", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@NombreColonia", colonia),
                    ComandoSQL.Parameters.AddWithValue("@Descripcion", descripcion),
                    ComandoSQL.Parameters.AddWithValue("@IdMunicipio", idmunicipio),
                    ComandoSQL.Parameters.AddWithValue("@CodigoPostal",codigopostal));
                return mensaje = "Insertado";
            }
            catch (Exception ex)
            {

                return mensaje = ex.Message;
            }
        }

    }
}
