using DtoModelConverters = AgileStudioServer.API.DtosNew.DtoModelConverters;
using ModelEntityConverters = AgileStudioServer.Application.Models.ModelEntityConverters;

using AgileStudioServer.API.Controllers;
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

            services.AddScoped<DtoModelConverters.BacklogItemConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemPatchConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemPostConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemSummaryConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemTypeConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemTypePatchConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemTypePostConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemTypeSummaryConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemTypeSchemaConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemTypePatchConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemTypePostConverter>();
            services.AddScoped<DtoModelConverters.BacklogItemTypeSummaryConverter>();
            services.AddScoped<DtoModelConverters.ProjectConverter>();
            services.AddScoped<DtoModelConverters.ProjectPatchConverter>();
            services.AddScoped<DtoModelConverters.ProjectPostConverter>();
            services.AddScoped<DtoModelConverters.ProjectSummaryConverter>();
            services.AddScoped<DtoModelConverters.ReleaseConverter>();
            services.AddScoped<DtoModelConverters.ReleasePatchConverter>();
            services.AddScoped<DtoModelConverters.ReleasePostConverter>();
            services.AddScoped<DtoModelConverters.ReleaseSummaryConverter>();
            services.AddScoped<DtoModelConverters.SprintConverter>();
            services.AddScoped<DtoModelConverters.SprintPatchConverter>();
            services.AddScoped<DtoModelConverters.SprintPostConverter>();
            services.AddScoped<DtoModelConverters.SprintSummaryConverter>();
            services.AddScoped<DtoModelConverters.UserSummaryConverter>();
            services.AddScoped<DtoModelConverters.WorkflowConverter>();
            services.AddScoped<DtoModelConverters.WorkflowPatchConverter>();
            services.AddScoped<DtoModelConverters.WorkflowPostConverter>();
            services.AddScoped<DtoModelConverters.WorkflowSummaryConverter>();
            services.AddScoped<DtoModelConverters.WorkflowStateConverter>();
            services.AddScoped<DtoModelConverters.WorkflowStatePatchConverter>();
            services.AddScoped<DtoModelConverters.WorkflowStatePostConverter>();
            services.AddScoped<DtoModelConverters.WorkflowStateSummaryConverter>();

            services.AddScoped<ModelEntityConverters.BacklogItemConverter>();
            services.AddScoped<ModelEntityConverters.BacklogItemTypeConverter>();
            services.AddScoped<ModelEntityConverters.BacklogItemTypeSchemaConverter>();
            services.AddScoped<ModelEntityConverters.ProjectConverter>();
            services.AddScoped<ModelEntityConverters.ReleaseConverter>();
            services.AddScoped<ModelEntityConverters.SprintConverter>();
            services.AddScoped<ModelEntityConverters.UserConverter>();
            services.AddScoped<ModelEntityConverters.WorkflowConverter>();
            services.AddScoped<ModelEntityConverters.WorkflowStateConverter>();

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
