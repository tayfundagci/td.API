﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using td.Domain.Entities;

namespace td.Persistence.Context
{
   
        public class DapperContext
        {
            private readonly IConfiguration _configuration;
            private readonly string _connectionString;

            public DapperContext(IConfiguration configuration)
            {
                _configuration = configuration;
                _connectionString = _configuration.GetConnectionString("SqlConnection");
            }

            public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        }

    
}

