
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class BacklogItemTypeConverter : AbstractModelEntityConverter, IModelEntityConverter<BacklogItemType, Data.Entities.BacklogItemType>
    {
        private BacklogItemTypeSchemaConverter _backlogItemTypeSchemaConverter;
        private WorkflowConverter _workflowConverter;
        private UserConverter _userConverter;

        public BacklogItemTypeConverter(
            DBContext _dbContext,
            BacklogItemTypeSchemaConverter backlogItemTypeSchemaConverter,
            WorkflowConverter workflowConverter,
            UserConverter userConverter) : base(_dbContext)
        {
            _backlogItemTypeSchemaConverter = backlogItemTypeSchemaConverter;
            _workflowConverter = workflowConverter;
            _userConverter = userConverter;
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(BacklogItemType) && 
                entity == typeof(Data.Entities.BacklogItemType);
        }

        public Data.Entities.BacklogItemType ConvertToEntity(BacklogItemType model)
        {
            Data.Entities.BacklogItemType? entity = null;

            if (model.ID > 0)
            {
                entity = _DBContext.BacklogItemType.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.BacklogItemType),
                        model.ID.ToString()
                    );
                }

                entity.Title = model.Title;
            }
            else
            {
                entity = new Data.Entities.BacklogItemType(model.Title);
            }

            if (model.BacklogItemTypeSchema != null)
            {
                entity.BacklogItemTypeSchema = _backlogItemTypeSchemaConverter
                    .ConvertToEntity(model.BacklogItemTypeSchema);
            }

            if (model.Workflow != null)
            {
                entity.Workflow = _workflowConverter.ConvertToEntity(model.Workflow);
            }

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = _userConverter.ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public BacklogItemType ConvertToModel(Data.Entities.BacklogItemType entity)
        {
            var model = new BacklogItemType(entity.Title)
            {
                ID = entity.ID
            };

            if (entity.BacklogItemTypeSchema != null)
            {
                model.BacklogItemTypeSchema = _backlogItemTypeSchemaConverter
                    .ConvertToModel(entity.BacklogItemTypeSchema);
            }

            if (entity.Workflow != null)
            {
                model.Workflow = _workflowConverter.ConvertToModel(entity.Workflow);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userConverter.ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
