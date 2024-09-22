
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class BacklogItemTypeSchemaConverter : AbstractModelEntityConverter, IModelEntityConverter<BacklogItemTypeSchema, Data.Entities.BacklogItemTypeSchema>
    {
        private UserConverter _userConverter;

        public BacklogItemTypeSchemaConverter(
            DBContext dBContext, 
            UserConverter userConverter) : base(dBContext)
        {
            _userConverter = userConverter;
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(BacklogItemTypeSchema) && 
                entity == typeof(Data.Entities.BacklogItemTypeSchema);
        }

        public Data.Entities.BacklogItemTypeSchema ConvertToEntity(BacklogItemTypeSchema model)
        {
            Data.Entities.BacklogItemTypeSchema? entity = null;

            if (model.ID > 0)
            {
                entity = _DBContext.BacklogItemTypeSchema.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.BacklogItemTypeSchema),
                        model.ID.ToString()
                    );
                }

                entity.Title = model.Title;
            }
            else
            {
                entity = new Data.Entities.BacklogItemTypeSchema(model.Title);
            }

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = _userConverter.ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public BacklogItemTypeSchema ConvertToModel(Data.Entities.BacklogItemTypeSchema entity)
        {
            var model = new BacklogItemTypeSchema(entity.Title)
            {
                ID = entity.ID
            };

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userConverter.ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
