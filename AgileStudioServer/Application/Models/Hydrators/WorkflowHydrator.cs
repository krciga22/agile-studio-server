
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowHydrator : AbstractModelHydrator
    {
        private WorkflowService _workflowService;
        private UserHydrator _userHydrator;

        public WorkflowHydrator(
            WorkflowService workflowService,
            UserHydrator userHydrator)
        {
            _workflowService = workflowService;
            _userHydrator = userHydrator;
        }

        public Workflow Hydrate(Data.Entities.Workflow entity, Workflow? model = null)
        {
            if(model == null)
            {
                if(entity.ID > 0)
                {
                    model = _workflowService.Get(entity.ID) ??
                       throw new ModelNotFoundException(
                           nameof(Workflow), entity.ID.ToString()
                       );
                }
                else
                {
                    model = new Workflow(entity.Title);
                }
            }

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            return model;
        }

        public Workflow Hydrate(API.DtosNew.WorkflowPostDto dto, Workflow? model = null)
        {
            model ??= new Workflow(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }

        public Workflow Hydrate(API.DtosNew.WorkflowPatchDto dto, Workflow? model = null)
        {
            model ??= _workflowService.Get(dto.ID) ??
                throw new ModelNotFoundException(
                    nameof(Workflow), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
