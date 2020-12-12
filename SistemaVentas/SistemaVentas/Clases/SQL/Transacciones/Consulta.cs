using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SistemaVentas.Clases.Entidates;
using System.Windows.Forms;
using SistemaVentas.PruebasConexion;
using System.Collections;

namespace SistemaVentas.Clases.SQL
{
    public class Consulta:SqlConexion
    {
       private static DataTable DtRegistros;
        private static DataSet dataSet;


        #region Importacion de Datos a SQL
      
        public static string Immportar(DataSet dataSet, int NumTable, string NombreTabla)
        {
            string Mensaje = null;
            try
            {
                SqlBulkCopy bulkCopy = default(SqlBulkCopy);
                bulkCopy = new SqlBulkCopy(ConexionString);
                bulkCopy.DestinationTableName = NombreTabla;
                bulkCopy.WriteToServer(dataSet.Tables[NumTable]);

                Mensaje = "El Archivo se ha Exportado correctamente";
            }
            catch (Exception ex )
            {

                Mensaje= ex.Message;
            }

            return Mensaje;
        }

        #endregion

        #region Lista de Tablas de SQL
      
        public static DataTable EntidadesLista()
        {
            return AccesoDatos.ToTable("SELECT  0 AS 'Id_Entidades', 'Seleccione...' AS 'Nombre'," +
                " 'Descripcion' AS 'Descripcion' FROM Entidades UNION SELECT * FROM Entidades", CommandType.Text, ConexionString);
        }

        #endregion

        #region Empleado

        public static DataTable AutenticarPermisos(int Nomina, string Contrseña)
        {
            return AccesoDatos.ToTable("Sp_ValidacionPermisos",CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina),
                ComandoSQL.Parameters.AddWithValue("@Contraseña", Contrseña));
        }

        public static DataTable ConsultaEmpleados()
        {
                return AccesoDatos.ToTable("Sp_Consulta_Empleados",CommandType.StoredProcedure, ConexionString );
        }

        //public static DataTable ConsultaEmpleados2()
        //{
        //    return DatoAcceso.ToTable("Sp_Consulta_Empleados", CommandType.StoredProcedure, 1);
        //}

        public static DataTable dtConsultaPuestos(int IdArea)
        {
            return AccesoDatos.ToTable("Sp_Consulta_Puestos",CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Area", IdArea));
        }

        public static DataTable SucursalListCombo(int IdEmpresa)
        {
            return AccesoDatos.ToTable("Sp_Consulta_SucursalXEmpresa_Combo", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@IdEmpresa", IdEmpresa));
        }

        public static DataTable EstatusEmple()
        {
            return AccesoDatos.ToTable("Sp_Consulta_EstatusEmpleado", CommandType.StoredProcedure, ConexionString);
        }

      
        public static int NumLog()
        {
            return Convert.ToInt32(AccesoDatos.ExecuteScalar("Sp_ObtenerID_Log_Accesos", CommandType.StoredProcedure, ConexionString));

        }

        public static DataTable EmpleadoEnviarCorreo(int Nomina)
        {
            return AccesoDatos.ToTable("Sp_Consultar_CorreoContraseña", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina));
        }
        public static int NominaEmpleado()
        {
            return Convert.ToInt32(AccesoDatos.ExecuteScalar("Sp_Obtener_IdEmpleados", CommandType.StoredProcedure, ConexionString));
        }

        public static DataTable NominaEmpleadoBuscar(int Nomina)
        {
            return AccesoDatos.ToTable("Sp_BuscarEmpleado_Nomina", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina));
            
        }

        public static DataTable NominaUsuarioBuscar(int Nomina)
        {
            return AccesoDatos.ToTable("Sp_BuscarUsuario_Nomina", CommandType.StoredProcedure, ConexionString,
              ComandoSQL.Parameters.AddWithValue("@Nomina", Nomina));
        }

