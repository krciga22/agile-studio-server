
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class BacklogItemTypeHydrator : AbstractEntityHydrator
    {
        public BacklogItemTypeHydrator(DBContext _dbContext) : base(_dbContext)
        {
            
        }

        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.BacklogItemType) && 
                to == typeof(BacklogItemType);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            Object? entity = null;

            if (from is Application.Models.BacklogItemType)
            {
                var model = (Application.Models.BacklogItemType)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemType.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemType), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItemType(
                        model.Title, model.BacklogItemTypeSchema.ID, model.Workflow.ID);
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

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var entity = (BacklogItemType)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.BacklogItemType)
            {
                var model = (Application.Models.BacklogItemType)from;

                entity.ID = model.ID;
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.CreatedOn = model.CreatedOn;
                entity.BacklogItemTypeSchemaID = model.BacklogItemTypeSchema.ID;
                entity.WorkflowID = model.Workflow.ID;
                entity.CreatedByID = model.CreatedBy?.ID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.BacklogItemTypeSchema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        model.BacklogItemTypeSchema, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    entity.Workflow = (Workflow)referenceHydrator.Hydrate(
                        model.Workflow, typeof(Workflow), maxDepth, nextDepth
                    );

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
