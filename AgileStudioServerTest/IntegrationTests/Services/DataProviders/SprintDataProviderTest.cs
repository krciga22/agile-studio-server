using AgileStudioServer;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Services.DataProviders
{
    public class SprintDataProviderTest : AbstractDataProviderTest
    {
        private readonly SprintDataProvider _SprintDataProvider;

        public SprintDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            SprintDataProvider sprintDataProvider) : base(dbContext, fixtures)
        {
            _SprintDataProvider = sprintDataProvider;
        }

        [Fact]
        public void CreateSprint_WithSprintPostDto_ReturnsSprintApiResource()
        {
            var project = _Fixtures.CreateProject();
            var sprintPostDto = new SprintPostDto(project.ID);

            var sprintApiResource = _SprintDataProvider.Create(sprintPostDto);

            Assert.IsType<SprintApiResource>(sprintApiResource);
        }

        [Fact]
        public void GetSprint_ById_ReturnsSprintApiResource()
        {
            var sprint = _Fixtures.CreateSprint();

            var sprintApiResource = _SprintDataProvider.Get(sprint.ID);

            Assert.IsType<SprintApiResource>(sprintApiResource);
        }

        [Fact]
        public void GetSprints_ByProjectId_ReturnsSprintApiResources()
        {
            var project = _Fixtures.CreateProject();

            var sprints = new List<Sprint>
            {
                _Fixtures.CreateSprint(1, project),
                _Fixtures.CreateSprint(2, project)
            };

            List<SprintApiResource> sprintApiResources = _SprintDataProvider.ListForProject(project.ID);

            Assert.Equal(sprints.Count, sprintApiResources.Count);
        }

        [Fact]
        public void UpdateSprint_WithValidSprintPatchDto_ReturnsSprintApiResource()
        {
            var sprint = _Fixtures.CreateSprint();
            var sprintPatchDto = new SprintPatchDto
            {
                Description = $"Sprint {sprint.SprintNumber} Description Updated"
            };

            var sprintApiResource = _SprintDataProvider.Update(sprint.ID, sprintPatchDto);
            Assert.IsType<SprintApiResource>(sprintApiResource);
            Assert.Equal(sprintPatchDto.Description, sprintApiResource.Description);
        }

        [Fact]
        public void DeleteSprint_WithValidId_ReturnsTrue()
        {
            var sprint = _Fixtures.CreateSprint();

            bool result = _SprintDataProvider.Delete(sprint.ID);
            Assert.True(result);
        }
    }
}
