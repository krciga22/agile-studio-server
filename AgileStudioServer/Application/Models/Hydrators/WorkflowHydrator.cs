
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowHydrator : AbstractModelHydrator
    {
        private DBContext _DBContext;
        private UserHydrator _userHydrator;

        public WorkflowHydrator(
            DBContext dbContext,
            UserHydrator userHydrator)
        {
            _DBContext = dbContext;
            _userHydrator = userHydrator;
        }

        public Workflow Hydrate(Data.Entities.Workflow entity, Workflow? model = null)
        {
            model ??= new Workflow(entity.Title);

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
            if(model == null)
            {
                Data.Entities.Workflow? workflowEntity = _DBContext.Workflow.Find(dto.ID) ??
                    throw new ModelNotFoundException(nameof(Workflow), dto.ID.ToString());

                model ??= Hydrate(workflowEntity);
            }

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
