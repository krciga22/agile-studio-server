
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class BacklogItemTypeHydrator : AbstractModelHydrator
    {
        private DBContext _DBContext;
        private BacklogItemTypeSchemaHydrator _backlogItemTypeSchemaHydrator;
        private UserHydrator _userHydrator;
        private WorkflowHydrator _workflowHydrator;

        public BacklogItemTypeHydrator(
            DBContext dbContext,
            BacklogItemTypeSchemaHydrator backlogItemTypeSchemaHydrator,
            UserHydrator userHydrator,
            WorkflowHydrator workflowHydrator)
        {
            _DBContext = dbContext;
            _backlogItemTypeSchemaHydrator = backlogItemTypeSchemaHydrator;
            _userHydrator = userHydrator;
            _workflowHydrator = workflowHydrator;
        }

        public BacklogItemType Hydrate(Data.Entities.BacklogItemType entity, BacklogItemType? model = null)
        {
            model ??= new BacklogItemType(entity.Title);

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            if (entity.BacklogItemTypeSchema != null)
            {
                model.BacklogItemTypeSchema = _backlogItemTypeSchemaHydrator.Hydrate(entity.BacklogItemTypeSchema);
            }

            if (entity.Workflow != null)
            {
                model.Workflow = _workflowHydrator.Hydrate(entity.Workflow);
            }

            return model;
        }

        public BacklogItemType Hydrate(API.DtosNew.BacklogItemTypePostDto dto, BacklogItemType? model = null)
        {
            model ??= new BacklogItemType(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;

            Data.Entities.BacklogItemTypeSchema? backlogItemTypeSchemaEntity =
                    _DBContext.BacklogItemTypeSchema.Find(dto.BacklogItemTypeSchemaId) ??
                        throw new ModelNotFoundException(
                            nameof(BacklogItemTypeSchema), 
                            dto.BacklogItemTypeSchemaId.ToString()
                        );
            model.BacklogItemTypeSchema = _backlogItemTypeSchemaHydrator.Hydrate(backlogItemTypeSchemaEntity);

            Data.Entities.Workflow? workflowEntity =
                    _DBContext.Workflow.Find(dto.WorkflowId) ??
                        throw new ModelNotFoundException(nameof(Workflow), dto.WorkflowId.ToString());
            model.Workflow = _workflowHydrator.Hydrate(workflowEntity);

            return model;
        }

        public BacklogItemType Hydrate(API.DtosNew.BacklogItemTypePatchDto dto, BacklogItemType? model = null)
        {
            if (model == null)
            {
                Data.Entities.BacklogItemType? backlogItemTypeEntity = 
                    _DBContext.BacklogItemType.Find(dto.ID) ??
                        throw new ModelNotFoundException(nameof(BacklogItemType), dto.ID.ToString());

                model ??= Hydrate(backlogItemTypeEntity);
            }

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }
    }
}
