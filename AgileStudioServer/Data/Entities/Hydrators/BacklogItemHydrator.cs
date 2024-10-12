
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class BacklogItemHydrator : AbstractEntityHydrator
    {
        public BacklogItemHydrator(DBContext dBContext) : base(dBContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.BacklogItem) && 
                to == typeof(BacklogItem);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? entity = null;

            if (from is Application.Models.BacklogItem)
            {
                var model = (Application.Models.BacklogItem)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItem.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItem), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItem(model.Title);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
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
                throw new HydrationNotSupported(from.GetType(), to.GetType());
            }

            var entity = (BacklogItem)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.BacklogItem)
            {
                var model = (Application.Models.BacklogItem)from;

                entity.ID = model.ID;
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.Project = (Project)referenceHydrator.Hydrate(
                        model.Project, typeof(Project), maxDepth, nextDepth
                    );

                    entity.BacklogItemType = (BacklogItemType)referenceHydrator.Hydrate(
                        model.BacklogItemType, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    entity.WorkflowState = (WorkflowState)referenceHydrator.Hydrate(
                        model.WorkflowState, typeof(WorkflowState), maxDepth, nextDepth
                    );

                    if (model.Sprint != null)
                    {
                        entity.Sprint = (Sprint)referenceHydrator.Hydrate(
                            model.Sprint, typeof(Sprint), maxDepth, nextDepth
                        );
                    }

                    if (model.Release != null)
                    {
                        entity.Release = (Release)referenceHydrator.Hydrate(
                            model.Release, typeof(Release), maxDepth, nextDepth
                        );
                    }

                    if (model.CreatedBy != null)
                    {
                        entity.CreatedBy = (User)referenceHydrator.Hydrate(
                            model.CreatedBy, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}
