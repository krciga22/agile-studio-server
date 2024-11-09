
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class BacklogItemTypeSchemaHydrator : AbstractEntityHydrator
    {
        public BacklogItemTypeSchemaHydrator(DBContext dBContext) : base(dBContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Application.Models.BacklogItemTypeSchema) 
            ) && to == typeof(BacklogItemTypeSchema);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? entity = null;

            if (from is Application.Models.BacklogItemTypeSchema)
            {
                var model = (Application.Models.BacklogItemTypeSchema)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemTypeSchema.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemTypeSchema), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItemTypeSchema(model.Title);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.BacklogItemTypeSchema.Find(from);
            }

            if (entity == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var entity = (BacklogItemTypeSchema)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.BacklogItemTypeSchema)
            {
                var model = (Application.Models.BacklogItemTypeSchema)from;

                entity.ID = model.ID;
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    if (model.CreatedById != null)
                    {
                        entity.CreatedBy = (User)referenceHydrator.Hydrate(
                            model.CreatedById, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}
