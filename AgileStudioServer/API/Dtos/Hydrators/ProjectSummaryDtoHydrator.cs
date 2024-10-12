
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

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
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? dto = null;

            if (from is Application.Models.Project)
            {
                var model = (Application.Models.Project)from;
                dto = new ProjectSummaryDto(model.ID, model.Title);
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
                throw new HydrationNotSupported(from.GetType(), to.GetType());
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
