using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Dapper_Application.Data.DataAccess.SqlDataAccess;

namespace Dapper_Application.Data.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "conn")
        {
            // Using statement to create and manage a database connection.
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            // Returning the result of asynchronously querying the database using Dapper.
            return await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string spName, T parameters, string connectionId = "conn")
        {
            // Using statement to create and manage a database connection.
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            // Asynchronously executing a stored procedure to save data using Dapper.
            await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
