using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.PruebasConexion
{
    internal  class BDMySQL
    {

        private MySqlTransaction transaction;
        private MySqlConnection con;
        private MySqlCommand comm;
        private String conString;

        internal BDMySQL(String conString)
        {
            this.conString = conString;
            con = new MySqlConnection(conString);
            con.Open();
            comm = new MySqlCommand();
            comm.Connection = con;
        }
    }
}