        public static DataTable ConsultaUsuarios()
        {
            return AccesoDatos.ToTable("Sp_ConsultaUsuarios", CommandType.StoredProcedure, ConexionString);
          
        }

        /// <summary>
        /// Consulta los 5 Usuarios iniciados recientemente Tabla Log_LoginUser
        /// </summary>
        /// <returns></returns>
        public static DataTable LogSesionesRecientes(string NombrePC)
        {
            return AccesoDatos.ToTable("Sp_Consulta_LoginRecientes", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("",NombrePC));
        }

        public static DataTable Login(string Usuario, string Contraseña)
        {
             return AccesoDatos.ToTable("Sp_LoginUsuario", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Usuario", Usuario),
                ComandoSQL.Parameters.AddWithValue("@Contraseña", Contraseña));

            #region Preuba Anterior


            /*SqlDataReader lectura = ComandoSQL.ExecuteReader();
            if (lectura.HasRows)
            {
                while (lectura.Read())
                {
                    UsuarioCache.Id_Usuario = lectura.GetInt32(0);
                    UsuarioCache.Nomina = lectura.GetInt32(1);
                    UsuarioCache.Nombre = lectura.GetString(2);
                    UsuarioCache.Apellido_Paterno = lectura.GetString(3);
                    UsuarioCache.Apellido_Materno = lectura.GetString(4);
                    UsuarioCache.CURP = lectura.GetString(5);
                    UsuarioCache.RFC = lectura.GetString(6);
                    UsuarioCache.Fecha_Nacimiento = lectura.GetDateTime(7);
                    UsuarioCache.Telefono = lectura.GetString(8);
                    UsuarioCache.Celular = lectura.GetString(9);
                    UsuarioCache.Email = lectura.GetString(10);
                    UsuarioCache.Usuario_Nombre = lectura.GetString(11);
                    UsuarioCache.Contraseña = lectura.GetString(12);
                    UsuarioCache.Puesto = lectura.GetInt32(13);
                    UsuarioCache.Calle = lectura.GetString(14);
                    UsuarioCache.Numero_Interno = lectura.GetString(15);
                    UsuarioCache.Numero_Exterior = lectura.GetString(16);
                    UsuarioCache.Entre_Calles = lectura.GetString(17);
                    UsuarioCache.Colonia = lectura.GetString(18);
                    UsuarioCache.Codigo_Postal = lectura.GetInt32(19);
                    UsuarioCache.Municipio = lectura.GetString(20);
                    UsuarioCache.Estado = lectura.GetString(21);
                    UsuarioCache.Pais = lectura.GetString(22);
                    UsuarioCache.PuestoUsuario = lectura.GetString(25);
                    byte[] imagen = (byte[])lectura[23];
                    UsuarioCache.Imagen = imagen;
                }
                return true;
            }
            else
            {
                return false;
            }*/
            #endregion
        }

        public static bool ValidarUsuario(string NombreUsuario, string Contraseña)
        {//no se esta utilizando
            DtRegistros = Login(NombreUsuario, Contraseña);
            if (DtRegistros.Rows.Count > 0)
            {
                UsuarioCache.Nombre = DtRegistros.Rows[0]["Nombre"].ToString();
                UsuarioCache.Apellido_Paterno = DtRegistros.Rows[0]["Apellido_Paterno"].ToString();
                UsuarioCache.Apellido_Materno = DtRegistros.Rows[0]["Apellido_Materno"].ToString();
                UsuarioCache.Email = DtRegistros.Rows[0]["Correo"].ToString();
                UsuarioCache.Contraseña = DtRegistros.Rows[0]["Contraseña"].ToString();
                UsuarioCache.Usuario_Nombre = DtRegistros.Rows[0]["Usuario_Name"].ToString();
                UsuarioCache.PuestoUsuario = DtRegistros.Rows[0]["Puesto"].ToString();
                UsuarioCache.Puesto = Convert.ToInt32(DtRegistros.Rows[0]["Id_Puesto"].ToString());
                UsuarioCache.Nomina = Convert.ToInt32(DtRegistros.Rows[0]["Nomina"].ToString());
                byte[] imagen = (byte[])(DtRegistros.Rows[0]["Foto"]);
                UsuarioCache.Imagen = imagen;
          


                return true;
            }
            return false;
        }
        #endregion

