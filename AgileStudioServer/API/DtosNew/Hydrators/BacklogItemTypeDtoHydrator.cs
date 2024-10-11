
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemTypeDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.BacklogItemType) &&
                to == typeof(BacklogItemTypeDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(BacklogItemTypeDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.BacklogItemType && referenceHydrator != null)
            {
                var model = (Application.Models.BacklogItemType)from;

                var backlogItemTypeSchemaSummaryDto = (BacklogItemTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemTypeSchema, typeof(BacklogItemTypeSchemaSummaryDto), maxDepth, depth
                );

                var workflowSummaryDto = (WorkflowSummaryDto)referenceHydrator.Hydrate(
                    model.Workflow, typeof(WorkflowSummaryDto), maxDepth, depth
                );

                dto = new BacklogItemTypeDto(model.ID, model.Title, model.CreatedOn, backlogItemTypeSchemaSummaryDto, workflowSummaryDto);
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
            if (to is not BacklogItemTypeDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (BacklogItemTypeDto)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.BacklogItemType)
            {
                var model = (Application.Models.BacklogItemType)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.BacklogItemTypeSchema = (BacklogItemTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemTypeSchema, typeof(BacklogItemTypeSchemaSummaryDto), maxDepth, depth
                    );

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
