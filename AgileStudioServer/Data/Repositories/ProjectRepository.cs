using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;

namespace AgileStudioServer.Data.Repositories
{
    public class ProjectRepository : AbstractDBRepository
    {
        public ProjectRepository(DBContext context) : base(context)
        {

        }

        public Project? Get(int id)
        {
            return _context.Project.Find(id);
        }

        public List<Project> GetAll()
        {
            return _context.Project.ToList();
        }

        public Project Create(Project project, bool saveChanges=true)
        {
            _context.Project.Add(project);
            if (saveChanges){
                _context.SaveChanges();
            }
            return project;
        }

        public Project Update(Project project, bool saveChanges = true)
        {
            var existingProject = Get(project.ID);
            if (existingProject is null) {
                throw new EntityNotFoundException(nameof(Project), project.ID.ToString());
            }

            _context.Project.Update(project);
            if (saveChanges){
                _context.SaveChanges();
            }
            return project;
        }

        public void Delete(Project project, bool saveChanges = true)
        {
            var existingProject = Get(project.ID);
            if (existingProject is null) {
                throw new EntityNotFoundException(nameof(Project), project.ID.ToString());
            }

            _context.Project.Remove(project);
            if (saveChanges){
                _context.SaveChanges();
            }
        }
    }
}
