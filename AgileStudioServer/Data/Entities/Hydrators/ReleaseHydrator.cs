
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators;

public class ReleaseHydrator : AbstractEntityHydrator
{
    private ProjectHydrator _projectHydrator;
    private UserHydrator _userHydrator;

    public ReleaseHydrator(
        DBContext dBContext,
        ProjectHydrator projectHydrator,
        UserHydrator userHydrator) : base(dBContext)
    {
        _projectHydrator = projectHydrator;
        _userHydrator = userHydrator;
    }

    public Release Hydrate(Application.Models.Release model, Release? entity = null)
    {
        if(entity == null)
        {
            if (model.ID > 0)
            {
                entity = _DBContext.Release.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Release),
                        model.ID.ToString()
                    );
                }
            }
            else
            {
                entity = new Release(model.Title);
            }
        }

        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.CreatedOn = model.CreatedOn;
        entity.StartDate = model.StartDate;
        entity.EndDate = model.EndDate;

        if (model.Project != null) {
            entity.Project = _projectHydrator.Hydrate(model.Project);
        }

        if(model.CreatedBy != null){
            entity.CreatedBy = _userHydrator.Hydrate(model.CreatedBy);
        }

        return entity;
    }
}
