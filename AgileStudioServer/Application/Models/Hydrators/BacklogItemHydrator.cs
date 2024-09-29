
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemHydrator : AbstractModelHydrator
    {
        private DBContext _DBContext;
        private ProjectHydrator _projectHydrator;
        private SprintHydrator _sprintHydrator;
        private ReleaseHydrator _releaseHydrator;
        private BacklogItemTypeHydrator _backlogItemTypeHydrator;
        private UserHydrator _userHydrator;
        private WorkflowStateHydrator _workflowStateHydrator;

        public BacklogItemHydrator(
            DBContext dbContext,
            ProjectHydrator projectHydrator,
            SprintHydrator sprintHydrator,
            ReleaseHydrator releaseHydrator,
            BacklogItemTypeHydrator backlogItemTypeSchemaHydrator,
            UserHydrator userHydrator,
            WorkflowStateHydrator workflowHydrator)
        {
            _DBContext = dbContext;
            _projectHydrator = projectHydrator;
            _sprintHydrator = sprintHydrator;
            _releaseHydrator = releaseHydrator;
            _backlogItemTypeHydrator = backlogItemTypeSchemaHydrator;
            _userHydrator = userHydrator;
            _workflowStateHydrator = workflowHydrator;
        }

        public BacklogItem Hydrate(Data.Entities.BacklogItem entity, BacklogItem? model = null)
        {
            model ??= new BacklogItem(entity.Title);

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

            Data.Entities.Project? projectEntity =
                _DBContext.Project.Find(dto.ProjectId) ??
                    throw new ModelNotFoundException(
                        nameof(Project),
                        dto.ProjectId.ToString()
                    );
            model.Project = _projectHydrator.Hydrate(projectEntity);

            Data.Entities.BacklogItemType? backlogItemTypeEntity =
                _DBContext.BacklogItemType.Find(dto.BacklogItemTypeId) ??
                    throw new ModelNotFoundException(
                        nameof(BacklogItemType),
                        dto.BacklogItemTypeId.ToString()
                    );
            model.BacklogItemType = _backlogItemTypeHydrator.Hydrate(backlogItemTypeEntity);

            Data.Entities.WorkflowState? workflowStateEntity = 
                _DBContext.WorkflowState.Find(dto.WorkflowStateId) ??
                    throw new ModelNotFoundException(
                        nameof(WorkflowState), 
                        dto.WorkflowStateId.ToString()
                    );
            model.WorkflowState = _workflowStateHydrator.Hydrate(workflowStateEntity);

            if (dto.SprintId != null)
            {
                int sprintId = (int)dto.SprintId;
                Data.Entities.Sprint? sprintEntity = _DBContext.Sprint.Find(sprintId) ??
                    throw new ModelNotFoundException(nameof(Sprint), sprintId.ToString());
                model.Sprint = _sprintHydrator.Hydrate(sprintEntity);
            }

            if (dto.ReleaseId != null)
            {
                int releaseId = (int)dto.ReleaseId;
                Data.Entities.Release? releaseEntity = _DBContext.Release.Find(releaseId) ??
                    throw new ModelNotFoundException(nameof(Release), releaseId.ToString());
                model.Release = _releaseHydrator.Hydrate(releaseEntity);
            }

            return model;
        }

        public BacklogItem Hydrate(API.DtosNew.BacklogItemPatchDto dto, BacklogItem? model = null)
        {
            if (model == null)
            {
                Data.Entities.BacklogItem? backlogItemEntity = _DBContext.BacklogItem.Find(dto.ID) ??
                    throw new ModelNotFoundException(nameof(BacklogItem), dto.ID.ToString());

                model ??= Hydrate(backlogItemEntity);
            }

            model.Title = dto.Title;
            model.Description = dto.Description;


            Data.Entities.WorkflowState? workflowStateEntity = _DBContext.WorkflowState.Find(dto.ID) ??
                    throw new ModelNotFoundException(nameof(WorkflowState), dto.ID.ToString());
            model.WorkflowState = _workflowStateHydrator.Hydrate(workflowStateEntity);

            if (dto.SprintId != null)
            {
                int sprintId = (int)dto.SprintId;
                Data.Entities.Sprint? sprintEntity = _DBContext.Sprint.Find(sprintId) ??
                    throw new ModelNotFoundException(nameof(Sprint), sprintId.ToString());
                model.Sprint = _sprintHydrator.Hydrate(sprintEntity);
            }

            if (dto.ReleaseId != null)
            {
                int releaseId = (int)dto.ReleaseId;
                Data.Entities.Release? releaseEntity = _DBContext.Release.Find(releaseId) ??
                    throw new ModelNotFoundException(nameof(Release), releaseId.ToString());
                model.Release = _releaseHydrator.Hydrate(releaseEntity);
            }

            return model;
        }
    }
}
