using AgileStudioServer.Exceptions;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Services.DataProviders
{
    public class ProjectDataProvider
    {
        private readonly DBContext _DBContext;

        public ProjectDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual List<ProjectApiResource> List()
        {
            List<Project> projects = _DBContext.Project.ToList();

            List<ProjectApiResource> projectApiResources = new();
            projects.ForEach(project =>
            {
                LoadReferences(project);

                projectApiResources.Add(
                    new ProjectApiResource(project)
                );
            });

            return projectApiResources;
        }

        public virtual ProjectApiResource? Get(int id)
        {
            Project? project = _DBContext.Project.Find(id);
            if (project is null)
            {
                return null;
            }

            LoadReferences(project);

            return new ProjectApiResource(project);
        }

        public virtual ProjectApiResource Create(ProjectPostDto projectPostDto)
        {
            var backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(projectPostDto.BacklogItemTypeSchemaId) ??
                throw new EntityNotFoundException(nameof(BacklogItemTypeSchema), projectPostDto.BacklogItemTypeSchemaId.ToString());

            var project = new Project(projectPostDto.Title)
            {
                Description = projectPostDto.Description,
                BacklogItemTypeSchema = backlogItemTypeSchema
            };

            _DBContext.Add(project);
            _DBContext.SaveChanges();

            return new ProjectApiResource(project);
        }

        public virtual ProjectApiResource Update(int id, ProjectPatchDto projectPatchDto)
        {
            var project = _DBContext.Project.Find(id) ??
                throw new EntityNotFoundException(nameof(Project), id.ToString());

            project.Title = projectPatchDto.Title;
            project.Description = projectPatchDto.Description;
            _DBContext.SaveChanges();

            LoadReferences(project);

            return new ProjectApiResource(project);
        }

        public virtual void Delete(int id)
        {
            var project = _DBContext.Project.Find(id) ??
                throw new EntityNotFoundException(nameof(Project), id.ToString());

            _DBContext.Project.Remove(project);
            _DBContext.SaveChanges();
        }

        private void LoadReferences(Project project)
        {
            _DBContext.Entry(project).Reference("BacklogItemTypeSchema").Load();
            _DBContext.Entry(project).Reference("CreatedBy").Load();
        }
    }
}
