using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AgileStudioServer.Data
{
    public class DBContextFactory
    {
        public static DBContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var options = ConfigureDefaultOptions(ref optionsBuilder);
            return new DBContext(options);
        }

        public static DbContextOptions ConfigureDefaultOptions(ref DbContextOptionsBuilder optionsBuilder)
        {
            return optionsBuilder.UseMySql(
                    GetConnectionString(),
                    ServerVersion.Create(
                        new Version("8.0"),
                        Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql
                    )
                )
                .UseSnakeCaseNamingConvention()
                .Options;
        }

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly());
            var configuration = builder.Build();
            var dbHost = configuration.GetValue<string>("DB:Host");
            var dbName = configuration.GetValue<string>("DB:Name");
            var dbUser = configuration.GetValue<string>("DB:User");
            var dbPass = configuration.GetValue<string>("DB:Pass");
            return string.Format("server={0};database={1};user={2};password={3}", dbHost, dbName, dbUser, dbPass);
        }
    }
}
