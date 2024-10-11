using AgileStudioServer.API.DtosNew;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class BacklogItemTypeControllerTest : AbstractControllerTest
    {
        private readonly BacklogItemTypeController _Controller;

        public BacklogItemTypeControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            BacklogItemTypeController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();

            BacklogItemTypeDto? apiResource = null;
            IActionResult result = _Controller.Get(backlogItemType.ID);
            if (result is OkObjectResult okResult)
            {
                apiResource = okResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(apiResource);
            Assert.Equal(backlogItemType.ID, apiResource.ID);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var backlogItemTypeSchema = _Fixtures.CreateBacklogItemTypeSchema();
            var workflow = _Fixtures.CreateWorkflow();
            var dto = new BacklogItemTypePostDto("Test Backlog Item Type Schema",
                backlogItemTypeSchema.ID, workflow.ID);

            BacklogItemTypeDto? apiResource = null;
            IActionResult result = _Controller.Post(dto);
            if (result is CreatedResult createdResult)
            {
                apiResource = createdResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(apiResource);
            Assert.Equal(dto.Title, apiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var backlogItemType = _Fixtures.CreateBacklogItemType();
            var title = $"{backlogItemType.Title} Updated";
            var dto = new BacklogItemTypePatchDto(backlogItemType.ID, title);

            IActionResult result = _Controller.Patch(backlogItemType.ID, dto);
            BacklogItemTypeDto? apiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                apiResource = okObjectResult.Value as BacklogItemTypeDto;
            }

            Assert.IsType<BacklogItemTypeDto>(apiResource);
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
