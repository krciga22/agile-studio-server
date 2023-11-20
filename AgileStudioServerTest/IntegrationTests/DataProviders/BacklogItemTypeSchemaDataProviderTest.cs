using AgileStudioServer;
using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
using AgileStudioServer.Models;

namespace AgileStudioServerTest.IntegrationTests.DataProviders
{
    public class BacklogItemTypeSchemaDataProviderTest : AbstractDataProviderTest
    {
        private readonly BacklogItemTypeSchemaDataProvider _BacklogItemTypeSchemaDataProvider;

        public BacklogItemTypeSchemaDataProviderTest(DBContext dbContext, BacklogItemTypeSchemaDataProvider backlogItemTypeSchemaDataProvider) : base(dbContext)
        {
            _BacklogItemTypeSchemaDataProvider = backlogItemTypeSchemaDataProvider;
        }

        [Fact]
        public void CreateBacklogItemTypeSchema_WithPostDto_ReturnsApiResource()
        {
            var project = CreateProject();

            var dto = new BacklogItemTypeSchemaPostDto() {
                Title = "Test Schema",
                ProjectId = project.ID
            };

            var apiResource = _BacklogItemTypeSchemaDataProvider.CreateBacklogItemTypeSchema(dto);

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
        }

        private Project CreateProject(string title = "test Project")
        {
            var project = new Project(title);
            _DBContext.Projects.Add(project);
            _DBContext.SaveChanges();
            return project;
        }
    }
}
