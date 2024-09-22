
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class ProjectConverter : IModelEntityConverter<Project, Data.Entities.Project>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(Project) && entity == typeof(Data.Entities.Project);
        }

        public Data.Entities.Project ConvertToEntity(Project model)
        {
            var entity = new Data.Entities.Project(model.Title);

            if(model.BacklogItemTypeSchema != null) {
                entity.BacklogItemTypeSchema = new BacklogItemTypeSchemaConverter()
                    .ConvertToEntity(model.BacklogItemTypeSchema);
            }

            if(model.CreatedBy != null){
                entity.CreatedBy = new UserConverter().ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public Project ConvertToModel(Data.Entities.Project entity)
        {
            var model = new Project(entity.Title);

            if (entity.BacklogItemTypeSchema != null)
            {
                model.BacklogItemTypeSchema = new BacklogItemTypeSchemaConverter()
                    .ConvertToModel(entity.BacklogItemTypeSchema);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = new UserConverter().ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
