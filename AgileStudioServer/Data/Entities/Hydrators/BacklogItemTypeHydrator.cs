
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Entities.Hydrators
{
    public class BacklogItemTypeHydrator : AbstractEntityHydrator, IEntityHydrator<Application.Models.BacklogItemType, BacklogItemType>
    {
        private BacklogItemTypeSchemaHydrator _backlogItemTypeSchemaHydrator;
        private WorkflowHydrator _workflowHydrator;
        private UserHydrator _userHydrator;

        public BacklogItemTypeHydrator(
            DBContext _dbContext,
            BacklogItemTypeSchemaHydrator backlogItemTypeSchemaHydrator,
            WorkflowHydrator workflowHydrator,
            UserHydrator userHydrator) : base(_dbContext)
        {
            _backlogItemTypeSchemaHydrator = backlogItemTypeSchemaHydrator;
            _workflowHydrator = workflowHydrator;
            _userHydrator = userHydrator;
        }

        public BacklogItemType Hydrate(Application.Models.BacklogItemType model, BacklogItemType? entity = null)
        {
            if(entity == null)
            {
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemType.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemType),
                            model.ID.ToString()
                        );
                    }
                }
                else
                {
                    entity = new BacklogItemType(model.Title);
                }
            }

            entity.Title = model.Title;

            if (model.BacklogItemTypeSchema != null)
            {
                entity.BacklogItemTypeSchema = _backlogItemTypeSchemaHydrator
                    .Hydrate(model.BacklogItemTypeSchema);
            }

            if (model.Workflow != null)
            {
                entity.Workflow = _workflowHydrator.Hydrate(model.Workflow);
            }

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = _userHydrator.Hydrate(model.CreatedBy);
            }

            return entity;
        }
    }
}
