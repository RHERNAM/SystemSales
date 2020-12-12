using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Formularios.Administracion;
using SistemaVentas.Formularios.Configuraciones;
using SistemaVentas.Formularios.Sistemas;
using SistemaVentas.Formularios.Ventas;
using SistemaVentas.PruebasConexion;
namespace SistemaVentas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Conexion();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormLogin());
            Application.Run(new FormConexionSQL());

        }

        private static void Conexion()
        {
            try
            {
                //conexion mysql
                //DataTable tabla = DataAccess.ToTable("select * from TSICON", CommandType.Text);


                //conexion sql
                //string cnn = "Data Source = 192.168.1.105; initial catalog = SISTEMAS; user id =sa; password = Camara20";
                //string consulta = "select * from TSICON";
                //SqlDataAdapter Da = new SqlDataAdapter(consulta, cnn);

                DataTable tabla = ConexionSQL("SELECT * FROM BaseDatos_Conexiones");


                foreach (DataRow dr in tabla.Rows)
                {
                    Conexion c = new Conexion();
                    c.ConnectionId = Convert.ToInt32(dr["IdConexion"]);
                    c.ConnectionName = dr["Nombre"].ToString();
                    c.tipoConexion = (dr["Tipo"].ToString() == "MYSQL") ? TipoConexion.MySql : TipoConexion.SqlServer;
                    c.DatabaseName = dr["Base_Datos"].ToString();
                    c.DataSource = dr["Servidor"].ToString();
                    c.User = dr["Usuario"].ToString();
                    c.Password = dr["Contraseña"].ToString();
                    c.Pooling = false;
                    c.ConnectTimeout = 60;
                    c.Encryption = Convert.ToInt16(dr["Encriptado"]);
                    Conexiones.RunTimeList.Add(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private static DataTable ConexionSQL(string Consulta)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection CadenaConexion = new SqlConnection("Data Source =DESKTOP-D7Q7TJ7; Initial Catalog = SistemasRechs; Integrated Security = True");//"Data Source = 192.168.1.105; initial catalog = SISTEMAS; user id =sa; password = Camara20");
                CadenaConexion.Open();

                SqlDataAdapter Dp = new SqlDataAdapter(Consulta, CadenaConexion);

                Dp.Fill(dt);

                CadenaConexion.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al conectarse a la base de datos", "Error");
            }

            return dt;
        }

    }
}
