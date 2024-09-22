using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class WorkflowService
    {
        private readonly DBContext _DBContext;

        private readonly WorkflowConverter _converter;

        public WorkflowService(DBContext dbContext, WorkflowConverter workflowConverter)
        {
            _DBContext = dbContext;
            _converter = workflowConverter;
        }

        public virtual List<Workflow> GetAll()
        {
            List<Data.Entities.Workflow> entities = _DBContext.Workflow.ToList();

            List<Workflow> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual Workflow? Get(int id)
        {
            Data.Entities.Workflow? entity = _DBContext.Workflow.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual Workflow Create(Workflow workflow)
        {
            Data.Entities.Workflow entity = _converter.ConvertToEntity(workflow);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual Workflow Update(Workflow workflow)
        {
            Data.Entities.Workflow entity = _converter.ConvertToEntity(workflow);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(Workflow workflow)
        {
            Data.Entities.Workflow entity = _converter.ConvertToEntity(workflow);

            _DBContext.Workflow.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
