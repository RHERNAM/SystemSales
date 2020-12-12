using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SCL;
using SistemaVentas.Clases.SQL;

namespace SistemaVentas.Clases.Entidates
{
    public class Kardex:SqlConexion
    {

        public static DataTable ConsultaKardexArticupoCodigo(string sCodigo)
        {
            return AccesoDatos.ToTable("Sp_Consulta_Kerdex_BuscarArticuloCodigo", CommandType.StoredProcedure,ConexionString,
                ComandoSQL.Parameters.AddWithValue("@Codigo", sCodigo));
        }
    }
}
