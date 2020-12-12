using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SCL;

namespace SistemaVentas.Clases.SQL.Transacciones
{
    class ClsEditar:SqlConexion
    {


        private static string mensaje;


        #region EMPRESA

        public static void EmpresaActualizar(int IdEmpresa, string Nombre, string RazonSocial, int Id_Impuesto, decimal Porcentaje_Impuesto,
            int Id_Moneda, string Trabaja_Impuesto, string Modo_Busqueda, string Carpeta_Copia_Seguridad, string Correo_EnvioReportes, string Ultima_Fecha_Copia_Seguridad,
            DateTime Ultima_Fecha_Copia_Date, int Frecuencia_Copias, int Id_Estatus, string Tipo_Empresa, string Redondeo_Total, byte[] Logo, string Calle, string Numero_Interno,
            string Numero_Externo, string Entre_Calles, int Id_Colonia, int Codigo_Postal, int Id_Municipio, int Id_Estado, int Id_Pais, string Direccion)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Actualizar_Empresa", CommandType.StoredProcedure, ConexionString,
                ComandoSQL.Parameters.AddWithValue("@IdEmpresa", IdEmpresa),
                ComandoSQL.Parameters.AddWithValue("@Nombre", Nombre),
                ComandoSQL.Parameters.AddWithValue("@RazonSocial", RazonSocial),
                ComandoSQL.Parameters.AddWithValue("@Id_Impuesto", Id_Impuesto),
                ComandoSQL.Parameters.AddWithValue("@Porcentaje_Impuesto", Porcentaje_Impuesto),
                ComandoSQL.Parameters.AddWithValue("@Id_Moneda", Id_Moneda),
                ComandoSQL.Parameters.AddWithValue("@Trabaja_Impuesto", Trabaja_Impuesto),
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
        public static string Sucursal(int IdSucursal, string Nombre, string Telefono, string Correo, int IdEmpresa, string Calle, string Numero_Interno, string Numero_Externo, string Entre_Calles,
                                    int IdColonia, int CodigoPostal, int IdMunicipio, int IdEstado, int IdPais, string Direccion)
        {
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Editar_Sucursal", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@Id_Sucursal", IdSucursal),
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
                return mensaje = "Actualizado";
            }
            catch (Exception ex)
            {

                return mensaje = ex.Message;
            }
        }
    }
}
