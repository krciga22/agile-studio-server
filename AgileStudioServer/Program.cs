using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DBContext>(optionsBuilder => {
                var dbHost = builder.Configuration.GetValue<string>("DB:Host");
                var dbName = builder.Configuration.GetValue<string>("DB:Name");
                var dbUser = builder.Configuration.GetValue<string>("DB:User");
                var dbPass = builder.Configuration.GetValue<string>("DB:Pass");

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}