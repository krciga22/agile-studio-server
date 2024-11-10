
using AgileStudioServer.API.Dtos.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) || 
                from == typeof(Application.Models.BacklogItem)
            ) && to == typeof(BacklogItemDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            if (referenceHydrator == null)
            {
                throw new ReferenceHydratorRequiredException(this);
            }

            Application.Models.BacklogItem? model = null;
            if (from is int)
            {
                model = (Application.Models.BacklogItem)referenceHydrator.Hydrate(
                    from, typeof(Application.Models.BacklogItem), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is Application.Models.BacklogItem)
            {
                model = (Application.Models.BacklogItem)from;
            }

            Object? dto = null;

            if (model != null)
            {
                var projectSummaryDto = (ProjectSummaryDto)referenceHydrator.Hydrate(
                    model.ProjectID, typeof(ProjectSummaryDto), maxDepth, depth
                );

                var backlogItemTypeSummaryDto = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemTypeID, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                );

                var workflowStateSummaryDto = (WorkflowStateSummaryDto)referenceHydrator.Hydrate(
                    model.WorkflowStateID, typeof(WorkflowStateSummaryDto), maxDepth, depth
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
                        model.ProjectID, typeof(ProjectSummaryDto), maxDepth, depth
                    );

                    dto.BacklogItemType = (BacklogItemTypeSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemTypeID, typeof(BacklogItemTypeSummaryDto), maxDepth, depth
                    );

                    dto.WorkflowState = (WorkflowStateSummaryDto)referenceHydrator.Hydrate(
                        model.WorkflowStateID, typeof(WorkflowStateSummaryDto), maxDepth, depth
                    );

                    if (model.CreatedByID != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedByID, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }

                    if (model.SprintID != null)
                    {
                        dto.Sprint = (SprintSummaryDto)referenceHydrator.Hydrate(
                            model.SprintID, typeof(SprintSummaryDto), maxDepth, depth
                        );
                    }

                    if (model.ReleaseID != null)
                    {
                        dto.Release = (ReleaseSummaryDto)referenceHydrator.Hydrate(
                            model.ReleaseID, typeof(ReleaseSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
