
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators;

public class WorkflowHydrator : AbstractEntityHydrator
{
    public WorkflowHydrator(DBContext dBContext) : base(dBContext)
    {

    }

    public override bool Supports(Type from, Type to)
    {
        return from == typeof(Application.Models.Workflow) &&
            to == typeof(Workflow);
    }

    public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
    {
        if (!Supports(from.GetType(), to))
        {
            throw new HydrationNotSupported(from.GetType(), to);
        }

        Object? entity = null;

        if (from is Application.Models.Workflow)
        {
            var model = (Application.Models.Workflow)from;
            if (model.ID > 0)
            {
                entity = _DBContext.Workflow.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Workflow), model.ID.ToString());
                }
            }
            else
            {
                entity = new Workflow(model.Title);
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

        var entity = (Workflow)to;
        int nextDepth = depth + 1;

        if (from is Application.Models.Workflow)
        {
            var model = (Application.Models.Workflow)from;

            entity.ID = model.ID;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.CreatedOn = model.CreatedOn;

            if (referenceHydrator != null && nextDepth <= maxDepth)
            {
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
