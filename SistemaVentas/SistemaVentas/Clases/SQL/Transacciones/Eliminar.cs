using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCL;

namespace SistemaVentas.Clases.SQL.Transacciones
{
    
    public class Eliminar:SqlConexion
    {
       
        public static void VentaDetalleEliminarArticulo(int NumeroVenta, int IdInventario)
        {
            AccesoDatos.ExecuteNonQuery("Sp_Eliminar_ArticuloDetalleVenta", CommandType.StoredProcedure, ConexionString,
             ComandoSQL.Parameters.AddWithValue("@NumeroVenta", NumeroVenta),
             ComandoSQL.Parameters.AddWithValue("@IdInevntario", IdInventario));
        }


        public static string Pais(int NumeroPais)
        {
            string mensaje;
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Eliminar_Pais", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@IdPais", NumeroPais));
                return mensaje = "Eliminado";
            }
            catch (Exception ex )
            {

                return mensaje=ex.Message;
            }
        }

        public static string Estados(int idEstado)
        {
            string mensaje;
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Eliminar_Estado", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@IdEstado", idEstado));
                return mensaje = "Eliminado";
            }
            catch (Exception ex)
            {

                return mensaje=ex.Message;
            }
        }

        public static string Municipio(int IdMunicipio)
        {
            string mensaje;
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Eliminar_Municipio", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@IdMunicipio", IdMunicipio));
                return mensaje = "Eliminado";
            }
            catch (Exception ex)
            {

                return mensaje = ex.Message;
            }
        }

        public static string Colonias(int IdColonia)
        {
            string mensaje;
            try
            {
                AccesoDatos.ExecuteNonQuery("Sp_Eliminar_Colonia", CommandType.StoredProcedure, ConexionString,
                    ComandoSQL.Parameters.AddWithValue("@IdColonia", IdColonia));
                return mensaje = "Eliminado";
            }
            catch (Exception ex)
            {

                return mensaje = ex.Message;
            }
        }
    }
}
