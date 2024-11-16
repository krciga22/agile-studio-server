
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
            return (
                from == typeof(int) || 
                from == typeof(Application.Models.BacklogItem)
            ) && to == typeof(BacklogItem);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
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
                    entity = new BacklogItem(
                        model.Title, 
                        model.ProjectID, 
                        model.BacklogItemTypeID, 
                        model.WorkflowStateID);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.BacklogItem.Find(from);
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

            var entity = (BacklogItem)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.BacklogItem)
            {
                var model = (Application.Models.BacklogItem)from;

                entity.ID = model.ID;
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.CreatedOn = model.CreatedOn;
                entity.ProjectID = model.ProjectID;
                entity.BacklogItemTypeID = model.BacklogItemTypeID;
                entity.WorkflowStateID = model.WorkflowStateID;
                entity.SprintID = model.SprintID;
                entity.ReleaseID = model.ReleaseID;
                entity.CreatedByID = model.CreatedByID;
                entity.ParentBacklogItemId = model.ParentBacklogItemId;
;
                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.Project = (Project)referenceHydrator.Hydrate(
                        model.ProjectID, typeof(Project), maxDepth, nextDepth
                    );

                    entity.BacklogItemType = (BacklogItemType)referenceHydrator.Hydrate(
                        model.BacklogItemTypeID, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    entity.WorkflowState = (WorkflowState)referenceHydrator.Hydrate(
                        model.WorkflowStateID, typeof(WorkflowState), maxDepth, nextDepth
                    );

                    if (model.SprintID != null)
                    {
                        entity.Sprint = (Sprint)referenceHydrator.Hydrate(
                            model.SprintID, typeof(Sprint), maxDepth, nextDepth
                        );
                    }

                    if (model.ReleaseID != null)
                    {
                        entity.Release = (Release)referenceHydrator.Hydrate(
                            model.ReleaseID, typeof(Release), maxDepth, nextDepth
                        );
                    }

                    if (model.CreatedByID != null)
                    {
                        entity.CreatedBy = (User)referenceHydrator.Hydrate(
                            model.CreatedByID, typeof(User), maxDepth, nextDepth
                        );
                    }

                    if (model.ParentBacklogItemId != null)
                    {
                        entity.ParentBacklogItem = (BacklogItem)referenceHydrator.Hydrate(
                            model.ParentBacklogItemId, typeof(BacklogItem), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}
