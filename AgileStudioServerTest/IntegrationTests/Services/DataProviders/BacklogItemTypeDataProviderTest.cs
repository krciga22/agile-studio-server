using AgileStudioServer;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Services.DataProviders
{
    public class BacklogItemTypeDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemTypeDataProvider _dataProvider;

        public BacklogItemTypeDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            BacklogItemTypeDataProvider backlogItemTypeDataProvider) : base(dbContext, fixtures)
        {
            _dataProvider = backlogItemTypeDataProvider;
        }

        [Fact]
        public void CreateBacklogItemType_WithPostDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var dto = new BacklogItemTypePostDto("Test Schema", backlogItemTypeSchema.ID);
            var apiResource = _dataProvider.Create(dto);
            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItemType_ById_ReturnsApiResource()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            var apiResource = _dataProvider.Get(backlogItemType.ID);

            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
        }

        [Fact]
        public void ListByBacklogItemTypeSchemaId_ReturnsApiResources()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            List<BacklogItemType> backlogItemTypes = new() {
                _Fixtures.CreateBacklogItemType(
                    title: "Test Backlog Item Type Schema 1",
                    backlogItemTypeSchema: backlogItemTypeSchema),
                _Fixtures.CreateBacklogItemType(
                    title: "Test Backlog Item Type Schema 2",
                    backlogItemTypeSchema: backlogItemTypeSchema)
            };

            List<BacklogItemTypeSubResource>? apiResources = _dataProvider.ListByBacklogItemTypeSchemaId(backlogItemTypeSchema.ID);

            Assert.IsType<List<BacklogItemTypeSubResource>>(apiResources);
            Assert.Equal(backlogItemTypes.Count, apiResources.Count);
        }

        [Fact]
        public void UpdateBacklogItemType_WithValidDto_ReturnsApiResource()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            var title = $"{backlogItemType.Title} Updated";
            var dto = new BacklogItemTypePatchDto(title);

            var apiResource = _dataProvider.Update(backlogItemType.ID, dto);
            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItemType_WithValidId_ReturnsTrue()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            bool result = _dataProvider.Delete(backlogItemType.ID);
            Assert.True(result);
        }
    }
}
