
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.BacklogItem) &&
                to == typeof(BacklogItemSummaryDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(BacklogItemSummaryDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.BacklogItem)
            {
                var model = (Application.Models.BacklogItem)from;
                dto = new BacklogItemSummaryDto(model.ID, model.Title);
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
            if (to is not BacklogItemSummaryDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (BacklogItemSummaryDto)to;

            if (from is Application.Models.BacklogItem)
            {
                var model = (Application.Models.BacklogItem)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
