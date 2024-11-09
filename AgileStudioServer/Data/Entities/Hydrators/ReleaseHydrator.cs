
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators;

public class ReleaseHydrator : AbstractEntityHydrator
{
    public ReleaseHydrator(DBContext dBContext) : base(dBContext)
    {

    }

    public override bool Supports(Type from, Type to)
    {
        return from == typeof(Application.Models.Release) &&
            to == typeof(Release);
    }

    public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
    {
        if (!Supports(from.GetType(), to))
        {
            throw new HydrationNotSupportedException(from.GetType(), to);
        }

        Object? entity = null;

        if (from is Application.Models.Release)
        {
            var model = (Application.Models.Release)from;
            if (model.ID > 0)
            {
                entity = _DBContext.Release.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Release), model.ID.ToString());
                }
            }
            else
            {
                entity = new Release(model.Title, model.Project.ID);
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
            throw new HydrationNotSupportedException(from.GetType(), to.GetType());
        }

        var entity = (Release)to;
        int nextDepth = depth + 1;

        if (from is Application.Models.Release)
        {
            var model = (Application.Models.Release)from;

            entity.ID = model.ID;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.CreatedOn = model.CreatedOn;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.ProjectID = model.Project.ID;
            entity.CreatedByID = model.CreatedBy?.ID;

            if (referenceHydrator != null && nextDepth <= maxDepth)
            {
                entity.Project = (Project)referenceHydrator.Hydrate(
                    model.Project, typeof(Project), maxDepth, nextDepth
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