        #region Articulos

        public static DataTable ArticuloBuscaNombre(string BuscarNomArt)
        {
            return AccesoDatos.ToTable("Sp_Buscar_DescripcionArticulo", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nombre", BuscarNomArt));
        }

        public static DataTable ArticuloBuscarNombreAutocomplete()
        {
            return AccesoDatos.ToTable("SELECT TOP 100 Nombre FROM Articulo", CommandType.Text, ConexionString);

        }

        public static AutoCompleteStringCollection CargarAutoComplete()
        {
            DtRegistros = ArticuloBuscarNombreAutocomplete();
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            foreach (DataRow Fila in DtRegistros.Rows)
            {
                autoComplete.Add(Convert.ToString(Fila["Nombre"]));
            }

            return autoComplete;
        }

        public static DataTable ArticuloConsulta()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Articulos", CommandType.StoredProcedure, ConexionString);
        }

        public static DataSet Articulo_DataSet()
        {
            DataSet dataSet;

            dataSet= AccesoDatos.ToDataSet("Sp_Consulta_Articulos", CommandType.StoredProcedure, ConexionString);
            dataSet.Tables[0].TableName = "dtAdaptArticulo";

            return dataSet;

        }

        public static DataTable CodigoBuscarTblArticulo(string Codigo)
        {
            return AccesoDatos.ToTable("Sp_Buscar_CodigoTblArticulo", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Codigo", Codigo));
        }

        public static DataTable ArticuloBuscarXdescripcion( string Descripcion)
        {
            return AccesoDatos.ToTable("Sp_BuscarArticuloXdescripcion", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@DescripcionArticulo", Descripcion));
        }

        public static DataTable PresentacionArticulo()
        {
            return AccesoDatos.ToTable("Sp_Consulta_PrestArticulo", CommandType.StoredProcedure, ConexionString);
        }

        public static DataTable Unidad_MedidaArt()
        {
            
            return AccesoDatos.ToTable("Sp_Consulta_UnidMedida", CommandType.StoredProcedure, ConexionString);
        }

        public static DataTable AgranelClasif()
        {
            
            return AccesoDatos.ToTable("Sp_Consulta_ClasfAgranel", CommandType.StoredProcedure, ConexionString);
        }


        public static DataTable FabricanteConsulta()
        {

            return AccesoDatos.ToTable("Sp_Consulta_Fabricante", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Proveedores
        public static DataTable ProveedoresLista()
        {
            return AccesoDatos.ToTable("Sp_Lista_Proveedores", CommandType.StoredProcedure, ConexionString);
        }
        #endregion

        #region Comprobantes

        public static DataTable ComprobantesLista()
        {
            return AccesoDatos.ToTable("Sp_Lista_Comprobantes", CommandType.StoredProcedure, ConexionString);
        }
        #endregion

        #region Inventario

        /// <summary>
        /// Buscar Articulo por Codigo en la Tabla de Inventario
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public static DataTable InventarioBuscaCodArt(string Codigo)
        {
            return AccesoDatos.ToTable("Sp_Buscar_ArticuloInventario", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Codigo", Codigo));
        }

        #endregion

        #region Entradas

        public static DataTable EntradasLista()
        {
            return AccesoDatos.ToTable("Sp_Consulta_ListaEntradas", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Ventas

        public static int Venta_Numero()
        {
            return Convert.ToInt32(AccesoDatos.ExecuteScalar("Sp_Obtener_IdVenta", CommandType.StoredProcedure, ConexionString));
        }
        public static DataSet Ticket_VentaDetalle(int NumVenta)
        {
            DataSet dataSet ;
            dataSet= AccesoDatos.ToDataSet("Sp_Ticket_VentaDetalle", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Venta", NumVenta));
            dataSet.Tables[0].TableName = "TicketDetalleVenta";
            dataSet.Tables[1].TableName = "TicketVenta";
            dataSet.Tables[2].TableName = "TicketPagosDetalle";
            return dataSet;
        }
        public static DataSet Ticket_Credito(int NumVenta, int Id_Cliente)
        {
            DataSet dataSet;
            dataSet = AccesoDatos.ToDataSet("Sp_Ticket_CreditoVenta", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Venta", NumVenta),
                ComandoSQL.Parameters.AddWithValue("@Id_Cliente", Id_Cliente));
            dataSet.Tables[0].TableName = "TicketDetalleVenta";
            dataSet.Tables[1].TableName = "TicketVenta";
            dataSet.Tables[2].TableName = "TicketPagosDetalle";
            dataSet.Tables[3].TableName = "TicketSaldoCredito";

            return dataSet;

        }
        /// <summary>
        /// Consulta Las Ventas que el Cajero ha suspendido.Solo sus ventas.
        /// </summary>
        /// <param name="IdEmpledo"></param>
        /// <returns></returns>
        public static DataTable VentaSuspendidos(int IdEmpledo)
        {
            return AccesoDatos.ToTable("Sp_Consulta_VentasSuspendidos", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Empleado", IdEmpledo));
        }

        /// <summary>
        /// Consulta La Venta Suspendido y el Datelle de esa Venta
        /// </summary>
        /// <param name="NumeroVentaSuspendido"></param>
        /// <returns></returns>
        public static DataSet VentaReanudarVentaSuspendido(int NumeroVentaSuspendido)
        {
                dataSet = AccesoDatos.ToDataSet("Sp_Consulta_ReanudarVentaSuspendido", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Numero_Venta", NumeroVentaSuspendido));
                dataSet.Tables[0].TableName = "DetalleVentaSuspendido";
                dataSet.Tables[1].TableName =  "VentaSuspendido";
            
            return dataSet;
        }

        #endregion

        #region Creditos

        public static int Credito_Numero()
        {
            return Convert.ToInt32(AccesoDatos.ExecuteScalar("Sp_Obtener_IdCredito", CommandType.StoredProcedure, ConexionString));
        }

        public static DataTable Credito_ConsultaNumCliente(int IdCliente)
        {
            return AccesoDatos.ToTable("Sp_Consultar_CreditoNumero", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Id_Cliente", IdCliente));
        }
        #endregion

        #region Caja

        public static DataTable CajaAperturas(int Empleado)
        {
            return AccesoDatos.ToTable("Sp_ConsultaAperturaCaja", CommandType.StoredProcedure, ConexionString,                
                ComandoSQL.Parameters.AddWithValue("@Id_Empleado", Empleado));
        }

        public static DataTable CajaConsulta()
        {
            return AccesoDatos.ToTable("Select * from Caja", CommandType.Text, ConexionString);
        }

        public static int AperturaNumero()
        {
            return Convert.ToInt32(AccesoDatos.ExecuteScalar("Sp_Obtner_IdApertura", CommandType.StoredProcedure,ConexionString));
        }
        public static int CajaNumero()
        {
            return Convert.ToInt32(AccesoDatos.ExecuteScalar("Sp_Obtner_IdCaja", CommandType.StoredProcedure, ConexionString));
        
        }


        #endregion

        #region Empresa

       public   static DataTable EmpresaRegistros()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Empresas", CommandType.StoredProcedure, ConexionString);
        }

        public static DataTable EmpresaCombobox()
        {
            return AccesoDatos.ToTable("Sp_Consultar_EmpresaCombobox", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Sucursal
       
        public static DataTable Sucursales()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Sucursales", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Pais

    
        public static DataTable PaisConsulta()
        {
            return AccesoDatos.ToTable("SELECT * FROM Pais", CommandType.Text, ConexionString);
        }

        public static DataTable PaisListCombo()
        {
            return AccesoDatos.ToTable("Sp_Consulta_PaisCombox", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Estados

       
        public static DataTable Estados()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Estados", CommandType.StoredProcedure, ConexionString);
        }
        public static DataTable Estados(int Pais)
        {
            return AccesoDatos.ToTable("Sp_Consulta_EstadoBuscarId", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@IdPais", Pais));
        }

        #endregion

        #region Municipios

       
        public static DataTable MunicipioConsulta()
        {
            return AccesoDatos.ToTable("Sp_Consultar_Municipios", CommandType.StoredProcedure, ConexionString);
        }

        public static DataTable MunicipioListCombo(int IdEstado)
        {
            return AccesoDatos.ToTable("Sp_Consulta_MunicipioCombox", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@IdEstado", IdEstado));
        }

        #endregion

        #region Colonias

       
        public static DataTable Colonias()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Colonias", CommandType.StoredProcedure, ConexionString);
        }

        public static DataTable Colonias(int CodigoPostal)
        {
            return AccesoDatos.ToTable("Sp_Consulta_CodigoPostal", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@CodigoPostal", CodigoPostal));
        }
              

        public static DataTable ColoniasPorMunicipio(int IdMunicipio)
        {
            return AccesoDatos.ToTable("Sp_Consulta_Colonia_IdMunicipio", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@IdMunicipio", IdMunicipio));
        }

        public static DataSet ColoniaTablas(int CodigoPostal)
        {
          
            dataSet= AccesoDatos.ToDataSet("Sp_Consulta_CodigoPostal", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@CodigoPostal", CodigoPostal));
            dataSet.Tables[0].TableName = "Colonias";
            dataSet.Tables[1].TableName = "Pais";
            dataSet.Tables[2].TableName = "Estado";
            dataSet.Tables[3].TableName = "Municipio";

            return dataSet;
        }
        #endregion

        #region Monedas
       
        public static DataTable MonedaList()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Moneda", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Impuestos

        
        public static DataTable ImpuestoList()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Impuestos", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Areas

       
        public static DataTable AreaComboLista()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Areas_Combo", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Caja Equipo
      
        public static DataTable CajaEquipo(string Nombre_PC, string IP, string Serial)
        {
            return AccesoDatos.ToTable("Sp_Consulta_EquipoCaja", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Nombre_PC", Nombre_PC),
                ComandoSQL.Parameters.AddWithValue("@IP", IP),
                ComandoSQL.Parameters.AddWithValue("@Serial",Serial));
        }

        #endregion

        #region Catalogo Parametros Entrada
     
        public static DataTable EntradaTiposLista()
        {
            return AccesoDatos.ToTable("Sp_Consulta_TipoEntradas", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region Inventario      

        public static DataTable InventarioLista()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Inventario", CommandType.StoredProcedure, ConexionString);
        }

        #endregion

        #region KARDEX

        public static DataTable KardexRegistros()
        {
            return AccesoDatos.ToTable("Sp_Consulta_Kerdex", CommandType.StoredProcedure,ConexionString);
        }
        #endregion
      

        public static DataTable GrafCategoria()
        {

            return AccesoDatos.ToTable("ProductosPresentacion", CommandType.StoredProcedure, ConexionString);
           
        }

        public static DataTable GrafProductos()
        {

            return AccesoDatos.ToTable("Sp_Productos_Preferidos", CommandType.StoredProcedure, ConexionString);

        }

        public static DataTable Deshboard()
        {
            return AccesoDatos.ToTable("Sp_ConsultaDeshboard", CommandType.StoredProcedure, ConexionString);

        }
    }
}
