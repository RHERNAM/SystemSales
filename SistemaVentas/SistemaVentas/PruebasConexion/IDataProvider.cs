using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.PruebasConexion
{
    public interface IDataProvider:IDisposable
    {
        void StartTransaction();
        void Commit();
        void RollBack();

        DataTable ToTable(CommandType commandType, String commandText, params Object[] parameters);
        DataSet ToDataSet(CommandType commandType, String commandText, params Object[] parameters);
        Object ExScalar(CommandType commandType, String commandText, params Object[] parameters);
        int ExNonQuery(CommandType commandType, String commandText, params Object[] parameters);

    }
}
