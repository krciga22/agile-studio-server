using AgileStudioServer.API.Controllers;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;

namespace AgileStudioServerTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContext>(optionsBuilder => {
                DBContextFactory.ConfigureDefaultOptions(ref optionsBuilder);
            });

            services.AddScoped<BacklogItemController>();
            services.AddScoped<BacklogItemTypeController>();
            services.AddScoped<BacklogItemTypeSchemaController>();
            services.AddScoped<ProjectController>();
            services.AddScoped<ReleaseController>();
            services.AddScoped<SprintController>();
            services.AddScoped<WorkflowController>();
            services.AddScoped<WorkflowStateController>();

            services.AddScoped<BacklogItemDataProvider>();
            services.AddScoped<BacklogItemTypeDataProvider>();
            services.AddScoped<BacklogItemTypeSchemaDataProvider>();
            services.AddScoped<ProjectDataProvider>();
            services.AddScoped<ReleaseDataProvider>();
            services.AddScoped<SprintDataProvider>();
            services.AddScoped<WorkflowDataProvider>();
            services.AddScoped<WorkflowStateDataProvider>();

            services.AddScoped<ModelFixtures>();
            services.AddScoped<EntityFixtures>();

            services.AddScoped<BacklogItemConverter>();
            services.AddScoped<BacklogItemTypeConverter>();
            services.AddScoped<BacklogItemTypeSchemaConverter>();
            services.AddScoped<ProjectConverter>();
            services.AddScoped<ReleaseConverter>();
            services.AddScoped<SprintConverter>();
            services.AddScoped<UserConverter>();
            services.AddScoped<WorkflowConverter>();
            services.AddScoped<WorkflowStateConverter>();

            services.AddScoped<BacklogItemService>();
            services.AddScoped<BacklogItemTypeService>();
            services.AddScoped<BacklogItemTypeSchemaService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<ReleaseService>();
            services.AddScoped<SprintService>();
            services.AddScoped<UserService>();
            services.AddScoped<WorkflowService>();
            services.AddScoped<WorkflowStateService>();
        }
    }
}
