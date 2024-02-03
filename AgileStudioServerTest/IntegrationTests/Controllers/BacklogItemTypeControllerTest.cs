
using AgileStudioServer;
using AgileStudioServer.Controllers;
using AgileStudioServer.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class BacklogItemTypeControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeController _Controller;

        public BacklogItemTypeControllerTest(DBContext dbContext, BacklogItemTypeController controller) : base(dbContext)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var backlogItemType = CreateBacklogItemType();

            BacklogItemTypeApiResource? apiResource = null;
            IActionResult result = _Controller.Get(backlogItemType.ID);
            if (result is OkObjectResult okResult){
                apiResource = okResult.Value as BacklogItemTypeApiResource;
            }

            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
            Assert.Equal(backlogItemType.ID, apiResource.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            var dto = new BacklogItemTypePostDto("Test Backlog Item Type Schema", backlogItemTypeSchema.ID);

            BacklogItemTypeApiResource? apiResource = null;
            IActionResult result = _Controller.Post(dto);
            if (result is CreatedResult createdResult){
                apiResource = createdResult.Value as BacklogItemTypeApiResource;
            }

            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var backlogItemType = CreateBacklogItemType();

            var title = $"{backlogItemType.Title} Updated";
            var dto = new BacklogItemTypePatchDto(title);

            IActionResult result = _Controller.Patch(backlogItemType.ID, dto);

            BacklogItemTypeApiResource? apiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                apiResource = okObjectResult.Value as BacklogItemTypeApiResource;
            }

            Assert.IsType<BacklogItemTypeApiResource>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var backlogItemType = CreateBacklogItemType();

            IActionResult result = _Controller.Delete(backlogItemType.ID);

            Assert.IsType<OkResult>(result as OkResult);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Delete(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        private BacklogItemType CreateBacklogItemType(string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            var backlogItemType = new BacklogItemType(title);
            backlogItemType.BacklogItemTypeSchema = backlogItemTypeSchema;
            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }

        private BacklogItemTypeSchema CreateBacklogItemTypeSchema(string title = "Test Backlog Item Type Schema")
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema(title);
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }
    }
}
