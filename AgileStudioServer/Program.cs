using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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
                DBContextFactory.ConfigureDefaultOptions(ref optionsBuilder);
            });

            builder.Services.AddScoped<BacklogItemDataProvider>();
            builder.Services.AddScoped<BacklogItemTypeDataProvider>();
            builder.Services.AddScoped<BacklogItemTypeSchemaDataProvider>();
            builder.Services.AddScoped<ProjectDataProvider>();
            builder.Services.AddScoped<ReleaseDataProvider>();
            builder.Services.AddScoped<SprintDataProvider>();
            builder.Services.AddScoped<WorkflowDataProvider>();
            builder.Services.AddScoped<WorkflowStateDataProvider>();

            builder.Services.AddScoped<BacklogItemConverter>();
            builder.Services.AddScoped<BacklogItemTypeConverter>();
            builder.Services.AddScoped<BacklogItemTypeSchemaConverter>();
            builder.Services.AddScoped<ProjectConverter>();
            builder.Services.AddScoped<ReleaseConverter>();
            builder.Services.AddScoped<SprintConverter>();
            builder.Services.AddScoped<WorkflowConverter>();
            builder.Services.AddScoped<WorkflowStateConverter>();

            string auth0Domain = builder.Configuration.GetValue<string>("Auth0:Domain");
            string auth0ClientId = builder.Configuration.GetValue<string>("Auth0:ClientId");
            string auth0ClientSecret = builder.Configuration.GetValue<string>("Auth0:ClientSecret");
            string auth0Audience = builder.Configuration.GetValue<string>("Auth0:Audience");

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{auth0Domain}";
                    options.Audience = auth0Audience;
                })
                .AddAuth0WebAppAuthentication(options =>
                {
                    options.Domain = auth0Domain;
                    options.ClientId = auth0ClientId;
                    options.ClientSecret = auth0ClientSecret;
                    options.CallbackPath = "/Auth/Callback";
                })
                .WithAccessToken(options =>
                {
                    options.Audience = auth0Audience;
                    options.UseRefreshTokens = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                var policyBuilder = new AuthorizationPolicyBuilder();
                policyBuilder.AddAuthenticationSchemes(new string[] {
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        JwtBearerDefaults.AuthenticationScheme
                    })
                    .RequireAuthenticatedUser();
                options.DefaultPolicy = policyBuilder.Build();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}