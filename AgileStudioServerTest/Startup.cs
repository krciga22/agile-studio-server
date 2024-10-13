using DtoHydrators = AgileStudioServer.API.Dtos.Hydrators;
using ModelHydrators = AgileStudioServer.Application.Models.Hydrators;
using EntityHydrators = AgileStudioServer.Data.Entities.Hydrators;

using AgileStudioServer.API.Controllers;
using AgileStudioServer.Application.Services;
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

            services.AddScoped<ModelFixtures>();
            services.AddScoped<EntityFixtures>();

            services.AddScoped<Hydrator>();
            services.AddScoped<HydratorRegistry>();

            services.AddScoped<IHydrator, DtoHydrators.BacklogItemDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.BacklogItemSummaryDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.BacklogItemTypeDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.BacklogItemTypeSummaryDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.BacklogItemTypeSchemaDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.BacklogItemTypeSchemaSummaryDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.ProjectDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.ProjectSummaryDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.ReleaseDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.ReleaseSummaryDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.SprintDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.SprintSummaryDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.UserSummaryDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.WorkflowDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.WorkflowSummaryDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.WorkflowStateDtoHydrator>();
            services.AddScoped<IHydrator, DtoHydrators.WorkflowStateSummaryDtoHydrator>();
            
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.SprintHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.UserHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, ModelHydrators.WorkflowStateHydrator>();

            services.AddScoped<IHydrator, EntityHydrators.BacklogItemHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.BacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.BacklogItemTypeSchemaHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.ChildBacklogItemTypeHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.ProjectHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.ReleaseHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.SprintHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.UserHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.WorkflowHydrator>();
            services.AddScoped<IHydrator, EntityHydrators.WorkflowStateHydrator>();

            services.AddScoped<BacklogItemService>();
            services.AddScoped<BacklogItemTypeService>();
            services.AddScoped<BacklogItemTypeSchemaService>();
            services.AddScoped<ChildBacklogItemTypeService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<ReleaseService>();
            services.AddScoped<SprintService>();
            services.AddScoped<UserService>();
            services.AddScoped<WorkflowService>();
            services.AddScoped<WorkflowStateService>();
        }
    }
}
