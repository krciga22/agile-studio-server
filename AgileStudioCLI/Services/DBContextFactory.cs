using AgileStudioServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AgileStudioCLI.Services
{
    // todo move this to AgileStudioServer project
    internal class DBContextFactory
    {
        public static DBContext Create()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets(Assembly.GetEntryAssembly());
            var configuration = builder.Build();
            var dbHost = configuration.GetValue<string>("DB:Host");
            var dbName = configuration.GetValue<string>("DB:Name");
            var dbUser = configuration.GetValue<string>("DB:User");
            var dbPass = configuration.GetValue<string>("DB:Pass");
            var connectionString = String.Format("server={0};database={1};user={2};password={3}", dbHost, dbName, dbUser, dbPass);
            var mysqlVersion = new Version("8.0");

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseMySql(
                    connectionString,
                    MySqlServerVersion.Create(
                        mysqlVersion,
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql
                    )
                )
                .UseSnakeCaseNamingConvention()
                .Options;

            return new DBContext(options);
        }
    }
}
