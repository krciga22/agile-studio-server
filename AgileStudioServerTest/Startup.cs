using DtoModelConverters = AgileStudioServer.API.DtosNew.DtoModelConverters;
using ModelEntityConverters = AgileStudioServer.Application.Models.ModelEntityConverters;
using DtoHydrators = AgileStudioServer.API.DtosNew.Hydrators;
using ModelHydrators = AgileStudioServer.Application.Models.Hydrators;
using EntityHydrators = AgileStudioServer.Data.Entities.Hydrators;

using AgileStudioServer.API.Controllers;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using AgileStudioServer.Core.Hydrator;

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

            services.AddScoped<DtoHydrators.BacklogItemDtoHydrator>();
            services.AddScoped<DtoHydrators.BacklogItemSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.BacklogItemTypeDtoHydrator>();
            services.AddScoped<DtoHydrators.BacklogItemTypeSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.BacklogItemTypeSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.BacklogItemTypeSchemaSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.ProjectDtoHydrator>();
            services.AddScoped<DtoHydrators.ProjectSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.ReleaseDtoHydrator>();
            services.AddScoped<DtoHydrators.ReleaseSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.SprintDtoHydrator>();
            services.AddScoped<DtoHydrators.SprintSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.UserSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.WorkflowDtoHydrator>();
            services.AddScoped<DtoHydrators.WorkflowSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.WorkflowSummaryDtoHydrator>();
            services.AddScoped<DtoHydrators.WorkflowStateSummaryDtoHydrator>();

            services.AddScoped<Hydrator>();
            services.AddScoped<HydratorRegistry>();
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.SprintHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.UserHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.WorkflowStateHydrator>();

            services.AddScoped<IHydrator, EntityHydrators.BacklogItemHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.SprintHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.UserHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.WorkflowStateHydrator>();

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
