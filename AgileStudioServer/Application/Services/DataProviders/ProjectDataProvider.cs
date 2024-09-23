using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Services.DataProviders
{
    public class ProjectDataProvider
    {
        private readonly DBContext _DBContext;

        public ProjectDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual List<ProjectDto> List()
        {
            List<Project> projects = _DBContext.Project.ToList();

            List<ProjectDto> projectApiResources = new();
            projects.ForEach(project =>
            {
                LoadReferences(project);

                projectApiResources.Add(
                    new ProjectDto(project)
                );
            });

            return projectApiResources;
        }

        public virtual ProjectDto? Get(int id)
        {
            Project? project = _DBContext.Project.Find(id);
            if (project is null)
            {
                return null;
            }

            LoadReferences(project);

            return new ProjectDto(project);
        }

        public virtual ProjectDto Create(ProjectPostDto projectPostDto)
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

            return new ProjectDto(project);
        }

        public virtual ProjectDto Update(int id, ProjectPatchDto projectPatchDto)
        {
            var project = _DBContext.Project.Find(id) ??
                throw new EntityNotFoundException(nameof(Project), id.ToString());

            project.Title = projectPatchDto.Title;
            project.Description = projectPatchDto.Description;
            _DBContext.SaveChanges();

            LoadReferences(project);

            return new ProjectDto(project);
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
