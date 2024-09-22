
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class ReleaseConverter : IModelEntityConverter<Release, Data.Entities.Release>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(Release) && entity == typeof(Data.Entities.Release);
        }

        public Data.Entities.Release ConvertToEntity(Release model)
        {
            var entity = new Data.Entities.Release(model.Title);

            if(model.Project != null) {
                entity.Project = new ProjectConverter()
                    .ConvertToEntity(model.Project);
            }

            if(model.CreatedBy != null){
                entity.CreatedBy = new UserConverter().ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public Release ConvertToModel(Data.Entities.Release entity)
        {
            var model = new Release(entity.Title);

            if (entity.Project != null)
            {
                model.Project = new ProjectConverter()
                    .ConvertToModel(entity.Project);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = new UserConverter().ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
