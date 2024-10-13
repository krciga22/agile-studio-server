
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class ChildBacklogItemTypeHydrator : AbstractModelHydrator
    {
        public ChildBacklogItemTypeHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Data.Entities.ChildBacklogItemType) && 
                to == typeof(ChildBacklogItemType);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? model = null;

            if (from is Data.Entities.ChildBacklogItemType)
            {
                model = new ChildBacklogItemType();
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
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
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (ChildBacklogItemType)to;
            int nextDepth = depth + 1;

            if (from is Data.Entities.ChildBacklogItemType)
            {
                var entity = (Data.Entities.ChildBacklogItemType)from;

                model.ID = entity.ID;
                model.CreatedOn = entity.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    model.ChildType = (BacklogItemType)referenceHydrator.Hydrate(
                        entity.ChildType, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    model.ParentType = (BacklogItemType)referenceHydrator.Hydrate(
                        entity.ParentType, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    model.Schema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        entity.Schema, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    if (entity.CreatedBy != null)
                    {
                        model.CreatedBy = (User)referenceHydrator.Hydrate(
                            entity.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}
