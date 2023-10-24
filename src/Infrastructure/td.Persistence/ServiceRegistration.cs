using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using td.Application.Interfaces;
using td.Persistence.Context;
using td.Persistence.Migrations;
using td.Persistence.Repositories;
using td.Persistence.Services;

namespace td.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<DatabaseMigration>();
            services.AddSingleton<DapperContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IJwtService, JwtService>();
            return services;
        }
    }
}
    