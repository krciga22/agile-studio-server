using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class SprintService
    {
        private readonly DBContext _DBContext;

        private readonly SprintConverter _converter;

        public SprintService(DBContext dbContext, SprintConverter sprintConverter)
        {
            _DBContext = dbContext;
            _converter = sprintConverter;
        }

        public virtual List<Sprint> GetByProjectId(int projectId)
        {
            List<Data.Entities.Sprint> entities = _DBContext.Sprint.Where(sprint => 
                sprint.Project.ID == projectId).ToList();

            List<Sprint> models = new();
            entities.ForEach(entity =>
            {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual Sprint? Get(int id)
        {
            Data.Entities.Sprint? entity = _DBContext.Sprint.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual Sprint Create(Sprint sprint)
        {
            Data.Entities.Sprint entity = _converter.ConvertToEntity(sprint);

            entity.SprintNumber = GetNextSprintNumber();

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual Sprint Update(Sprint sprint)
        {
            Data.Entities.Sprint entity = _converter.ConvertToEntity(sprint);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(Sprint sprint)
        {
            Data.Entities.Sprint entity = _converter.ConvertToEntity(sprint);

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
    }
}
