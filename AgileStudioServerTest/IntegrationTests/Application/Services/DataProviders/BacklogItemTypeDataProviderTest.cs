using AgileStudioServer.API.Dtos;
using AgileStudioServer.API.ApiResources;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders
{
    public class BacklogItemTypeDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemTypeDataProvider _DataProvider;

        public BacklogItemTypeDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            BacklogItemTypeDataProvider backlogItemTypeDataProvider) : base(dbContext, fixtures)
        {
            _DataProvider = backlogItemTypeDataProvider;
        }

        [Fact]
        public void CreateBacklogItemType_WithPostDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var workflow = _Fixtures.CreateWorkflow();
            var dto = new BacklogItemTypePostDto("Test Schema", backlogItemTypeSchema.ID, workflow.ID);

            var apiResource = _DataProvider.Create(dto);

            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItemType_ById_ReturnsApiResource()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            var apiResource = _DataProvider.Get(backlogItemType.ID);

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

            List<BacklogItemTypeSubResource>? apiResources = _DataProvider.ListByBacklogItemTypeSchemaId(backlogItemTypeSchema.ID);

            Assert.IsType<List<BacklogItemTypeSubResource>>(apiResources);
            Assert.Equal(backlogItemTypes.Count, apiResources.Count);
        }

        [Fact]
        public void UpdateBacklogItemType_WithValidDto_ReturnsApiResource()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();
            var title = $"{backlogItemType.Title} Updated";
            var dto = new BacklogItemTypePatchDto(title);

            var apiResource = _DataProvider.Update(backlogItemType.ID, dto);

            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItemType_WithValidId_ReturnsTrue()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            _DataProvider.Delete(backlogItemType.ID);

            var result = _DataProvider.Get(backlogItemType.ID);
            Assert.Null(result);
        }
    }
}
