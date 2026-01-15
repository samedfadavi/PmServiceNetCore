using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ClassData : ISqlDataAccess, IProcedureDataAccess
    {
        private readonly string _connectionString;

        public ClassData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Pm");
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        #region ISqlDataAccess Methods

        public async Task<List<Dictionary<string, object>>> ExecuteSqlAsync(string sql)
        {
            var result = new List<Dictionary<string, object>>();
            using var con = CreateConnection();
            using var cmd = new SqlCommand(sql, con);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                result.Add(Enumerable.Range(0, reader.FieldCount)
                    .ToDictionary(reader.GetName, reader.GetValue));
            return result;
        }

        public async Task<List<Dictionary<string, object>>> ExecuteSqlAsync(string sql, SqlParameter[] parameters)
        {
            var result = new List<Dictionary<string, object>>();
            using var con = CreateConnection();
            using var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(parameters);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                result.Add(Enumerable.Range(0, reader.FieldCount)
                    .ToDictionary(reader.GetName, reader.GetValue));
            return result;
        }

        public async Task<object> ExecuteScalarAsync(string sql)
        {
            using var con = CreateConnection();
            using var cmd = new SqlCommand(sql, con);
            await con.OpenAsync();
            return await cmd.ExecuteScalarAsync();
        }

        public async Task<int> ExecuteNonQueryAsync(string sql)
        {
            using var con = CreateConnection();
            using var cmd = new SqlCommand(sql, con);
            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        #endregion

        #region IProcedureDataAccess Methods

        public async Task<DataTable> ExecuteProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            using var con = CreateConnection();
            using var cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            using var da = new SqlDataAdapter(cmd);
            var table = new DataTable();
            await con.OpenAsync();
            da.Fill(table);
            return table;
        }

        public async Task<int> ExecuteProcedureNonQueryAsync(string procedureName, params SqlParameter[] parameters)
        {
            using var con = CreateConnection();
            using var cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (parameters?.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        #endregion
    }
}
