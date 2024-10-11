
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class WorkflowStateDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.WorkflowState) &&
                to == typeof(WorkflowStateDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(WorkflowStateDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.WorkflowState  &&
                referenceHydrator != null)
            {
                var model = (Application.Models.WorkflowState)from;

                var workflowSummaryDto = (WorkflowSummaryDto)referenceHydrator.Hydrate(
                    model.Workflow, typeof(WorkflowSummaryDto), maxDepth, depth
                );

                dto = new WorkflowStateDto(model.ID, model.Title, workflowSummaryDto, model.CreatedOn);
                Hydrate(model, dto, maxDepth, depth, referenceHydrator);
            }

            if (dto == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return dto;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (to is not WorkflowStateDto)
            {
                throw new Exception("Unsupported to");
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
                        model.Workflow, typeof(WorkflowSummaryDto), maxDepth, depth
                    );

                    if (model.CreatedBy != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedBy, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
