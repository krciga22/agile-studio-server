
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators;

public class WorkflowHydrator : AbstractEntityHydrator
{
    private UserHydrator _userHydrator;

    public WorkflowHydrator(
        DBContext dBContext,
        UserHydrator userHydrator) : base(dBContext)
    {
        _userHydrator = userHydrator;
    }

    public bool CanConvert(Type model, Type entity)
    {
        return model == typeof(Workflow) && entity == typeof(Data.Entities.Workflow);
    }

    public Workflow Hydrate(Application.Models.Workflow model, Workflow? entity = null)
    {
        if (entity == null)
        {
            if (model.ID > 0)
            {
                entity = _DBContext.Workflow.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Workflow),
                        model.ID.ToString()
                    );
                }
            }
            else
            {
                entity = new Workflow(model.Title);
            }
        }

        entity.Title = model.Title;

        if (model.CreatedBy != null){
            entity.CreatedBy = _userHydrator.Hydrate(model.CreatedBy);
        }

        return entity;
    }
}
