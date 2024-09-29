
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowStateHydrator : AbstractModelHydrator
    {
        private DBContext _DBContext;
        private WorkflowHydrator _workflowHydrator;
        private UserHydrator _userHydrator;

        public WorkflowStateHydrator(
            DBContext dbContext,
            WorkflowHydrator workflowHydrator,
            UserHydrator userHydrator)
        {
            _DBContext = dbContext;
            _workflowHydrator = workflowHydrator;
            _userHydrator = userHydrator;
        }

        public WorkflowState Hydrate(Data.Entities.WorkflowState entity, WorkflowState? model = null)
        {
            model ??= new WorkflowState(entity.Title);
            
            model.ID = entity.ID;
            model.Title  = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.Workflow != null)
            {
                model.Workflow = _workflowHydrator.Hydrate(entity.Workflow);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            return model;
        }

        public WorkflowState Hydrate(API.DtosNew.WorkflowStatePostDto dto, WorkflowState? model = null)
        {
            model ??= new WorkflowState(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;

            Data.Entities.Workflow? workflowEntity = _DBContext.Workflow.Find(dto.WorkflowId) ??
                throw new ModelNotFoundException(nameof(Workflow), dto.WorkflowId.ToString());
            model.Workflow = _workflowHydrator.Hydrate(workflowEntity);

            return model;
        }

        public WorkflowState Hydrate(API.DtosNew.WorkflowStatePatchDto dto, WorkflowState? model = null)
        {
            if(model == null)
            {
                Data.Entities.WorkflowState? workflowStateEntity = _DBContext.WorkflowState.Find(dto.ID) ??
                    throw new ModelNotFoundException(nameof(WorkflowState), dto.ID.ToString());

                model ??= Hydrate(workflowStateEntity);
            }
            
            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
