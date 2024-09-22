
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class BacklogItemTypeSchemaConverter : IModelEntityConverter<BacklogItemTypeSchema, Data.Entities.BacklogItemTypeSchema>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(BacklogItemTypeSchema) && 
                entity == typeof(Data.Entities.BacklogItemTypeSchema);
        }

        public Data.Entities.BacklogItemTypeSchema ConvertToEntity(BacklogItemTypeSchema model)
        {
            var entity = new Data.Entities.BacklogItemTypeSchema(model.Title);

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = new UserConverter().ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public BacklogItemTypeSchema ConvertToModel(Data.Entities.BacklogItemTypeSchema entity)
        {
            var model = new BacklogItemTypeSchema(entity.Title);

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = new UserConverter().ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
