
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemTypeSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.BacklogItemType) &&
                to == typeof(BacklogItemTypeSummaryDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(BacklogItemTypeSummaryDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.BacklogItemType)
            {
                var model = (Application.Models.BacklogItemType)from;
                dto = new BacklogItemTypeSummaryDto(model.ID, model.Title, model.CreatedOn);
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
            if (to is not BacklogItemTypeSummaryDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (BacklogItemTypeSummaryDto)to;

            if (from is Application.Models.BacklogItemType)
            {
                var model = (Application.Models.BacklogItemType)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;
            }
        }
    }
}
