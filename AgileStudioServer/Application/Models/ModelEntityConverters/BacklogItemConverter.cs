
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class BacklogItemConverter : IModelEntityConverter<BacklogItem, Data.Entities.BacklogItem>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(BacklogItem) && 
                entity == typeof(Data.Entities.BacklogItem);
        }

        public Data.Entities.BacklogItem ConvertToEntity(BacklogItem model)
        {
            var entity = new Data.Entities.BacklogItem(model.Title);

            if (model.Project != null)
            {
                entity.Project = new ProjectConverter()
                    .ConvertToEntity(model.Project);
            }

            if (model.Sprint != null)
            {
                entity.Sprint = new SprintConverter()
                    .ConvertToEntity(model.Sprint);
            }

            if (model.Release != null)
            {
                entity.Release = new ReleaseConverter()
                    .ConvertToEntity(model.Release);
            }

            if (model.BacklogItemType != null)
            {
                entity.BacklogItemType = new BacklogItemTypeConverter()
                    .ConvertToEntity(model.BacklogItemType);
            }

            if (model.WorkflowState != null)
            {
                entity.WorkflowState = new WorkflowStateConverter()
                    .ConvertToEntity(model.WorkflowState);
            }

            if (model.CreatedBy != null)
            {
                entity.CreatedBy = new UserConverter().ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public BacklogItem ConvertToModel(Data.Entities.BacklogItem entity)
        {
            var model = new BacklogItem(entity.Title);

            if (entity.Project != null)
            {
                model.Project = new ProjectConverter()
                    .ConvertToModel(entity.Project);
            }

            if (entity.Sprint != null)
            {
                model.Sprint = new SprintConverter()
                    .ConvertToModel(entity.Sprint);
            }

            if (entity.Release != null)
            {
                model.Release = new ReleaseConverter()
                    .ConvertToModel(entity.Release);
            }

            if (entity.BacklogItemType != null)
            {
                model.BacklogItemType = new BacklogItemTypeConverter()
                    .ConvertToModel(entity.BacklogItemType);
            }

            if (entity.WorkflowState != null)
            {
                model.WorkflowState = new WorkflowStateConverter()
                    .ConvertToModel(entity.WorkflowState);
            }

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = new UserConverter().ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
