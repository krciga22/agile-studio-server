
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators;

public class ProjectHydrator : AbstractEntityHydrator
{
    public ProjectHydrator(DBContext dBContext) : base(dBContext)
    {
        
    }

    public override bool Supports(Type from, Type to)
    {
        return from == typeof(Application.Models.Project) &&
            to == typeof(Project);
    }

    public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
    {
        Object? entity = null;

        if (to != typeof(Project))
        {
            throw new Exception("Unsupported to"); // todo
        }

        if (from is Application.Models.Project)
        {
            var model = (Application.Models.Project)from;
            if (model.ID > 0)
            {
                entity = _DBContext.Project.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Project), model.ID.ToString());
                }
            }
            else
            {
                entity = new Project(model.Title);
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
        if (to is not Project)
        {
            throw new Exception("Unsupported to");
        }

        var entity = (Project)to;
        int nextDepth = depth + 1;

        if (from is Application.Models.Project)
        {
            var model = (Application.Models.Project)from;

            entity.ID = model.ID;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.CreatedOn = model.CreatedOn;

            if (referenceHydrator != null && nextDepth <= maxDepth)
            {
                entity.BacklogItemTypeSchema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                    model.BacklogItemTypeSchema, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
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
