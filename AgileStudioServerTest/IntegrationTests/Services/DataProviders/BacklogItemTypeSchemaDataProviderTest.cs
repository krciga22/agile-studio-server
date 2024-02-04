using AgileStudioServer;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Services.DataProviders
{
    public class BacklogItemTypeSchemaDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemTypeSchemaDataProvider _dataProvider;

        public BacklogItemTypeSchemaDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            BacklogItemTypeSchemaDataProvider backlogItemTypeSchemaDataProvider) : base(dbContext, fixtures)
        {
            _dataProvider = backlogItemTypeSchemaDataProvider;
        }

        [Fact]
        public void CreateBacklogItemTypeSchema_WithPostDto_ReturnsApiResource()
        {
            var dto = new BacklogItemTypeSchemaPostDto("Test Schema");

            var apiResource = _dataProvider.Create(dto);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        [Fact]
        public void GetBacklogItemTypeSchemas_WithProjectId_ReturnsApiResources()
        {
            var backlogItemTypeSchemas = new List<BacklogItemTypeSchema>
            {
                _Fixtures.CreateBacklogItemTypeSchema("Test Backlog Item Type Schema 1"),
                _Fixtures.CreateBacklogItemTypeSchema("Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSchemaApiResource> apiResources = _dataProvider.List();

            Assert.Equal(backlogItemTypeSchemas.Count, apiResources.Count);
        }

        [Fact]
        public void GetBacklogItemTypeSchema_ById_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            var apiResource = _dataProvider.Get(backlogItemTypeSchema.ID);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        [Fact]
        public void UpdateBacklogItemTypeSchema_WithValidDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            var title = $"{backlogItemTypeSchema.Title} Updated";
            var dto = new BacklogItemTypeSchemaPatchDto(title);

            var apiResource = _dataProvider.Update(backlogItemTypeSchema.ID, dto);
            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItemTypeSchema_WithValidId_ReturnsTrue()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            bool result = _dataProvider.Delete(backlogItemTypeSchema.ID);
            Assert.True(result);
        }
    }
}
