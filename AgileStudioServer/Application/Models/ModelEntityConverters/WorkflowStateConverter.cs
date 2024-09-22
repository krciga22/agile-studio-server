
using AgileStudioServer.Data;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Application.Models.ModelEntityConverters
{
    public class WorkflowStateConverter : AbstractModelEntityConverter, IModelEntityConverter<WorkflowState, Data.Entities.WorkflowState>
    {
        private WorkflowConverter _workflowConverter;
        private UserConverter _userConverter;

        public WorkflowStateConverter(
            DBContext dBContext,
            WorkflowConverter workflowConverter,
            UserConverter userConverter) : base(dBContext)
        {
            _workflowConverter = workflowConverter;
            _userConverter = userConverter;
        }

        public bool CanConvert(Type model, Type entity)
        {
            return model == typeof(WorkflowState) && entity == typeof(Data.Entities.WorkflowState);
        }

        public Data.Entities.WorkflowState ConvertToEntity(WorkflowState model)
        {
            Data.Entities.WorkflowState? entity = null;

            if (model.ID > 0)
            {
                entity = _DBContext.WorkflowState.Find(model.ID);
                if (entity == null)
                {
                    throw new EntityNotFoundException(
                        nameof(Data.Entities.WorkflowState),
                        model.ID.ToString()
                    );
                }

                entity.Title = model.Title;
            }
            else
            {
                entity = new Data.Entities.WorkflowState(model.Title);
            }

            if (model.Workflow != null)
            {
                entity.Workflow = _workflowConverter.ConvertToEntity(model.Workflow);
            }

            if (model.CreatedBy != null){
                entity.CreatedBy = _userConverter.ConvertToEntity(model.CreatedBy);
            }

            return entity;
        }

        public WorkflowState ConvertToModel(Data.Entities.WorkflowState entity)
        {
            var model = new WorkflowState(entity.Title)
            {
                ID = entity.ID
            };

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
