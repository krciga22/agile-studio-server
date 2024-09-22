
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class SprintConverter : IModelEntityConverter<Sprint, Data.Entities.Sprint>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(Sprint) && entity == typeof(Data.Entities.Sprint);
        }

        public Data.Entities.Sprint ConvertToEntity(Sprint model)
        {
            var entity = new Data.Entities.Sprint(model.SprintNumber);

            if(model.Project != null) {
                entity.Project = new ProjectConverter()
                    .ConvertToEntity(model.Project);
            }

            if(model.CreatedBy != null){
                entity.CreatedBy = new UserConverter().ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public Sprint ConvertToModel(Data.Entities.Sprint entity)
        {
            var model = new Sprint(entity.SprintNumber);

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
