
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class ProjectHydrator : AbstractModelHydrator
    {
        private ProjectService _projectService;
        private UserHydrator _userHydrator;
        private WorkflowService _workflowService;
        private WorkflowHydrator _workflowHydrator;
        private BacklogItemTypeSchemaHydrator _backlogItemTypeSchemaHydrator;
        private BacklogItemTypeSchemaService _backlogItemTypeSchemaService;

        public ProjectHydrator(
            ProjectService projectService,
            UserHydrator userHydrator,
            WorkflowService workflowService,
            WorkflowHydrator workflowHydrator,
            BacklogItemTypeSchemaHydrator backlogItemTypeSchemaHydrator,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService)
        {
            _projectService = projectService;
            _userHydrator = userHydrator;
            _workflowService = workflowService;
            _workflowHydrator = workflowHydrator;
            _backlogItemTypeSchemaHydrator = backlogItemTypeSchemaHydrator;
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
        }

        public Project Hydrate(Data.Entities.Project entity, Project? model = null)
        {
            if (model == null)
            {
                if (entity.ID > 0)
                {
                    model = _projectService.Get(entity.ID) ??
                       throw new ModelNotFoundException(
                           nameof(Project), entity.ID.ToString()
                       );
                }
                else
                {
                    model = new Project(entity.Title);
                }
            }

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            if (entity.BacklogItemTypeSchema != null)
            {
                model.BacklogItemTypeSchema = _backlogItemTypeSchemaHydrator.Hydrate(entity.BacklogItemTypeSchema);
            }

            return model;
        }

        public Project Hydrate(API.DtosNew.ProjectPostDto dto, Project? model = null)
        {
            model ??= new Project(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;
            
            model.BacklogItemTypeSchema = _backlogItemTypeSchemaService
                .Get(dto.BacklogItemTypeSchemaId) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemTypeSchema), dto.BacklogItemTypeSchemaId.ToString()
                );

            return model;
        }

        public Project Hydrate(API.DtosNew.ProjectPatchDto dto, Project? model = null)
        {
            model ??= _projectService.Get(dto.ID) ??
                throw new ModelNotFoundException(
                    nameof(Project), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
