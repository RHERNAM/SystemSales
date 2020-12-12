using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using SistemaVentas.Clases.SQL.Conexion;

namespace SistemaVentas.Clases.SQL
{
    public class SqlConexion
    {
        #region RJCODE


        //private readonly string ConexionString;

        //public SqlConexion()
        //{
        //    ConexionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ToString();
        //}

        //public static string ConexionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ToString();
        //protected SqlConnection ObtenerConexion()
        //{
        //    return new SqlConnection(ConexionString);
        //}
        #endregion

        //public static string ConexionString = "Data Source = (local); DataBase=SistemaVenta; Integrated Security = True";
        //public static string ConexionString = Properties.Settings.Default.ConexionBD;
        //public static string ConexionString = ConfigurationManager.ConnectionStrings["SistemaVentas.Properties.Settings.ConexionBD"].ToString();
        public static string ConexionString =Convert.ToString(Desencryptacion.checkServer());
        public static SqlCommand ComandoSQL = new SqlCommand();
        
    }
}
