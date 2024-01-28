using AgileStudioServer;
using AgileStudioServer.Controllers;
using AgileStudioServer.DataProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

[assembly: UserSecretsId("458c7384-b3de-4f95-8b07-128aa520c896")]
namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder().AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly);
            var configuration = builder.Build();

            services.AddDbContext<DBContext>(optionsBuilder => {
                var dbHost = configuration.GetValue<string>("DB:Host");
                var dbName = configuration.GetValue<string>("DB:Name");
                var dbUser = configuration.GetValue<string>("DB:User");
                var dbPass = configuration.GetValue<string>("DB:Pass");

                var connectionString = String.Format("server={0};database={1};user={2};password={3}", dbHost, dbName, dbUser, dbPass);
                var mysqlVersion = new Version("8.0");
                optionsBuilder
                    .UseMySql(
                        connectionString,
                        MySqlServerVersion.Create(
                            mysqlVersion,
                            Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql
                        )
                    )
                    .UseSnakeCaseNamingConvention();
            });

            services.AddScoped<BacklogItemTypeController>();
            services.AddScoped<BacklogItemTypeSchemasController>();
            services.AddScoped<ProjectsController>();

            services.AddScoped<BacklogItemTypeDataProvider>();
            services.AddScoped<BacklogItemTypeSchemaDataProvider>();
            services.AddScoped<ProjectDataProvider>();
        }
    }
}
