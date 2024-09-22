using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
using AgileStudioServer.Data.Repositories;
using AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Data.Repositories
{
    public class ProjectRepositoryTest : AbstractDataProviderTest
    {
        private readonly ProjectRepository _repository;

        public ProjectRepositoryTest(
            DBContext dbContext,
            Fixtures fixtures,
            ProjectRepository projectRepository) : base(dbContext, fixtures)
        {
            _repository = projectRepository;
        }

        [Fact]
        public void Create_ReturnsProject()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var project = new Project("Test Project");
            project.BacklogItemTypeSchema = backlogItemTypeSchema;

            var updatedProject = _repository.Create(project);

            Assert.IsType<Project>(updatedProject);
        }

        [Fact]
        public void Get_WithValidProjectId_ReturnsProject()
        {
            var project = _Fixtures.CreateProject();

            var returnedProject = _repository.Get(project.ID);

            Assert.IsType<Project>(returnedProject);
        }

        [Fact]
        public void Get_WithInvalidProjectId_ReturnsProject()
        {
            var invalidProjectId = 100;

            var returnedProject = _repository.Get(invalidProjectId);

            Assert.Null(returnedProject);
        }

        [Fact]
        public void GetAll_ReturnsProjects()
        {
            var projects = new List<Project>
            {
                _Fixtures.CreateProject("Test Project 1"),
                _Fixtures.CreateProject("Test Project 2")
            };

            List<Project> returnedProjects = _repository.GetAll();

            Assert.Equal(projects.Count, returnedProjects.Count);
        }

        [Fact]
        public void Update_WithExistingProject_ReturnsUpdatedProject()
        {
            var project = _Fixtures.CreateProject();
            var newTitle = $"{project.Title} Updated";
            project.Title = newTitle;

            var returnedProject = _repository.Update(project);

            Assert.IsType<Project>(returnedProject);
            Assert.Equal(newTitle, returnedProject.Title);
        }

        [Fact]
        public void Update_WithNonExistingProject_ThrowsException()
        {
            var project = new Project("test");

            Assert.Throws<EntityNotFoundException>(() => _repository.Update(project));
        }

        [Fact]
        public void Delete_WithExistingProject_MakesGetReturnNull()
        {
            var project = _Fixtures.CreateProject();

            _repository.Delete(project);

            var result = _repository.Get(project.ID);
            Assert.Null(result);
        }

        [Fact]
        public void Delete_WithNonExistingProject_ThrowsException()
        {
            var project = new Project("test");

            Assert.Throws<EntityNotFoundException>(() => _repository.Delete(project));
        }
    }
}
