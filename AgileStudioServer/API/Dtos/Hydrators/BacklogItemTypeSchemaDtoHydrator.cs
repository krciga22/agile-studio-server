
using AgileStudioServer.API.Dtos.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.API.Dtos.Hydrators
{
    public class BacklogItemTypeSchemaDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||  
                from == typeof(Application.Models.BacklogItemTypeSchema)
            ) && to == typeof(BacklogItemTypeSchemaDto);
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
                dto = new BacklogItemTypeSchemaDto(model.ID, model.Title, model.CreatedOn);
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

            var dto = (BacklogItemTypeSchemaDto)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.BacklogItemTypeSchema)
            {
                var model = (Application.Models.BacklogItemTypeSchema)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    if (model.CreatedById != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedById, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
