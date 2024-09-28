
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowStateHydrator : AbstractModelHydrator
    {
        private WorkflowStateService _workflowStateService;
        private WorkflowService _workflowService;
        private WorkflowHydrator _workflowHydrator;
        private UserHydrator _userHydrator;

        public WorkflowStateHydrator(
            WorkflowStateService workflowStateService,
            WorkflowService workflowService,
            WorkflowHydrator workflowHydrator,
            UserHydrator userHydrator)
        {
            _workflowStateService = workflowStateService;
            _workflowService = workflowService;
            _workflowHydrator = workflowHydrator;
            _userHydrator = userHydrator;
        }

        public WorkflowState Hydrate(Data.Entities.WorkflowState entity, WorkflowState? model = null)
        {
            if(model == null)
            {
                if (entity.ID > 0)
                {
                    model = _workflowStateService.Get(entity.ID) ??
                        throw new ModelNotFoundException(
                            nameof(WorkflowState), entity.ID.ToString()
                        );
                }
                else
                {
                    model = new WorkflowState(entity.Title);
                }
            }
            
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
            model.Workflow = _workflowService.Get(dto.WorkflowId) ??
                throw new ModelNotFoundException(
                    nameof(Workflow), dto.WorkflowId.ToString()
                );

            return model;
        }

        public WorkflowState Hydrate(API.DtosNew.WorkflowStatePatchDto dto, WorkflowState? model = null)
        {
            model ??= _workflowStateService.Get(dto.ID) ??
                throw new ModelNotFoundException(
                    nameof(WorkflowState), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
