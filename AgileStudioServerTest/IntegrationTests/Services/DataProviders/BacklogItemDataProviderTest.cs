using AgileStudioServer;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Services.DataProviders
{
    public class BacklogItemDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemDataProvider _dataProvider;

        public BacklogItemDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            BacklogItemDataProvider backlogItemDataProvider) : base(dbContext, fixtures)
        {
            _dataProvider = backlogItemDataProvider;
        }

        [Fact]
        public void CreateBacklogItem_WithPostDto_ReturnsApiResource()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType();
            var sprint = _Fixtures.CreateSprint(project: project);
            var release = _Fixtures.CreateRelease(project: project);
            var dto = new BacklogItemPostDto("Test Backlog Item", project.ID, backlogItemType.ID, sprint.ID, release.ID);

            var apiResource = _dataProvider.Create(dto);

            Assert.IsType<BacklogItemApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItem_ById_ReturnsApiResource()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            var apiResource = _dataProvider.Get(backlogItem.ID);

            Assert.IsType<BacklogItemApiResource>(apiResource);
        }

        [Fact]
        public void ListForProject_ById_ReturnsApiResource()
        {
            var project = _Fixtures.CreateProject();
            var backlogItemType = _Fixtures.CreateBacklogItemType(
                backlogItemTypeSchema: project.BacklogItemTypeSchema);

            List<BacklogItem> backlogItems = new() {
                _Fixtures.CreateBacklogItem(
                    title: "Test Backlog Item 1",
                    project: project,
                    backlogItemType: backlogItemType),
                _Fixtures.CreateBacklogItem(
                    title: "Test Backlog Item 2",
                    project: project,
                    backlogItemType: backlogItemType)
            };

            var apiResources = _dataProvider.ListForProjectId(project.ID);

            Assert.IsType<List<BacklogItemApiResource>>(apiResources);
            Assert.Equal(backlogItems.Count, apiResources.Count);
        }

        [Fact]
        public void UpdateBacklogItem_WithValidDto_ReturnsApiResource()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();
            var title = $"{backlogItem.Title} Updated";
            var dto = new BacklogItemPatchDto(backlogItem.ID, title);

            var apiResource = _dataProvider.Update(backlogItem.ID, dto);

            Assert.IsType<BacklogItemApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItem_WithValidId_ReturnsTrue()
        {
            var backlogItem = _Fixtures.CreateBacklogItem();

            _dataProvider.Delete(backlogItem.ID);

            var result = _dataProvider.Get(backlogItem.ID);
            Assert.Null(result);
        }
    }
}
