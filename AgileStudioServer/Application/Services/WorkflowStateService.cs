using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class WorkflowStateService
    {
        private readonly DBContext _DBContext;

        private readonly WorkflowStateConverter _converter;

        public WorkflowStateService(DBContext dbContext, WorkflowStateConverter workflowStateConverter)
        {
            _DBContext = dbContext;
            _converter = workflowStateConverter;
        }

        public virtual List<WorkflowState> GetByWorkflowId(int workflowId)
        {
            List<Data.Entities.WorkflowState> entities = _DBContext.WorkflowState.
                Where(x => x.Workflow.ID == workflowId).ToList();

            List<WorkflowState> models = new();
            entities.ForEach(entity => {
                models.Add(_converter.ConvertToModel(entity));
            });

            return models;
        }

        public virtual WorkflowState? Get(int id)
        {
            Data.Entities.WorkflowState? entity = _DBContext.WorkflowState.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual WorkflowState Create(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = _converter.ConvertToEntity(workflowState);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual WorkflowState Update(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = _converter.ConvertToEntity(workflowState);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = _converter.ConvertToEntity(workflowState);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
