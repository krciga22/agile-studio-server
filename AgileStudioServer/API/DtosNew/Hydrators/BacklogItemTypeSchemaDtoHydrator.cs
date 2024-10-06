
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class BacklogItemTypeSchemaDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.BacklogItemTypeSchema) &&
                to == typeof(BacklogItemTypeSchemaDto);
        }

        public override Object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            Object? dto = null;

            if (to != typeof(BacklogItemTypeSchemaDto))
            {
                throw new Exception("Unsupported to"); // todo
            }

            if (from is Application.Models.BacklogItemTypeSchema)
            {
                var model = (Application.Models.BacklogItemTypeSchema)from;
                dto = new BacklogItemTypeSchemaDto(model.ID, model.Title, model.CreatedOn);
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
            if (to is not BacklogItemTypeSchemaDto)
            {
                throw new Exception("Unsupported to");
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
