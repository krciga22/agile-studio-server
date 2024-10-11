
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class WorkflowSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.Workflow) && 
                to == typeof(WorkflowSummaryDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(WorkflowSummaryDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.Workflow)
            {
                var model = (Application.Models.Workflow)from;
                dto = new WorkflowSummaryDto(model.ID, model.Title);
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
            if (to is not WorkflowSummaryDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (WorkflowSummaryDto)to;

            if (from is Application.Models.Workflow)
            {
                var model = (Application.Models.Workflow)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
