
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemTypeSchemaSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.BacklogItemTypeSchema) &&
                to == typeof(BacklogItemTypeSchemaSummaryDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(BacklogItemTypeSchemaSummaryDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.BacklogItemTypeSchema)
            {
                var model = (Application.Models.BacklogItemTypeSchema)from;
                dto = new BacklogItemTypeSchemaSummaryDto(model.ID, model.Title);
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
            if (to is not BacklogItemTypeSchemaSummaryDto)
            {
                throw new Exception("Unsupported to");
            }

            var dto = (BacklogItemTypeSchemaSummaryDto)to;

            if (from is Application.Models.BacklogItemTypeSchema)
            {
                var model = (Application.Models.BacklogItemTypeSchema)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
