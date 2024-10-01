using DtoModelConverters = AgileStudioServer.API.DtosNew.DtoModelConverters;
using ModelEntityConverters = AgileStudioServer.Application.Models.ModelEntityConverters;
using DtoHydrators = AgileStudioServer.API.DtosNew.Hydrators;
using ModelHydrators = AgileStudioServer.Application.Models.Hydrators;
using EntityHydrators = AgileStudioServer.Data.Entities.Hydrators;

using AgileStudioServer.Application.Services;
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

            builder.Services.AddScoped<DtoModelConverters.BacklogItemConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemPatchConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemPostConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemSummaryConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemTypeConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemTypePatchConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemTypePostConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemTypeSummaryConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemTypeSchemaConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemTypePatchConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemTypePostConverter>();
            builder.Services.AddScoped<DtoModelConverters.BacklogItemTypeSummaryConverter>();
            builder.Services.AddScoped<DtoModelConverters.ProjectConverter>();
            builder.Services.AddScoped<DtoModelConverters.ProjectPatchConverter>();
            builder.Services.AddScoped<DtoModelConverters.ProjectPostConverter>();
            builder.Services.AddScoped<DtoModelConverters.ProjectSummaryConverter>();
            builder.Services.AddScoped<DtoModelConverters.ReleaseConverter>();
            builder.Services.AddScoped<DtoModelConverters.ReleasePatchConverter>();
            builder.Services.AddScoped<DtoModelConverters.ReleasePostConverter>();
            builder.Services.AddScoped<DtoModelConverters.ReleaseSummaryConverter>();
            builder.Services.AddScoped<DtoModelConverters.SprintConverter>();
            builder.Services.AddScoped<DtoModelConverters.SprintPatchConverter>();
            builder.Services.AddScoped<DtoModelConverters.SprintPostConverter>();
            builder.Services.AddScoped<DtoModelConverters.SprintSummaryConverter>();
            builder.Services.AddScoped<DtoModelConverters.UserSummaryConverter>();
            builder.Services.AddScoped<DtoModelConverters.WorkflowConverter>();
            builder.Services.AddScoped<DtoModelConverters.WorkflowPatchConverter>();
            builder.Services.AddScoped<DtoModelConverters.WorkflowPostConverter>();
            builder.Services.AddScoped<DtoModelConverters.WorkflowSummaryConverter>();
            builder.Services.AddScoped<DtoModelConverters.WorkflowStateConverter>();
            builder.Services.AddScoped<DtoModelConverters.WorkflowStatePatchConverter>();
            builder.Services.AddScoped<DtoModelConverters.WorkflowStatePostConverter>();
            builder.Services.AddScoped<DtoModelConverters.WorkflowStateSummaryConverter>();

            builder.Services.AddScoped<ModelEntityConverters.BacklogItemConverter>();
            builder.Services.AddScoped<ModelEntityConverters.BacklogItemTypeConverter>();
            builder.Services.AddScoped<ModelEntityConverters.BacklogItemTypeSchemaConverter>();
            builder.Services.AddScoped<ModelEntityConverters.ProjectConverter>();
            builder.Services.AddScoped<ModelEntityConverters.ReleaseConverter>();
            builder.Services.AddScoped<ModelEntityConverters.SprintConverter>();
            builder.Services.AddScoped<ModelEntityConverters.UserConverter>();
            builder.Services.AddScoped<ModelEntityConverters.WorkflowConverter>();
            builder.Services.AddScoped<ModelEntityConverters.WorkflowStateConverter>();

            builder.Services.AddScoped<DtoHydrators.BacklogItemDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.BacklogItemSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.BacklogItemTypeDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.BacklogItemTypeSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.BacklogItemTypeSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.BacklogItemTypeSchemaSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.ProjectDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.ProjectSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.ReleaseDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.ReleaseSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.SprintDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.SprintSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.UserSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.WorkflowDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.WorkflowSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.WorkflowSummaryDtoHydrator>();
            builder.Services.AddScoped<DtoHydrators.WorkflowStateSummaryDtoHydrator>();

            builder.Services.AddScoped<ModelHydrators.HydratorRegistry>();
            builder.Services.AddScoped<ModelHydrators.HydratorLoader>();
            builder.Services.AddScoped<ModelHydrators.BacklogItemTypeHydrator>();
            builder.Services.AddScoped<ModelHydrators.BacklogItemTypeSchemaHydrator>();
            builder.Services.AddScoped<ModelHydrators.ProjectHydrator>();
            builder.Services.AddScoped<ModelHydrators.ReleaseHydrator>();
            builder.Services.AddScoped<ModelHydrators.SprintHydrator>();
            builder.Services.AddScoped<ModelHydrators.UserHydrator>();
            builder.Services.AddScoped<ModelHydrators.WorkflowHydrator>();
            builder.Services.AddScoped<ModelHydrators.WorkflowStateHydrator>();

            builder.Services.AddScoped<EntityHydrators.BacklogItemHydrator>();
            builder.Services.AddScoped<EntityHydrators.BacklogItemTypeHydrator>();
            builder.Services.AddScoped<EntityHydrators.BacklogItemTypeSchemaHydrator>();
            builder.Services.AddScoped<EntityHydrators.ProjectHydrator>();
            builder.Services.AddScoped<EntityHydrators.ReleaseHydrator>();
            builder.Services.AddScoped<EntityHydrators.SprintHydrator>();
            builder.Services.AddScoped<EntityHydrators.UserHydrator>();
            builder.Services.AddScoped<EntityHydrators.WorkflowHydrator>();
            builder.Services.AddScoped<EntityHydrators.WorkflowStateHydrator>();

            builder.Services.AddScoped<BacklogItemService>();
            builder.Services.AddScoped<BacklogItemTypeService>();
            builder.Services.AddScoped<BacklogItemTypeSchemaService>();
            builder.Services.AddScoped<ProjectService>();
            builder.Services.AddScoped<ReleaseService>();
            builder.Services.AddScoped<SprintService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<WorkflowService>();
            builder.Services.AddScoped<WorkflowStateService>();

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