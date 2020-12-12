using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SistemaVentas.PruebasConexion
{
    public class DatoAcceso:IDisposable
    {
        private Conexion conn;
        private IDataProvider access;
        private IntPtr handle;
        public static bool useWebService { get; set; }
        public static string wsUrl { get; set; }
        private static WebServiceClientList<WebServicioCliente> wsClients;



        public DatoAcceso(Conexion connection)
        {
            GC.Collect();
            conn = connection;
            if (conn.tipoConexion == TipoConexion.MySql)
            {
                //access = new BDMySQL(conn.GetConnectionString());
            }
            else if (conn.tipoConexion == TipoConexion.SqlServer)
            {
                //access = new DbSqlServer(conn.GetConnectionString());
            }
        }

        public DatoAcceso()
           : this(GetConnection(0))
        {
        }
        public DatoAcceso(int conID)
            : this(GetConnection(conID))
        {

        }
        public DatoAcceso(string conName)
            : this(GetConnection(/*conName*/))
        {
        }
        public void Dispose()
        {
            try
            {
                access.Dispose();
                CloseHandle(handle);
                handle = IntPtr.Zero;
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {
            }


        }
        ~DatoAcceso()
        {
            this.Dispose();
        }
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);


        private static Conexion GetConnection(int connectionId)
        {
            var tmpConnection = Conexiones.FindConnection(connectionId);
            if (tmpConnection == null) throw new Exception("ConnectionIdName value not found");
            return tmpConnection;
        }

        private static Conexion GetConnection()
        {
            return GetConnection(0);
        }

        public static DataTable ToTable(string commandText, CommandType commandType, int connectionId, params DalParametros[] parameters)
        {
            if (useWebService)
            {
                //codigo para traer los datos por el web service
                //webLogin();
                WebServicioCliente wsLocalClient = webClientLogin();
                var wsResult = wsLocalClient.ToDataTable(commandText, commandType, connectionId, parameters);
                wsLocalClient.isBusy = false;
                return wsResult;
            }

            Conexion conn = GetConnection(connectionId);
            return ToTable(commandText, commandType,Convert.ToInt32( conn), parameters);
        }

        private static WebServicioCliente webClientLogin()
        {
            wsUrl = (wsUrl) ?? "http://187.160.254.67:8080";
            WebServicioCliente wsLocalClient;
            if (wsClients == null)
                wsClients = new WebServiceClientList<WebServicioCliente>();
            wsLocalClient = wsClients.cliente_libre("sistemas", "locabus", wsUrl + "/locabus/Login.php", wsUrl + "/locabus/Data.php", wsUrl + "/locabus/Logout.php");
            if (!wsLocalClient.isConected())
            {
                wsLocalClient = new WebServicioCliente("sistemas", "locabus", wsUrl + "/locabus/Login.php", wsUrl + "/locabus/Data.php", wsUrl + "/locabus/Logout.php");
                if (!wsLocalClient.isConected())
                {
                    wsLocalClient.isBusy = false;
                    throw new Exception("No se puede conectar al WebService");
                    //return null;
                }
            }
            return wsLocalClient;
        }

     
        

    }
}
