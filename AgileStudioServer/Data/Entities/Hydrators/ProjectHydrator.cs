
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators;

public class ProjectHydrator : AbstractEntityHydrator
{
    private BacklogItemTypeSchemaHydrator _backlogItemTypeSchemaHydrator;
    private UserHydrator _userHydrator;

    public ProjectHydrator(
        DBContext dBContext,
        BacklogItemTypeSchemaHydrator backlogItemTypeSchemaHydrator,
        UserHydrator userHydrator) : base(dBContext)
    {
        _backlogItemTypeSchemaHydrator = backlogItemTypeSchemaHydrator;
        _userHydrator = userHydrator;
    }

    public Project Hydrate(Application.Models.Project model, Project? entity = null)
    {
        if(entity == null)
        {
            if (model.ID > 0)
            {
                entity = _DBContext.Project.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Project),
                        model.ID.ToString()
                    );
                }
            }
            else
            {
                entity = new Project(model.Title);
            }
        }

        entity.Title = model.Title;

        if (model.BacklogItemTypeSchema != null) {
            entity.BacklogItemTypeSchema = _backlogItemTypeSchemaHydrator
                .Hydrate(model.BacklogItemTypeSchema);
        }

        if(model.CreatedBy != null){
            entity.CreatedBy = _userHydrator.Hydrate(model.CreatedBy);
        }

        return entity;
    }
}
