
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class ProjectDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.Project) &&
                to == typeof(ProjectDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(ProjectDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.Project && referenceHydrator != null)
            {
                var model = (Application.Models.Project)from;

                var backlogItemTypeSchemaSummaryDto = (BacklogItemTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemTypeSchema, typeof(BacklogItemTypeSchemaSummaryDto), maxDepth, depth
                );

                dto = new ProjectDto(model.ID, model.Title, model.CreatedOn, backlogItemTypeSchemaSummaryDto);
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
            if (to is not ProjectDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (ProjectDto)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.Project)
            {
                var model = (Application.Models.Project)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.BacklogItemTypeSchema = (BacklogItemTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemTypeSchema, typeof(BacklogItemTypeSchemaSummaryDto), maxDepth, depth
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
