using AgileStudioServer.Application.Models;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class SprintService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public SprintService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<Sprint> GetByProjectId(int projectId)
        {
            List<Data.Entities.Sprint> entities = _DBContext.Sprint.Where(sprint => 
                sprint.Project.ID == projectId).ToList();

            return HydrateSprintModels(entities);
        }

        public virtual Sprint? Get(int id)
        {
            Data.Entities.Sprint? entity = _DBContext.Sprint.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateSprintModel(entity);
        }

        public virtual Sprint Create(Sprint sprint)
        {
            Data.Entities.Sprint entity = HydrateSprintEntity(sprint);

            entity.SprintNumber = GetNextSprintNumber();

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateSprintModel(entity);
        }

        public virtual Sprint Update(Sprint sprint)
        {
            Data.Entities.Sprint entity = HydrateSprintEntity(sprint);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateSprintModel(entity);
        }

        public virtual void Delete(Sprint sprint)
        {
            Data.Entities.Sprint entity = HydrateSprintEntity(sprint);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        public int GetNextSprintNumber()
        {
            return GetLastSprintNumber() + 1;
        }

        private int GetLastSprintNumber()
        {
            var lastSprint = _DBContext.Sprint
                .OrderByDescending(sprint => sprint.SprintNumber)
                .FirstOrDefault();

            return lastSprint?.SprintNumber ?? 0;
        }

        private List<Sprint> HydrateSprintModels(List<Data.Entities.Sprint> entities, int depth = 3)
        {
            List<Sprint> models = new();

            entities.ForEach(entity => {
                Sprint model = HydrateSprintModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private Sprint HydrateSprintModel(Data.Entities.Sprint sprint, int depth = 3)
        {
            return (Sprint)_Hydrator.Hydrate(
                sprint, typeof(Sprint), depth
            );
        }

        private Data.Entities.Sprint HydrateSprintEntity(Sprint sprint, int depth = 3)
        {
            return (Data.Entities.Sprint)_Hydrator.Hydrate(
                sprint, typeof(Data.Entities.Sprint), depth
            );
        }
    }
}
