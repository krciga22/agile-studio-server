
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class WorkflowStateHydrator : AbstractModelHydrator
    {
        public WorkflowStateHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.WorkflowState)
                || from == typeof(API.Dtos.WorkflowStatePostDto)
                || from == typeof(API.Dtos.WorkflowStatePatchDto)
            ) && to == typeof(WorkflowState);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? model = null;

            if (from is Data.Entities.WorkflowState)
            {
                var entity = (Data.Entities.WorkflowState)from;
                model = new WorkflowState(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.WorkflowStatePostDto)
            {
                var dto = (API.Dtos.WorkflowStatePostDto)from;
                model = new WorkflowState(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.WorkflowStatePatchDto)
            {
                var dto = (API.Dtos.WorkflowStatePatchDto)from;
                var entity = _DBContext.WorkflowState.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(WorkflowState), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
            }

            if (model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (WorkflowState)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.WorkflowState)
            {
                var entity = (Data.Entities.WorkflowState)from;
                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    if (entity.Workflow != null)
                    {
                        model.Workflow = (Workflow)referenceHydrator.Hydrate(
                            entity.Workflow, typeof(Workflow), maxDepth, nextDepth
                        );
                    }

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)referenceHydrator.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.Dtos.WorkflowStatePostDto)
            {
                var dto = (API.Dtos.WorkflowStatePostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;

                if (referenceHydrator != null && depth < maxDepth)
                {
                    if (dto.WorkflowId > 0)
                    {
                        Data.Entities.Workflow? workflowEntity = _DBContext.Workflow.Find(dto.WorkflowId) ??
                            throw new ModelNotFoundException(
                                nameof(Workflow), 
                                dto.WorkflowId.ToString()
                            );

                        model.Workflow = (Workflow)referenceHydrator.Hydrate(
                            workflowEntity, typeof(Workflow), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.Dtos.WorkflowStatePatchDto)
            {
                var dto = (API.Dtos.WorkflowStatePatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
