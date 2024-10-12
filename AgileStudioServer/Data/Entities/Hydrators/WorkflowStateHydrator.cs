
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class WorkflowStateHydrator : AbstractEntityHydrator
    {
        public WorkflowStateHydrator(DBContext dBContext) : base(dBContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return from == typeof(Application.Models.WorkflowState) &&
                to == typeof(WorkflowState);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupported(from.GetType(), to);
            }

            Object? entity = null;

            if (from is Application.Models.WorkflowState)
            {
                var model = (Application.Models.WorkflowState)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.WorkflowState.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(WorkflowState), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new WorkflowState(model.Title);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }

            if (entity == null)
            {
                throw new Exception("Hydration failed for from and to"); // todo
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupported(from.GetType(), to.GetType());
            }

            var entity = (WorkflowState)to;
            int nextDepth = depth + 1;

            if (from is Application.Models.WorkflowState)
            {
                var model = (Application.Models.WorkflowState)from;

                entity.ID = model.ID;
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
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
