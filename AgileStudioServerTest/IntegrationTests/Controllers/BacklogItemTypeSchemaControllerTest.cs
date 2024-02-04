
using AgileStudioServer;
using AgileStudioServer.Controllers;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class BacklogItemTypeSchemaControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeSchemaController _Controller;

        public BacklogItemTypeSchemaControllerTest(
            DBContext dbContext,
            Fixtures fixtures,
            BacklogItemTypeSchemaController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void List_ReturnsApiResources()
        {
            List<BacklogItemTypeSchema> backlogItemTypeSchemas = new() {
                _Fixtures.CreateBacklogItemTypeSchema("Test Backlog Item Type Schema 1"),
                _Fixtures.CreateBacklogItemTypeSchema("Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSchemaApiResource>? apiResources = null;
            IActionResult result = _Controller.List();
            if (result is OkObjectResult okResult){
                apiResources = okResult.Value as List<BacklogItemTypeSchemaApiResource>;
            }

            Assert.IsType<List<BacklogItemTypeSchemaApiResource>>(apiResources);
            Assert.Equal(backlogItemTypeSchemas.Count, apiResources.Count);
        }

        [Fact]
        public void ListBacklogItemTypes_WithId_ReturnsApiResources()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            List<BacklogItemType> backlogItemTypes = new() {
                _Fixtures.CreateBacklogItemType(backlogItemTypeSchema, "Test Backlog Item Type Schema 1"),
                _Fixtures.CreateBacklogItemType(backlogItemTypeSchema, "Test Backlog Item Type Schema 2")
            };

            List<BacklogItemTypeSubResource>? apiResources = null;
            IActionResult result = _Controller.ListBacklogItemTypes(backlogItemTypeSchema.ID);
            if (result is OkObjectResult okResult){
                apiResources = okResult.Value as List<BacklogItemTypeSubResource>;
            }

            Assert.IsType<List<BacklogItemTypeSubResource>>(apiResources);
            Assert.Equal(backlogItemTypes.Count, apiResources.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            BacklogItemTypeSchemaApiResource? apiResource = null;
            IActionResult result = _Controller.Get(backlogItemTypeSchema.ID);
            if (result is OkObjectResult okResult){
                apiResource = okResult.Value as BacklogItemTypeSchemaApiResource;
            }

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(backlogItemTypeSchema.ID, apiResource.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var dto = new BacklogItemTypeSchemaPostDto("Test Backlog Item Type Schema");

            BacklogItemTypeSchemaApiResource? apiResource = null;
            IActionResult result = _Controller.Post(dto);
            if (result is CreatedResult createdResult){
                apiResource = createdResult.Value as BacklogItemTypeSchemaApiResource;
            }

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            var title = $"{backlogItemTypeSchema.Title} Updated";
            var dto = new BacklogItemTypeSchemaPatchDto(title);

            IActionResult result = _Controller.Patch(backlogItemTypeSchema.ID, dto);

            BacklogItemTypeSchemaApiResource? apiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                apiResource = okObjectResult.Value as BacklogItemTypeSchemaApiResource;
            }

            Assert.IsType<BacklogItemTypeSchemaApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();

            IActionResult result = _Controller.Delete(backlogItemTypeSchema.ID);

            Assert.IsType<OkResult>(result as OkResult);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Delete(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }
    }
}
