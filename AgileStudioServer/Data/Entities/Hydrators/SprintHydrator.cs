
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators;

public class SprintHydrator : AbstractEntityHydrator
{
    private ProjectHydrator _projectHydrator;
    private UserHydrator _userHydrator;

    public SprintHydrator(
        DBContext dBContext,
        ProjectHydrator projectHydrator,
        UserHydrator userHydrator) : base(dBContext)
    {
        _projectHydrator = projectHydrator;
        _userHydrator = userHydrator;
    }

    public Sprint Hydrate(Application.Models.Sprint model, Sprint? entity = null)
    {
        if(entity == null)
        {
            if (model.ID > 0)
            {
                entity = _DBContext.Sprint.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Sprint),
                        model.ID.ToString()
                    );
                }
            }
            else
            {
                entity = new Sprint(model.SprintNumber);
            }
        }

        entity.SprintNumber = model.SprintNumber;
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
