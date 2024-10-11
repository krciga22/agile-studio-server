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
        public void Get_WithId_ReturnsDto()
        {
            var workflowState = _Fixtures.CreateWorkflowState();

            WorkflowStateDto? dto = null;
            IActionResult result = _Controller.Get(workflowState.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(dto);
            Assert.Equal(workflowState.ID, dto.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var workflowStatePostDto = new WorkflowStatePostDto("Test WorkflowState", workflow.ID);

            WorkflowStateDto? dto = null;
            IActionResult result = _Controller.Post(workflowStatePostDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(dto);
            Assert.Equal(workflowStatePostDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var workflowState = _Fixtures.CreateWorkflowState();
            var title = $"{workflowState.Title} Updated";
            var workflowStatePatchDto = new WorkflowStatePatchDto(workflowState.ID, title);

            IActionResult result = _Controller.Patch(workflowState.ID, workflowStatePatchDto);
            WorkflowStateDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as WorkflowStateDto;
            }

            Assert.IsType<WorkflowStateDto>(dto);
            Assert.Equal(workflowStatePatchDto.Title, dto.Title);
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
