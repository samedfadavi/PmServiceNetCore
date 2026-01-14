using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Data
{
    public class ClassData
    {
        private readonly string _connectionString;

        public ClassData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Pm");
        }

        private SqlConnection CreateConnection()
            => new SqlConnection(_connectionString);

        #region Core Helpers

        public async Task<List<Dictionary<string, object>>> ExecuteSqlAsync(string sql)
        {
            var result = new List<Dictionary<string, object>>();

            using var con = CreateConnection();
            using var cmd = new SqlCommand(sql, con);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = Enumerable.Range(0, reader.FieldCount)
                    .ToDictionary(reader.GetName, reader.GetValue);
                result.Add(row);
            }

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
            {
                var row = Enumerable.Range(0, reader.FieldCount)
                    .ToDictionary(reader.GetName, reader.GetValue);
                result.Add(row);
            }

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

        #region Stored Procedures

        public async Task<DataTable> ExecuteProcedureAsync(
            string procedureName,
            params SqlParameter[] parameters)
        {
            using var con = CreateConnection();
            using var cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters?.Any() == true)
                cmd.Parameters.AddRange(parameters);

            using var da = new SqlDataAdapter(cmd);
            var table = new DataTable();

            await con.OpenAsync();
            da.Fill(table);

            return table;
        }

        public async Task<int> ExecuteProcedureNonQueryAsync(
            string procedureName,
            params SqlParameter[] parameters)
        {
            using var con = CreateConnection();
            using var cmd = new SqlCommand(procedureName, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters?.Any() == true)
                cmd.Parameters.AddRange(parameters);

            await con.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        #endregion

        #region Bulk Copy

        public async Task<bool> BulkCopyAsync(
            string sourceQuery,
            string destinationTable,
            string destinationConnectionString)
        {
            try
            {
                using var sourceCon = CreateConnection();
                using var cmd = new SqlCommand(sourceQuery, sourceCon);

                await sourceCon.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();

                using var bulk = new SqlBulkCopy(destinationConnectionString)
                {
                    DestinationTableName = destinationTable
                };

                await bulk.WriteToServerAsync(reader);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Legacy-Compatible Helpers

        public async Task<bool> HasDataAsync(string sql)
        {
            using var con = CreateConnection();
            using var cmd = new SqlCommand(sql, con);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            return reader.HasRows;
        }

        public async Task<string> GetStringAsync(string sql)
        {
            var value = await ExecuteScalarAsync(sql);
            return value?.ToString();
        }

        public async Task<int> GetIntAsync(string sql)
        {
            var value = await ExecuteScalarAsync(sql);
            return value == null ? 0 : int.Parse(value.ToString());
        }

        #endregion
    }
}
