
namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class WorkflowStateConverter : IModelEntityConverter<WorkflowState, Data.Entities.WorkflowState>
    {
        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(WorkflowState) && entity == typeof(Data.Entities.WorkflowState);
        }

        public Data.Entities.WorkflowState ConvertToEntity(WorkflowState model)
        {
            var entity = new Data.Entities.WorkflowState(model.Title);

            if (model.Workflow != null)
            {
                entity.Workflow = new WorkflowConverter()
                    .ConvertToEntity(model.Workflow);
            }

            if (model.CreatedBy != null){
                entity.CreatedBy = new UserConverter().ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public WorkflowState ConvertToModel(Data.Entities.WorkflowState entity)
        {
            var model = new WorkflowState(entity.Title);

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
