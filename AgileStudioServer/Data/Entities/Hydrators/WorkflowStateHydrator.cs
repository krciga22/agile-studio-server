
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class WorkflowStateHydrator : AbstractEntityHydrator, IEntityHydrator<Application.Models.WorkflowState, WorkflowState>
    {
        private WorkflowHydrator _workflowHydrator;
        private UserHydrator _userHydrator;

        public WorkflowStateHydrator(
            DBContext dBContext,
            WorkflowHydrator workflowHydrator,
            UserHydrator userHydrator) : base(dBContext)
        {
            _workflowHydrator = workflowHydrator;
            _userHydrator = userHydrator;
        }

        public WorkflowState Hydrate(Application.Models.WorkflowState model, WorkflowState? entity = null)
        {
            if(entity == null)
            {
                if (model.ID > 0)
                {
                    entity = _DBContext.WorkflowState.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(WorkflowState),
                            model.ID.ToString()
                        );
                    }
                }
                else
                {
                    entity = new WorkflowState(model.Title);
                }
            }

            if (model.Workflow != null)
            {
                entity.Workflow = _workflowHydrator.Hydrate(model.Workflow);
            }

            if (model.CreatedBy != null){
                entity.CreatedBy = _userHydrator.Hydrate(model.CreatedBy);
            }

            return entity;
        }
    }
}
