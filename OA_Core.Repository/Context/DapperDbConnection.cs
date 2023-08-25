using MySql.Data.MySqlClient;
using OA_Core.Domain.Config;
using System.Data;

namespace OA_Core.Repository.Context
{
    public class DapperDbConnection
    {
        private readonly AppConfig _configuration;

        public DapperDbConnection(AppConfig configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateDbConnection()
        {
            var connectionString =  _configuration.ConnectionString;
            var connection = new MySqlConnection(connectionString);
            return connection;
        }
    }
}
