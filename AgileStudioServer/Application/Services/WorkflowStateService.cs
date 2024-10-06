using AgileStudioServer.Application.Models;
using EntityHydrators = AgileStudioServer.Data.Entities.Hydrators;
using AgileStudioServer.Data;
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.Application.Services
{
    public class WorkflowStateService
    {
        private DBContext _DBContext;
        private Hydrator _Hydrator;
        private EntityHydrators.WorkflowStateHydrator _EntityHydrator;

        public WorkflowStateService(
            DBContext dbContext,
            Hydrator hydrator,
            EntityHydrators.WorkflowStateHydrator entityHydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
            _EntityHydrator = entityHydrator;
        }

        public virtual List<WorkflowState> GetByWorkflowId(int workflowId)
        {
            List<Data.Entities.WorkflowState> entities = _DBContext.WorkflowState.
                Where(x => x.Workflow.ID == workflowId).ToList();

            List<WorkflowState> models = new();
            entities.ForEach(entity => {
                WorkflowState model = (WorkflowState)_Hydrator.Hydrate(
                    entity, typeof(WorkflowState), 1
                );
                models.Add(model);
            });

            return models;
        }

        public virtual WorkflowState? Get(int id)
        {
            Data.Entities.WorkflowState? entity = _DBContext.WorkflowState.Find(id);
            if (entity is null) {
                return null;
            }

            WorkflowState model = (WorkflowState)_Hydrator.Hydrate(
                entity, typeof(WorkflowState), 1
            );

            return model;
        }

        public virtual WorkflowState Create(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = _EntityHydrator.Hydrate(workflowState);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            WorkflowState model = (WorkflowState)_Hydrator.Hydrate(
                entity, typeof(WorkflowState), 1
            );

            return model;
        }

        public virtual WorkflowState Update(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = _EntityHydrator.Hydrate(workflowState);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            WorkflowState model = (WorkflowState)_Hydrator.Hydrate(
                entity, typeof(WorkflowState), 1
            );

            return model;
        }

        public virtual void Delete(WorkflowState workflowState)
        {
            Data.Entities.WorkflowState entity = _EntityHydrator.Hydrate(workflowState);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
