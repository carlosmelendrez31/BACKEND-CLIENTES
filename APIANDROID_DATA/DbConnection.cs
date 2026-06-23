using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIANDROID_DATA
{
    public class DbConnection
    {

        private readonly string _connectionString;

        public DbConnection(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public NpgsqlConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}
