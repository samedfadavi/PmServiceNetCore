using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
namespace Infrastructure.Data
{
        public interface IProcedureDataAccess
        {
            Task<DataTable> ExecuteProcedureAsync(string procedureName, params SqlParameter[] parameters);
            Task<int> ExecuteProcedureNonQueryAsync(string procedureName, params SqlParameter[] parameters);
        }
    
}
