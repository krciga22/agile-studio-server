using AgileStudioServer.Application.Models;
using AgileStudioServer.Data;
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.Application.Services
{
    public class WorkflowStateService
    {
        private DBContext _DBContext;
        private Hydrator _Hydrator;

        public WorkflowStateService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<WorkflowState> GetByWorkflowId(int workflowId)
        {
            List<Data.Entities.WorkflowState> entities = _DBContext.WorkflowState.
                Where(x => x.Workflow.ID == workflowId).ToList();

            return HydrateWorkflowStateModels(entities);
        }

        public virtual WorkflowState? Get(int id)
        {
            Data.Entities.WorkflowState? entity = _DBContext.WorkflowState.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateWorkflowStateModel(entity);
        }

        public virtual WorkflowState Create(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowStateModel(entity);
        }

        public virtual WorkflowState Update(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowStateModel(entity);
        }

        public virtual void Delete(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<WorkflowState> HydrateWorkflowStateModels(List<Data.Entities.WorkflowState> entities, int depth = 3)
        {
            List<WorkflowState> models = new();

            entities.ForEach(entity => {
                WorkflowState model = HydrateWorkflowStateModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private WorkflowState HydrateWorkflowStateModel(Data.Entities.WorkflowState workflowState, int depth = 3)
        {
            return (WorkflowState)_Hydrator.Hydrate(
                workflowState, typeof(WorkflowState), depth
            );
        }

        private Data.Entities.WorkflowState HydrateWorkflowStateEntity(WorkflowState workflowState, int depth = 3)
        {
            return (Data.Entities.WorkflowState)_Hydrator.Hydrate(
                workflowState, typeof(Data.Entities.WorkflowState), depth
            );
        }
    }
}
