using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace SistemaVentas.PruebasConexion
{
    public  class WebServicioCliente
    {
        private WebClient wsClient;
        private Uri urlLogin;//Pagina que abre una nueva SESSION
        private Uri urlLogout;//Pagina que destruye la SESSION
        private Uri urlData;//Pagina que ejecuta, la peticion en la base de datos
        private bool bConected = false;//Estado de la conexion
        private DateTime conected;//Fecha en que se establece la conexion
        private int timeSession = 14;// tiempo maximo de conexion
        private string pass;//Passwqord de acceso
        private string user;//Usuario
        private string loginSession = "";//Variable de SESSION
        private string loginCookie = "";//Cookie que se envia al servidor para que reconozca la procedencia de la peticion
        private DataTable dtResponse;//Tabla con informacion del estado de error de la ultima peticion al WebService

        public bool isBusy { get; set; }//Esta ocupado el cliente o no
        public bool useEncryption { get; set; }//Se encriptan los parametros enviados al WebServer



        /// <summary>
        /// Inicializacion de variables y creacion de session en el WebService
        /// </summary>
        /// <param name="sUser">Usuario</param>
        /// <param name="sPass">Password de acceso</param>
        /// <param name="sUrllogin">Url para crear una nueva session</param>
        /// <param name="sUrldata">Url para el acceso a datos</param>/// 
        /// <param name="sUrllogout"> Url para cerrar la session</param>/// 
        public WebServicioCliente(string sUser, string sPass, string sUrllogin, string sUrldata, string sUrllogout)
        {
            isBusy = true;
            user = sUser;
            pass = sPass;

            urlLogin = new Uri(sUrllogin);
            urlLogout = new Uri(sUrllogout);
            urlData = new Uri(sUrldata);
            bConected = false;
            useEncryption = true;

            login();
            isBusy = false;
        }

        /// <summary>
        /// Inicializacion de variables y creacion de session en el WebService
        /// </summary>
        /// <param name="sUser">Usuario</param>
        /// <param name="sPass">Password de acceso</param>
        /// <param name="sUrllogin">Url para crear una nueva session</param>
        /// <param name="sUrldata">Url para el acceso a datos</param>/// 
        /// <param name="sUrllogout"> Url para cerrar la session</param>/// 
        /// <param name="bEncryption">Envio de parametros codificado</param>///
        public WebServicioCliente(string sUser, string sPass, string sUrllogin, string sUrldata, string sUrllogout, bool bEncryption)
        {
            isBusy = true;
            user = sUser;
            pass = sPass;

            urlLogin = new Uri(sUrllogin);
            urlLogout = new Uri(sUrllogout);
            urlData = new Uri(sUrldata);
            bConected = false;
            useEncryption = bEncryption;

            login();
            isBusy = false;
        }


        /// <summary>
        /// Abre una nueva session si es necesario
        /// </summary>        
        private void login()
        {
            string tmpResponse = "";
            NameValueCollection sendData = new NameValueCollection();
            if (!bConected)
            {
                wsClient = new WebClient();

                sendData["user"] = ((useEncryption) ? Encryption.Encrypt(user) : user);
                sendData["pass"] = ((useEncryption) ? Encryption.Encrypt(pass) : pass);
                //urlLogin = new Uri("http://192.168.1.54/locabus/Log");
                dtResponse = null;//Se pone nula la tabla de respuesta al servidor en espera de proxima respuesta
                try
                {
                    byte[] responseBytes = wsClient.UploadValues(urlLogin, "POST", sendData);
                    tmpResponse = Encoding.UTF8.GetString(responseBytes);

                    DataSet datos = JsonConvert.DeserializeObject<DataSet>(tmpResponse);
                    dtResponse = datos.Tables["response"];
                }
                catch (WebException e)
                {
                    setResponseError_ifNeeded("404", e.Message);
                    return;
                }
                catch (JsonException e)
                {
                    setResponseError_ifNeeded("13", e.Message);
                    return;
                }

                if (dtResponse.Rows[0]["error"].ToString() == "0")
                {
                    loginSession = dtResponse.Rows[0]["s"].ToString();
                    loginCookie = wsClient.ResponseHeaders["Set-Cookie"];
                    wsClient.Headers.Add("Cookie", loginCookie);
                    conected = DateTime.Now;
                    bConected = true;
                }
            }

            TimeSpan dif = DateTime.Now - conected;

            if ((dif.Days < 1 && dif.Hours < 1 && dif.Minutes > timeSession) || loginSession.Equals(""))
            {
                wsClient = new WebClient();

                sendData["user"] = ((useEncryption) ? Encryption.Encrypt(user) : user);
                sendData["pass"] = ((useEncryption) ? Encryption.Encrypt(pass) : pass);
                dtResponse = null;//Se pone nula la tabla de respuesta al servidor en espera de proxima respuesta
                try
                {
                    byte[] responseBytes = wsClient.UploadValues(urlLogin, "POST", sendData);
                    tmpResponse = Encoding.UTF8.GetString(responseBytes);

                    DataSet datos = JsonConvert.DeserializeObject<DataSet>(tmpResponse);
                    dtResponse = datos.Tables["response"];
                }
                catch (WebException e)
                {
                    setResponseError_ifNeeded("404", e.Message);
                    return;
                }
                catch (JsonException e)
                {
                    setResponseError_ifNeeded("13", e.Message);
                    return;
                }

                if (dtResponse.Rows[0]["error"].ToString() == "0")
                {
                    loginSession = dtResponse.Rows[0]["s"].ToString();
                    loginCookie = wsClient.ResponseHeaders["Set-Cookie"];
                    wsClient.Headers.Add("Cookie", loginCookie);
                    conected = DateTime.Now;
                    bConected = true;
                }
            }
        }


        /// <summary>
        /// Cierra la session
        /// </summary>
        public void logout()
        {
            if (bConected)
            {
                wsClient = new WebClient();
                NameValueCollection sendData = new NameValueCollection();
                string tmpResponse = "";
                dtResponse = null;//Se pone nula la tabla de respuesta al servidor en espera de proxima respuesta
                try
                {
                    byte[] responseBytes = wsClient.UploadValues(urlLogout, "POST", sendData);
                    tmpResponse = Encoding.UTF8.GetString(responseBytes);
                }
                catch (WebException e)
                {
                    wsClient.Dispose();
                    setResponseError_ifNeeded("404", e.Message);
                    resetCookie();
                    return;
                }

                try
                {
                    wsClient.Dispose();
                    DataSet datos = JsonConvert.DeserializeObject<DataSet>(tmpResponse);
                    dtResponse = datos.Tables["response"];
                    if (dtResponse.Rows[0]["error"].ToString() == "0")
                    {
                        //Se inicializan las variables aun si hay error
                    }
                    resetCookie();
                }
                catch (Exception e)
                {
                    wsClient.Dispose();
                    setResponseError_ifNeeded("14", "Error de logOut");
                    resetCookie();
                }
            }
        }


        private void resetCookie()
        {
            loginSession = "";
            loginCookie = "";
            wsClient.Headers.Remove("Cookie");
            conected = new DateTime();
            bConected = false;
        }

        /// <summary>
        /// Fuerza informacion de error en la tabla dtResponse si tiene valor nulo o el numero de error es 0
        /// </summary>
        /// <param name="errno">Numero de error</param>
        /// <param name="strError">Mensaje de error</param>
        private void setResponseError_ifNeeded(string errno, string strError)
        {
            if (dtResponse != null && dtResponse.Rows.Count > 0 && dtResponse.Rows[0]["error"] != null)
            {
                if (dtResponse.Rows[0]["error"].ToString() == "0")
                {
                    dtResponse.Rows[0]["error"] = errno;
                    dtResponse.Columns.Add("errstr");
                    dtResponse.Columns.Add("errfile");
                    dtResponse.Columns.Add("errline");
                    dtResponse.Columns.Add("errno");
                    dtResponse.Rows[0]["errstr"] = strError;
                    dtResponse.Rows[0]["errfile"] = "";
                    dtResponse.Rows[0]["errline"] = 0;
                    dtResponse.Rows[0]["errno"] = errno;
                }
            }
            else
            {
                dtResponse = JsonConvert.DeserializeObject<DataSet>("{\"response\":[{\"error\":" + errno + ",\"errstr\":\"" + strError + "\",\"errfile\":\"\",\"errline\":0,\"errno\":" + errno + "}]}").Tables["response"];
            }
        }

        /// <summary>
        /// Solicitud de retorno de informacion en DataTable
        /// El estado de error de la respuesta de retorno del WebService se guarda en la tabla dtResponse
        /// </summary>
        /// <param name="sQuery">instruccion a ejecutar</param>
        /// <param name="type">tipo de comando a ejecutar</param>
        /// <param name="idCon">id de conexion del DALL</param>
        /// <param name="parameters">parametros enviados al StoreProcedure</param>
        public DataTable ToDataTable(String sQuery, CommandType type, int idCon, params DalParametros[] parameters)
        {
            string tmpResponse = "";
            login();

            if (bConected)
            {
                dtResponse = null;//Se pone nula la tabla de respuesta al servidor en espera de proxima respuesta
                tmpResponse = postData(sQuery, CommandType.StoredProcedure, idCon, parameters);
                if (tmpResponse == null)//no hubo respuesta por error
                {
                    //En caso de error dtResponse obtiene su mensaje de error en postData
                    return new DataTable();
                    //return null;
                }
                try
                {
                    DataSet ds = JsonConvert.DeserializeObject<DataSet>(tmpResponse);
                    dtResponse = ds.Tables["response"];
                    ds.Tables.Remove("response");
                    return ds.Tables[0];//"Table-0"
                }
                catch (Exception e)
                {
                    setResponseError_ifNeeded("11", "No hubo Resultado");
                    return new DataTable();
                    //hubo respuesta del servidor                    
                }
            }
            setResponseError_ifNeeded("12", "No se ha establecido conexion");
            return new DataTable();
            //return null;
        }



        /// <summary>
        /// Envio de datos al WebService
        /// </summary>
        /// <param name="sQuery">instruccion a ejecutar</param>
        /// <param name="type">tipo de comando a ejecutar</param>
        /// <param name="idCon">id de conexion del DALL</param>
        /// <param name="parameters">parametros enviados al StoreProcedure</param>
        private String postData(String sQuery, CommandType type, int idCon, params DalParametros[] parameters)
        {
            String result = null;

            NameValueCollection sendData = new NameValueCollection();
            try
            {
                sendData["param"] = prepareParametro(sQuery, type, idCon, parameters);
                byte[] responseBytes = wsClient.UploadValues(urlData, "POST", sendData);
                result = Encoding.UTF8.GetString(responseBytes);
            }
            catch (Exception e)
            {//No es necesariamente un error 404 de no encontro Servidor
                setResponseError_ifNeeded("404", e.Message);
                return null;
            }
            return result;
        }

        /// <summary>
        /// Crea cadena con parametros para solicitud de acceso a datos
        /// </summary>
        /// <param name="sQuery">instruccion a ejecutar</param>
        /// <param name="type">tipo de comando a ejecutar</param>
        /// <param name="idCon">id de conexion del DALL</param>
        /// <param name="parameters">parametros enviados al StoreProcedure</param>
        private String prepareParametro(String sQuery, CommandType type, int idCon, params DalParametros[] parameters)
        {
            String s = loginSession + "|" + conected.ToString() + "|" + user + "|" + idCon.ToString() + "|" + sQuery + "|" + type.ToString();

            foreach (var p in parameters)
                s += "|" + p.Value.ToString();
            return ((useEncryption) ? Encryption.Encrypt(s) : s);
        }

        /// <summary>
        /// Indica si la conexion esta activa
        /// </summary>
        public bool isConected()
        {
            if (bConected)
            {
                TimeSpan dif = DateTime.Now - conected;
                //if((dif.Days < 1 && dif.Hours < 1 && dif.Minutes > timeSession) || loginSession.Equals(""))
                if (dif.Minutes > timeSession || dif.Hours > 1 || dif.Days > 1 || loginSession.Equals(""))
                {
                    //Fuerza la desconexion y cierre de session
                    logout();
                    return false;
                }
                return true;
            }
            return false;
        }
    }


  

    //Lista para manejo de varios Clientes simultaneos
    public class WebServiceClientList<T>
    {
        List<WebServicioCliente> lista_Clientes;

        public WebServiceClientList()
        {
            lista_Clientes = new List<WebServicioCliente>();
        }

        public WebServicioCliente this[int index]
        {
            set { lista_Clientes[index] = value; }
            get { return (index >= 0 ? (lista_Clientes[index] ?? null) : null); }
        }
        /// <summary>
        /// Busca un cliente que no este en uso(isBusy ==true), si no encuentra crea otro
        /// </summary>
        /// <param name="sUser">Usuario</param>
        /// <param name="sPass">Password de acceso</param>
        /// <param name="sUrllogin">Url para crear una nueva session</param>
        /// <param name="sUrldata">Url para el acceso a datos</param>
        /// <param name="sUrllogout"> Url para cerrar la session</param>
        /// <returns></returns>
        public WebServicioCliente cliente_libre(string sUser, string sPass, string sUrllogin, string sUrldata, string sUrllogout)
        {
            for (int i = 0; i < lista_Clientes.Count; i++)
            {
                if (lista_Clientes[i].isBusy)
                    continue;
                lista_Clientes[i].isBusy = true;
                return lista_Clientes[i];
            }
            bool bEncryption = true;
            WebServicioCliente wsClient = new WebServicioCliente(sUser, sPass, sUrllogin, sUrldata, sUrllogout, bEncryption);
            wsClient.isBusy = true;
            this.Add(wsClient);
            return wsClient;
        }
        /// <summary>
        /// Busca un cliente que no este en uso(isBusy ==true), si no encuentra crea otro
        /// </summary>
        /// <param name="sUser">Usuario</param>
        /// <param name="sPass">Password de acceso</param>
        /// <param name="sUrllogin">Url para crear una nueva session</param>
        /// <param name="sUrldata">Url para el acceso a datos</param>
        /// <param name="sUrllogout"> Url para cerrar la session</param>
        /// <param name="bEncryption">Envio de parametros codificado</param>
        /// <returns></returns>
        public WebServicioCliente cliente_libre(string sUser, string sPass, string sUrllogin, string sUrldata, string sUrllogout, bool bEncryption)
        {
            for (int i = 0; i < lista_Clientes.Count; i++)
            {
                if (lista_Clientes[i].isBusy)
                    continue;
                lista_Clientes[i].isBusy = true;
                return lista_Clientes[i];
            }
            WebServicioCliente wsClient = new WebServicioCliente(sUser, sPass, sUrllogin, sUrldata, sUrllogout, bEncryption);
            wsClient.isBusy = true;
            this.Add(wsClient);
            return wsClient;
        }
        /*
        public IEnumerable<WebServiceClient> Elements()
        {
            //for (int i = 0; i < lista.Count; i++)
            for (int i = lista_Clientes.Count - 1; i >= 0; i--)
            {
                yield return lista_Clientes[i];
            }

        }
        */
        public int length()
        {
            return lista_Clientes.Count;
        }

        /// <summary>
        /// Agrega elemento de cliente a la lista
        /// </summary>
        public WebServicioCliente Add(WebServicioCliente element)
        {
            lista_Clientes.Add(element);
            return element;
        }

    }
}
