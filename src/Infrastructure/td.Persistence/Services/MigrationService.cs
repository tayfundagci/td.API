using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using td.Persistence.Migrations;

namespace td.Persistence.Services
{
    public static class MigrationService
    {
        public static IHost MigrateDatabase(this IHost host, IConfiguration configuration)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<DatabaseMigration>();
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    databaseService.CreateDatabase("td", configuration);
                    runner.ListMigrations();
                    runner.MigrateUp();
                }
                catch (Exception e)
                {
                    throw new NotImplementedException();
                }
            }
            return host;
        }
    }
}
