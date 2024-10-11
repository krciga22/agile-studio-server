
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class ProjectSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.Project) &&
                to == typeof(ProjectSummaryDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(ProjectSummaryDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.Project)
            {
                var model = (Application.Models.Project)from;
                dto = new ProjectSummaryDto(model.ID, model.Title);
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
            if (to is not ProjectSummaryDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (ProjectSummaryDto)to;

            if (from is Application.Models.Project)
            {
                var model = (Application.Models.Project)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
