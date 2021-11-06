using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Timecards.Application.Interfaces;

namespace Timecards.Infrastructure.EF
{
    public class MSSqlConnection : IConnection
    {
        private readonly ConnectionOptions _options;

        public MSSqlConnection(IOptions<ConnectionOptions> options)
        {
            _options = options.Value;
        }

        public IDbConnection OpenConnection()
        {
            var sqlConnection = new SqlConnection(_options.DefaultConnection);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}