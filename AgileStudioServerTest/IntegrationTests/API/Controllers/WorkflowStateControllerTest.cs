using AgileStudioServer.API.DtosNew;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class WorkflowStateControllerTest : AbstractControllerTest
    {
        private readonly WorkflowStateController _Controller;

        public WorkflowStateControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            WorkflowStateController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var workflowState = _Fixtures.CreateWorkflowState();

            WorkflowStateDto? apiResource = null;
            IActionResult result = _Controller.Get(workflowState.ID);
            if (result is OkObjectResult okResult)
            {
                apiResource = okResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(apiResource);
            Assert.Equal(workflowState.ID, apiResource.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var workflowStatePostDto = new WorkflowStatePostDto("Test WorkflowState", workflow.ID);

            WorkflowStateDto? apiResource = null;
            IActionResult result = _Controller.Post(workflowStatePostDto);
            if (result is CreatedResult createdResult)
            {
                apiResource = createdResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(apiResource);
            Assert.Equal(workflowStatePostDto.Title, apiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var workflowState = _Fixtures.CreateWorkflowState();
            var title = $"{workflowState.Title} Updated";
            var workflowStatePatchDto = new WorkflowStatePatchDto(workflowState.ID, title);

            IActionResult result = _Controller.Patch(workflowState.ID, workflowStatePatchDto);
            WorkflowStateDto? apiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                apiResource = okObjectResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(apiResource);
            Assert.Equal(workflowStatePatchDto.Title, apiResource.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var workflowState = _Fixtures.CreateWorkflowState();

            IActionResult result = _Controller.Delete(workflowState.ID);

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
