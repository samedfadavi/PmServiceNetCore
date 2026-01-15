using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

    namespace Infrastructure.Data
{
        public interface ISqlDataAccess
        {
            Task<List<Dictionary<string, object>>> ExecuteSqlAsync(string sql);
            Task<List<Dictionary<string, object>>> ExecuteSqlAsync(string sql, SqlParameter[] parameters);
            Task<object> ExecuteScalarAsync(string sql);
            Task<int> ExecuteNonQueryAsync(string sql);
            Task<DataTable> ExecuteProcedureAsync(string procedureName, params SqlParameter[] parameters);
            Task<int> ExecuteProcedureNonQueryAsync(string procedureName, params SqlParameter[] parameters);
        }
    }

