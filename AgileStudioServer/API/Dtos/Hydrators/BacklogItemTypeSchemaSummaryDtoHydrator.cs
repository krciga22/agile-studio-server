
using AgileStudioServer.API.Dtos.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemTypeSchemaSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) || 
                from == typeof(Application.Models.BacklogItemTypeSchema)
            ) && to == typeof(BacklogItemTypeSchemaSummaryDto);
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

            Application.Models.BacklogItemTypeSchema? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (Application.Models.BacklogItemTypeSchema)referenceHydrator.Hydrate(
                    from, typeof(Application.Models.BacklogItemTypeSchema), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is Application.Models.BacklogItemTypeSchema)
            {
                model = (Application.Models.BacklogItemTypeSchema)from;
            }

            Object? dto = null;
            if (model != null)
            {
                dto = new BacklogItemTypeSchemaSummaryDto(model.ID, model.Title);
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
