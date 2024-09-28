
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class BacklogItemHydrator : AbstractEntityHydrator
    {
        private ProjectHydrator _projectHydrator;
        private SprintHydrator _sprintHydrator;
        private ReleaseHydrator _releaseHydrator;
        private BacklogItemTypeHydrator _backlogItemTypeHydrator;
        private WorkflowStateHydrator _workflowStateHydrator;
        private UserHydrator _userHydrator;

        public BacklogItemHydrator(
            DBContext dBContext,
            ProjectHydrator projectHydrator,
            SprintHydrator sprintHydrator,
            ReleaseHydrator releaseHydrator,
            BacklogItemTypeHydrator backlogItemTypeHydrator,
            WorkflowStateHydrator workflowStateHydrator,
            UserHydrator userHydrator) : base(dBContext)
        {
            _projectHydrator = projectHydrator;
            _sprintHydrator = sprintHydrator;
            _releaseHydrator = releaseHydrator;
            _backlogItemTypeHydrator = backlogItemTypeHydrator;
            _workflowStateHydrator = workflowStateHydrator;
            _userHydrator = userHydrator;
        }

        public BacklogItem Hydrate(Application.Models.BacklogItem model, BacklogItem? entity = null)
        {
            if(entity == null)
            {
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItem.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItem),
                            model.ID.ToString()
                        );
                    }
                }
                else
                {
                    entity = new BacklogItem(model.Title);
                }
            }

            entity.Title = model.Title;

            if (model.Project != null)
            {
                entity.Project = _projectHydrator.Hydrate(model.Project);
            }

            if (model.Sprint != null)
            {
                entity.Sprint = _sprintHydrator.Hydrate(model.Sprint);
            }

            if (model.Release != null)
            {
                entity.Release = _releaseHydrator.Hydrate(model.Release);
            }

            if (model.BacklogItemType != null)
            {
                entity.BacklogItemType = _backlogItemTypeHydrator
                    .Hydrate(model.BacklogItemType);
            }

            if (model.WorkflowState != null)
            {
                entity.WorkflowState = _workflowStateHydrator
                    .Hydrate(model.WorkflowState);
            }

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = _userHydrator.Hydrate(model.CreatedBy);
            }

            return entity;
        }
    }
}
