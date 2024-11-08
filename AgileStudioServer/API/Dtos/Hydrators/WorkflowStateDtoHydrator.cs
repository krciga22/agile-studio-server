
using AgileStudioServer.API.Dtos.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class WorkflowStateDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) || 
                from == typeof(Application.Models.WorkflowState)
            ) && to == typeof(WorkflowStateDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            if(referenceHydrator == null)
            {
                throw new ReferenceHydratorRequiredException(this);
            }

            Application.Models.WorkflowState? model = null;
            if (from is int)
            {
                model = (Application.Models.WorkflowState)referenceHydrator.Hydrate(
                    from, typeof(Application.Models.WorkflowState), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is Application.Models.WorkflowState)
            {
                model = (Application.Models.WorkflowState)from;
            }

            Object? dto = null;
            if (model != null)
            {
                var workflowSummaryDto = (WorkflowSummaryDto)referenceHydrator.Hydrate(
                    model.WorkflowId, typeof(WorkflowSummaryDto), maxDepth, depth
                );

                dto = new WorkflowStateDto(model.ID, model.Title, workflowSummaryDto, model.CreatedOn);
                Hydrate(model, dto, maxDepth, depth, referenceHydrator);
            }

            if (dto == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return dto;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var dto = (WorkflowStateDto)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.WorkflowState)
            {
                var model = (Application.Models.WorkflowState)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.Workflow = (WorkflowSummaryDto)referenceHydrator.Hydrate(
                        model.WorkflowId, typeof(WorkflowSummaryDto), maxDepth, depth
                    );

                    if (model.CreatedById != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedById, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
