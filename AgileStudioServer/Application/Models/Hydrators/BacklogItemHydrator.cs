
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemHydrator : AbstractModelHydrator
    {
        private BacklogItemService _backlogItemService;
        private ProjectService _projectService;
        private ProjectHydrator _projectHydrator;
        private SprintService _sprintService;
        private SprintHydrator _sprintHydrator;
        private ReleaseService _releaseService;
        private ReleaseHydrator _releaseHydrator;
        private BacklogItemTypeService _backlogItemTypeService;
        private BacklogItemTypeHydrator _backlogItemTypeHydrator;
        private UserHydrator _userHydrator;
        private WorkflowStateService _workflowStateService;
        private WorkflowStateHydrator _workflowStateHydrator;

        public BacklogItemHydrator(
            BacklogItemService backlogItemService,
            ProjectService projectService,
            ProjectHydrator projectHydrator,
            SprintService sprintService,
            SprintHydrator sprintHydrator,
            ReleaseService releaseService,
            ReleaseHydrator releaseHydrator,
            BacklogItemTypeService backlogItemTypeService,
            BacklogItemTypeHydrator backlogItemTypeSchemaHydrator,
            UserHydrator userHydrator,
            WorkflowStateService workflowService,
            WorkflowStateHydrator workflowHydrator)
        {
            _backlogItemService = backlogItemService;
            _projectService = projectService;
            _projectHydrator = projectHydrator;
            _sprintService = sprintService;
            _sprintHydrator = sprintHydrator;
            _releaseService = releaseService;
            _releaseHydrator = releaseHydrator;
            _backlogItemTypeService = backlogItemTypeService;
            _backlogItemTypeHydrator = backlogItemTypeSchemaHydrator;
            _userHydrator = userHydrator;
            _workflowStateService = workflowService;
            _workflowStateHydrator = workflowHydrator;
        }

        public BacklogItem ConvertToModel(Data.Entities.BacklogItem entity, BacklogItem? model = null)
        {
            if (model == null)
            {
                if (entity.ID > 0)
                {
                    model = _backlogItemService.Get(entity.ID) ??
                       throw new ModelNotFoundException(
                           nameof(BacklogItem), entity.ID.ToString()
                       );
                }
                else
                {
                    model = new BacklogItem(entity.Title);
                }
            }

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            if (entity.Project != null)
            {
                model.Project = _projectHydrator.Hydrate(entity.Project);
            }

            if (entity.Sprint != null)
            {
                model.Sprint = _sprintHydrator.Hydrate(entity.Sprint);
            }

            if (entity.Release != null)
            {
                model.Release = _releaseHydrator.Hydrate(entity.Release);
            }

            if (entity.BacklogItemType != null)
            {
                model.BacklogItemType = _backlogItemTypeHydrator.Hydrate(entity.BacklogItemType);
            }

            if (entity.WorkflowState != null)
            {
                model.WorkflowState = _workflowStateHydrator.Hydrate(entity.WorkflowState);
            }

            return model;
        }

        public BacklogItem Hydrate(API.DtosNew.BacklogItemPostDto dto, BacklogItem? model = null)
        {
            model ??= new BacklogItem(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;

            model.Project = _projectService.Get(dto.ProjectId) ??
                throw new ModelNotFoundException(
                    nameof(Project), dto.ProjectId.ToString()
                );

            model.BacklogItemType = _backlogItemTypeService
                .Get(dto.BacklogItemTypeId) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItemType), dto.BacklogItemTypeId.ToString()
                );

            model.WorkflowState = _workflowStateService
                .Get(dto.WorkflowStateId) ??
                throw new ModelNotFoundException(
                    nameof(WorkflowState), dto.WorkflowStateId.ToString()
                );

            if (dto.SprintId != null)
            {
                int sprintId = (int) dto.SprintId;
                model.Sprint = _sprintService.Get(sprintId) ??
                    throw new ModelNotFoundException(
                        nameof(Sprint), sprintId.ToString()
                    );
            }

            if (dto.ReleaseId != null)
            {
                int ReleaseId = (int)dto.ReleaseId;
                model.Release = _releaseService.Get(ReleaseId) ??
                    throw new ModelNotFoundException(
                        nameof(Release), ReleaseId.ToString()
                    );
            }

            return model;
        }

        public BacklogItem Hydrate(API.DtosNew.BacklogItemPatchDto dto, BacklogItem? model = null)
        {
            model ??= _backlogItemService.Get(dto.ID) ??
                throw new ModelNotFoundException(
                    nameof(BacklogItem), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;
            model.WorkflowState = _workflowStateService
                .Get(dto.WorkflowStateId) ??
                throw new ModelNotFoundException(
                    nameof(WorkflowState), dto.WorkflowStateId.ToString()
                );

            if (dto.SprintId != null)
            {
                int sprintId = (int)dto.SprintId;
                model.Sprint = _sprintService.Get(sprintId) ??
                    throw new ModelNotFoundException(
                        nameof(Sprint), sprintId.ToString()
                    );
            }

            if (dto.ReleaseId != null)
            {
                int ReleaseId = (int)dto.ReleaseId;
                model.Release = _releaseService.Get(ReleaseId) ??
                    throw new ModelNotFoundException(
                        nameof(Release), ReleaseId.ToString()
                    );
            }

            return model;
        }
    }
}
