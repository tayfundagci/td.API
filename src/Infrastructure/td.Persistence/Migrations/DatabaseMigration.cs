using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Persistence.Repositories;

namespace td.Persistence.Migrations
{
    public class DatabaseMigration : DapperRepository
    {
        public DatabaseMigration(IConfiguration configuration) : base(configuration)
        {
        }
        public void CreateDatabase(string dbName, IConfiguration configuration)
        {
           
        }
    }
}
