
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class WorkflowConverter : IModelEntityConverter<Workflow, Data.Entities.Workflow>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(Workflow) && entity == typeof(Data.Entities.Workflow);
        }

        public Data.Entities.Workflow ConvertToEntity(Workflow model)
        {
            var entity = new Data.Entities.Workflow(model.Title);

            if(model.CreatedBy != null){
                entity.CreatedBy = new UserConverter().ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public Workflow ConvertToModel(Data.Entities.Workflow entity)
        {
            var model = new Workflow(entity.Title);

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = new UserConverter().ConvertToModel(entity.CreatedBy);
            }

            return model;
        }
    }
}
