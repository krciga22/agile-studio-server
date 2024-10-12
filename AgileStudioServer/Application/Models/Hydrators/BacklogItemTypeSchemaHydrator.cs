
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemTypeSchemaHydrator : AbstractModelHydrator
    {
        public BacklogItemTypeSchemaHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(Data.Entities.BacklogItemTypeSchema)
                || from == typeof(API.Dtos.BacklogItemTypeSchemaPostDto)
                || from == typeof(API.Dtos.BacklogItemTypeSchemaPatchDto)
            ) && to == typeof(BacklogItemTypeSchema);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? model = null;

            if (from is Data.Entities.BacklogItemTypeSchema)
            {
                var entity = (Data.Entities.BacklogItemTypeSchema)from;
                model = new BacklogItemTypeSchema(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.BacklogItemTypeSchemaPostDto)
            {
                var dto = (API.Dtos.BacklogItemTypeSchemaPostDto)from;
                model = new BacklogItemTypeSchema(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is API.Dtos.BacklogItemTypeSchemaPatchDto)
            {
                var dto = (API.Dtos.BacklogItemTypeSchemaPatchDto)from;
                var entity = _DBContext.BacklogItemTypeSchema.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItemTypeSchema), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
            }

            if (model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupported(from.GetType(), to.GetType());
            }

            var model = (BacklogItemTypeSchema)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.BacklogItemTypeSchema)
            {
                var entity = (Data.Entities.BacklogItemTypeSchema)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)referenceHydrator.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
            else if (from is API.Dtos.BacklogItemTypeSchemaPostDto)
            {
                var dto = (API.Dtos.BacklogItemTypeSchemaPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
            else if (from is API.Dtos.BacklogItemTypeSchemaPatchDto)
            {
                var dto = (API.Dtos.BacklogItemTypeSchemaPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
