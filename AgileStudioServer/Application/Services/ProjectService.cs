using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Models.ModelEntityConverters;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class ProjectService
    {
        private readonly DBContext _DBContext;

        private readonly ProjectConverter _converter;

        public ProjectService(DBContext dbContext, ProjectConverter projectConverter)
        {
            _DBContext = dbContext;
            _converter = projectConverter;
        }

        public virtual List<Project> GetAll()
        {
            List<Data.Entities.Project> entities = _DBContext.Project.ToList();

            List<Project> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual List<Project> GetByCreatedByUserId(int userId)
        {
            List<Data.Entities.Project> entities = _DBContext.Project.Where(project =>
                project.CreatedBy != null && project.CreatedBy.ID == userId).ToList();

            List<Project> models = new();
            entities.ForEach(entity => {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual List<Project> GetByUserId(int userId)
        {
            List<Data.Entities.Project> entities = _DBContext.Project.Where(project =>
                project.CreatedBy != null && project.CreatedBy.ID == userId).ToList();

            List<Project> models = new();
            entities.ForEach(entity =>
            {
                models.Add(
                    _converter.ConvertToModel(entity)
                );
            });

            return models;
        }

        public virtual Project? Get(int id)
        {
            Data.Entities.Project? entity = _DBContext.Project.Find(id);
            if (entity is null) {
                return null;
            }

            return _converter.ConvertToModel(entity);
        }

        public virtual Project Create(Project project)
        {
            Data.Entities.Project entity = _converter.ConvertToEntity(project);

            _DBContext.Add(project);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual Project Update(Project project)
        {
            Data.Entities.Project entity = _converter.ConvertToEntity(project);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return _converter.ConvertToModel(entity);
        }

        public virtual void Delete(Project project)
        {
            Data.Entities.Project entity = _converter.ConvertToEntity(project);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }
    }
}
