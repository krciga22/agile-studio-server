using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class ProjectServiceTest : AbstractServiceTest
    {
        private readonly ProjectService _projectService;

        public ProjectServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            ProjectService projectService) : base(dbContext, fixtures)
        {
            _projectService = projectService;
        }

        [Fact]
        public void Create_ReturnsProject()
        {
            Project project = new("Test Project");
            project.BacklogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            project = _projectService.Create(project);

            Assert.NotNull(project);
            Assert.True(project.ID > 0);
        }

        [Fact]
        public void Get_ReturnsProject()
        {
            var project = _Fixtures.CreateProject();

            var returnedProject = _projectService.Get(project.ID);

            Assert.NotNull(returnedProject);
            Assert.Equal(project.ID, returnedProject.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllProjects()
        {
            var projects = new List<Project>
            {
                _Fixtures.CreateProject("Test Project 1"),
                _Fixtures.CreateProject("Test Project 2")
            };

            List<Project> returnedProjects = _projectService.GetAll();

            Assert.Equal(projects.Count, returnedProjects.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedProject()
        {
            var project = _Fixtures.CreateProject();
            var title = $"{project.Title} Updated";

            project.Title = title;
            project = _projectService.Update(project);

            Assert.NotNull(project);
            Assert.Equal(title, project.Title);
        }

        [Fact]
        public void Delete_DeletesProject()
        {
            var project = _Fixtures.CreateProject();

            _projectService.Delete(project);

            project = _projectService.Get(project.ID);
            Assert.Null(project);
        }
    }
}
