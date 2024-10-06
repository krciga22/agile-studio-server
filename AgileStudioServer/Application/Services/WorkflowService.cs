using AgileStudioServer.Application.Models;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class WorkflowService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public WorkflowService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<Workflow> GetAll()
        {
            List<Data.Entities.Workflow> entities = _DBContext.Workflow.ToList();
            return HydrateWorkflowModels(entities);
        }

        public virtual Workflow? Get(int id)
        {
            Data.Entities.Workflow? entity = _DBContext.Workflow.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateWorkflowModel(entity);
        }

        public virtual Workflow Create(Workflow workflow)
        {
            Data.Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowModel(entity);
        }

        public virtual Workflow Update(Workflow workflow)
        {
            Data.Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowModel(entity);
        }

        public virtual void Delete(Workflow workflow)
        {
            Data.Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Workflow.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<Workflow> HydrateWorkflowModels(List<Data.Entities.Workflow> entities, int depth = 1)
        {
            List<Workflow> models = new();

            entities.ForEach(entity => {
                Workflow model = HydrateWorkflowModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private Workflow HydrateWorkflowModel(Data.Entities.Workflow workflow, int depth = 1)
        {
            return (Workflow)_Hydrator.Hydrate(
                workflow, typeof(Workflow), depth
            );
        }

        private Data.Entities.Workflow HydrateWorkflowEntity(Workflow workflow, int depth = 1)
        {
            return (Data.Entities.Workflow)_Hydrator.Hydrate(
                workflow, typeof(Data.Entities.Workflow), depth
            );
        }
    }
}
