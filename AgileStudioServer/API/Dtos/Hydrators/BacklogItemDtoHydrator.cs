
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.BacklogItem) &&
                to == typeof(BacklogItemDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? dto = null;

            if (from is Application.Models.BacklogItem && referenceHydrator != null)
            {
                var model = (Application.Models.BacklogItem)from;

                var projectSummaryDto = (ProjectSummaryDto)referenceHydrator.Hydrate(
                    model.Project, typeof(ProjectSummaryDto), maxDepth, depth
                );

                var backlogItemTypeSummaryDto = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemType, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                );

                var workflowStateSummaryDto = (WorkflowStateSummaryDto)referenceHydrator.Hydrate(
                    model.WorkflowState, typeof(WorkflowStateSummaryDto), maxDepth, depth
                );

                dto = new BacklogItemDto(model.ID, model.Title, model.CreatedOn, projectSummaryDto, backlogItemTypeSummaryDto, workflowStateSummaryDto);
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

            var dto = (BacklogItemDto)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.BacklogItem)
            {
                var model = (Application.Models.BacklogItem)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.Project = (ProjectSummaryDto)referenceHydrator.Hydrate(
                        model.Project, typeof(ProjectSummaryDto), maxDepth, depth
                    );

                    dto.BacklogItemType = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemType, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                    );

                    dto.WorkflowState = (WorkflowStateSummaryDto)referenceHydrator.Hydrate(
                        model.WorkflowState, typeof(WorkflowStateSummaryDto), maxDepth, depth
                    );

                    if (model.CreatedBy != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedBy, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }

                    if (model.Sprint != null)
                    {
                        dto.Sprint = (SprintSummaryDto)referenceHydrator.Hydrate(
                            model.Sprint, typeof(SprintSummaryDto), maxDepth, depth
                        );
                    }

                    if (model.Release != null)
                    {
                        dto.Release = (ReleaseSummaryDto)referenceHydrator.Hydrate(
                            model.Release, typeof(ReleaseSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
