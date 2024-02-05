
using AgileStudioServer;
using AgileStudioServer.Controllers;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServerTest.IntegrationTests.Controllers
{
    public class BacklogItemTypeControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeController _Controller;

        public BacklogItemTypeControllerTest(
            DBContext dbContext,
            Fixtures fixtures, 
            BacklogItemTypeController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

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
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
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
            var backlogItemType = _Fixtures.CreateBacklogItemType();
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
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            IActionResult result = _Controller.Delete(backlogItemType.ID);

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
