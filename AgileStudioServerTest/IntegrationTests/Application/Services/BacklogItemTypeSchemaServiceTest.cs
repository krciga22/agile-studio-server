using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class BacklogItemTypeSchemaServiceTest : AbstractServiceTest
    {
        private readonly BacklogItemTypeSchemaService _backlogItemTypeSchemaService;

        public BacklogItemTypeSchemaServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService) : base(dbContext, fixtures)
        {
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
        }

        [Fact]
        public void Create_ReturnsBacklogItemTypeSchema()
        {
            BacklogItemTypeSchema backlogItemTypeSchema = new("Test BacklogItemTypeSchema");

            backlogItemTypeSchema = _backlogItemTypeSchemaService.Create(backlogItemTypeSchema);

            Assert.NotNull(backlogItemTypeSchema);
            Assert.True(backlogItemTypeSchema.ID > 0);
        }

        [Fact]
        public void Get_ReturnsBacklogItemTypeSchema()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            var returnedBacklogItemTypeSchema = _backlogItemTypeSchemaService.Get(backlogItemTypeSchema.ID);

            Assert.NotNull(returnedBacklogItemTypeSchema);
            Assert.Equal(backlogItemTypeSchema.ID, returnedBacklogItemTypeSchema.ID);
        }

        [Fact]
        public void GetAll_ReturnsAllBacklogItemTypeSchemas()
        {
            var backlogItemTypeSchemas = new List<BacklogItemTypeSchema>
            {
                _Fixtures.CreateBacklogItemTypeSchema("Test BacklogItemTypeSchema 1"),
                _Fixtures.CreateBacklogItemTypeSchema("Test BacklogItemTypeSchema 2")
            };

            List<BacklogItemTypeSchema> returnedBacklogItemTypeSchemas = _backlogItemTypeSchemaService
                .GetAll();

            Assert.Equal(backlogItemTypeSchemas.Count, returnedBacklogItemTypeSchemas.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedBacklogItemTypeSchema()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var title = $"{backlogItemTypeSchema.Title} Updated";

            backlogItemTypeSchema.Title = title;
            backlogItemTypeSchema = _backlogItemTypeSchemaService.Update(backlogItemTypeSchema);

            Assert.NotNull(backlogItemTypeSchema);
            Assert.Equal(title, backlogItemTypeSchema.Title);
        }

        [Fact]
        public void Delete_DeletesBacklogItemTypeSchema()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            _backlogItemTypeSchemaService.Delete(backlogItemTypeSchema);

            backlogItemTypeSchema = _backlogItemTypeSchemaService.Get(backlogItemTypeSchema.ID);
            Assert.Null(backlogItemTypeSchema);
        }
    }
}
