
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class BacklogItemTypeConverter : IModelEntityConverter<BacklogItemType, Data.Entities.BacklogItemType>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(BacklogItemType) && 
                entity == typeof(Data.Entities.BacklogItemType);
        }

        public Data.Entities.BacklogItemType ConvertToEntity(BacklogItemType model)
        {
            var entity = new Data.Entities.BacklogItemType(model.Title);

            if (model.BacklogItemTypeSchema != null)
            {
                entity.BacklogItemTypeSchema = new BacklogItemTypeSchemaConverter()
                    .ConvertToEntity(model.BacklogItemTypeSchema);
            }

            if (model.Workflow != null)
            {
                entity.Workflow = new WorkflowConverter()
                    .ConvertToEntity(model.Workflow);
            }

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = new UserConverter().ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public BacklogItemType ConvertToModel(Data.Entities.BacklogItemType entity)
        {
            var model = new BacklogItemType(entity.Title);

            if (entity.BacklogItemTypeSchema != null)
            {
                model.BacklogItemTypeSchema = new BacklogItemTypeSchemaConverter()
                    .ConvertToModel(entity.BacklogItemTypeSchema);
            }

            if (entity.Workflow != null)
            {
                model.Workflow = new WorkflowConverter()
                    .ConvertToModel(entity.Workflow);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = new UserConverter().ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
