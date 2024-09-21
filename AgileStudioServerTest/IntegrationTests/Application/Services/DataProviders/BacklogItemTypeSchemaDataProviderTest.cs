using AgileStudioServer.API.Dtos;
using AgileStudioServer.API.ApiResources;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders
{
    public class BacklogItemTypeSchemaDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemTypeSchemaDataProvider _DataProvider;

        public BacklogItemTypeSchemaDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            BacklogItemTypeSchemaDataProvider backlogItemTypeSchemaDataProvider) : base(dbContext, fixtures)
        {
            _DataProvider = backlogItemTypeSchemaDataProvider;
        }

        [Fact]
        public void CreateBacklogItemTypeSchema_WithPostDto_ReturnsApiResource()
        {
            var dto = new BacklogItemTypeSchemaPostDto("Test Schema");

            var apiResource = _DataProvider.Create(dto);

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

            List<BacklogItemTypeSchemaApiResource> apiResources = _DataProvider.List();

            Assert.Equal(backlogItemTypeSchemas.Count, apiResources.Count);
        }

        [Fact]
        public void GetBacklogItemTypeSchema_ById_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            var apiResource = _DataProvider.Get(backlogItemTypeSchema.ID);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        [Fact]
        public void UpdateBacklogItemTypeSchema_WithValidDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var title = $"{backlogItemTypeSchema.Title} Updated";
            var dto = new BacklogItemTypeSchemaPatchDto(title);

            var apiResource = _DataProvider.Update(backlogItemTypeSchema.ID, dto);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteBacklogItemTypeSchema_WithValidId_ReturnsTrue()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            _DataProvider.Delete(backlogItemTypeSchema.ID);

            var result = _DataProvider.Get(backlogItemTypeSchema.ID);
            Assert.Null(result);
        }
    }
}
