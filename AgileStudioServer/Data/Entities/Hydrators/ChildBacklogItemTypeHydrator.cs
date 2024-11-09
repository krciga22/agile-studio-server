
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class ChildBacklogItemTypeHydrator : AbstractEntityHydrator
    {
        public ChildBacklogItemTypeHydrator(DBContext _dbContext) : base(_dbContext)
        {
            
        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) || 
                from == typeof(Application.Models.ChildBacklogItemType) 
            ) && to == typeof(ChildBacklogItemType);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? entity = null;

            if (from is Application.Models.ChildBacklogItemType)
            {
                var model = (Application.Models.ChildBacklogItemType)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.ChildBacklogItemType.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(ChildBacklogItemType), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new ChildBacklogItemType(
                        model.ChildTypeID, model.ParentTypeID, model.SchemaID);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.ChildBacklogItemType.Find(from);
            }

            if (entity == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var entity = (ChildBacklogItemType)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.ChildBacklogItemType)
            {
                var model = (Application.Models.ChildBacklogItemType)from;

                entity.ID = model.ID;
                entity.CreatedOn = model.CreatedOn;
                entity.ChildTypeID = model.ChildTypeID;
                entity.ParentTypeID = model.ParentTypeID;
                entity.SchemaID = model.SchemaID;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.ChildType = (BacklogItemType)referenceHydrator.Hydrate(
                        model.ChildTypeID, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    entity.ParentType = (BacklogItemType)referenceHydrator.Hydrate(
                        model.ParentTypeID, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    entity.Schema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        model.SchemaID, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    if (model.CreatedByID != null)
                    {
                        entity.CreatedBy = (User)referenceHydrator.Hydrate(
                            model.CreatedByID, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}
