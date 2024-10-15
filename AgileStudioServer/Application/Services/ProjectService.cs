using AgileStudioServer.Application.Models;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Services
{
    public class ProjectService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public ProjectService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<Project> GetAll()
        {
            List<Data.Entities.Project> entities = _DBContext.Project.ToList();

            return HydrateProjectModels(entities);
        }

        public virtual List<Project> GetByCreatedByUserId(int userId)
        {
            List<Data.Entities.Project> entities = _DBContext.Project.Where(project =>
                project.CreatedBy != null && project.CreatedBy.ID == userId).ToList();

            return HydrateProjectModels(entities);
        }

        public virtual Project? Get(int id)
        {
            Data.Entities.Project? entity = _DBContext.Project.Find(id);
            if (entity is null) {
                return null;
            }

            return HydrateProjectModel(entity);
        }

        public virtual Project Create(Project project)
        {
            Data.Entities.Project entity = HydrateProjectEntity(project);

            _DBContext.Project.Add(entity);
            _DBContext.SaveChanges();

            return HydrateProjectModel(entity);
        }

        public virtual Project Update(Project project)
        {
            Data.Entities.Project entity = HydrateProjectEntity(project);

            _DBContext.Project.Update(entity);
            _DBContext.SaveChanges();

            return HydrateProjectModel(entity);
        }

        public virtual void Delete(Project project)
        {
            Data.Entities.Project entity = HydrateProjectEntity(project);

            _DBContext.Project.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<Project> HydrateProjectModels(List<Data.Entities.Project> entities, int depth = 3)
        {
            List<Project> models = new();

            entities.ForEach(entity => {
                Project model = HydrateProjectModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private Project HydrateProjectModel(Data.Entities.Project project, int depth = 3)
        {
            return (Project)_Hydrator.Hydrate(
                project, typeof(Project), depth
            );
        }

        private Data.Entities.Project HydrateProjectEntity(Project project, int depth = 3)
        {
            return (Data.Entities.Project)_Hydrator.Hydrate(
                project, typeof(Data.Entities.Project), depth
            );
        }
    }
}
