using AgileStudioServer.API.Dtos;
using AgileStudioServer.API.ApiResources;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders
{
    public class ProjectRepositoryTest : AbstractDataProviderTest
    {
        private readonly ProjectDataProvider _DataProvider;

        public ProjectRepositoryTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            ProjectDataProvider projectDataProvider) : base(dbContext, fixtures)
        {
            _DataProvider = projectDataProvider;
        }

        [Fact]
        public void CreateProject_WithProjectPostDto_ReturnsProjectApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var projectPostDto = new ProjectPostDto("Test Project", backlogItemTypeSchema.ID);

            var projectApiResource = _DataProvider.Create(projectPostDto);

            Assert.IsType<ProjectApiResource>(projectApiResource);
        }

        [Fact]
        public void GetProject_ById_ReturnsProjectApiResource()
        {
            var project = _Fixtures.CreateProject();

            var projectApiResource = _DataProvider.Get(project.ID);

            Assert.IsType<ProjectApiResource>(projectApiResource);
        }

        [Fact]
        public void GetProjects_WithNoArguments_ReturnsProjectApiResources()
        {
            var projects = new List<Project>
            {
                _Fixtures.CreateProject("Test Project 1"),
                _Fixtures.CreateProject("Test Project 2")
            };

            List<ProjectApiResource> projectApiResources = _DataProvider.List();

            Assert.Equal(projects.Count, projectApiResources.Count);
        }

        [Fact]
        public void UpdateProject_WithValidProjectPatchDto_ReturnsProjectApiResource()
        {
            var project = _Fixtures.CreateProject();
            var title = $"{project.Title} Updated";
            var projectPatchDto = new ProjectPatchDto(title);

            var projectApiResource = _DataProvider.Update(project.ID, projectPatchDto);

            Assert.IsType<ProjectApiResource>(projectApiResource);
            Assert.Equal(projectPatchDto.Title, projectApiResource.Title);
        }

        [Fact]
        public void DeleteProject_WithValidId_ReturnsTrue()
        {
            var project = _Fixtures.CreateProject();

            _DataProvider.Delete(project.ID);

            var result = _DataProvider.Get(project.ID);
            Assert.Null(result);
        }
    }
}
