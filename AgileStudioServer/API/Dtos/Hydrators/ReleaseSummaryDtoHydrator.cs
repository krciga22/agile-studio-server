
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class ReleaseSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.Release) &&
                to == typeof(ReleaseSummaryDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(ReleaseSummaryDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.Release)
            {
                var model = (Application.Models.Release)from;
                dto = new ReleaseSummaryDto(model.ID, model.Title);
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
            if (to is not ReleaseSummaryDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (ReleaseSummaryDto)to;

            if (from is Application.Models.Release)
            {
                var model = (Application.Models.Release)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
