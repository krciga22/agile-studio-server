using AgileStudioServer.API.DtosNew;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class WorkflowControllerTest : AbstractControllerTest
    {
        private readonly WorkflowController _Controller;

        public WorkflowControllerTest(
            DBContext dbContext,
            EntityFixtures fixtures,
            WorkflowController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithNoArguments_ReturnsDtos()
        {
            List<Workflow> workflows = new() {
                _Fixtures.CreateWorkflow("Test Workflow 1"),
                _Fixtures.CreateWorkflow("Test Workflow 2")
            };

            List<WorkflowDto>? dtos = null;
            IActionResult result = _Controller.Get();
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<WorkflowDto>;
            }

            Assert.IsType<List<WorkflowDto>>(dtos);
            Assert.Equal(workflows.Count, dtos.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsDto()
        {
            var workflow = _Fixtures.CreateWorkflow();

            WorkflowDto? dto = null;
            IActionResult result = _Controller.Get(workflow.ID);
            if (result is OkObjectResult okResult)
            {
                dto = okResult.Value as WorkflowDto;
            }

            Assert.IsType<WorkflowDto>(dto);
            Assert.Equal(workflow.ID, dto.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void GetWorkflowStatesForWorkflow_WithId_ReturnsDtos()
        {
            var workflow = _Fixtures.CreateWorkflow();

            List<WorkflowState> workflowStates = new() {
                _Fixtures.CreateWorkflowState(
                    title: "Test Workflow State 1",
                    workflow: workflow),
                _Fixtures.CreateWorkflowState(
                    title: "Test Workflow State 2",
                    workflow: workflow)
            };

            List<WorkflowStateDto>? dtos = null;
            IActionResult result = _Controller.GetWorkflowStatesForWorkflow(workflow.ID);
            if (result is OkObjectResult okResult)
            {
                dtos = okResult.Value as List<WorkflowStateDto>;
            }

            Assert.IsType<List<WorkflowStateDto>>(dtos);
            Assert.Equal(workflowStates.Count, dtos.Count);
        }

        [Fact]
        public void Post_WithDto_ReturnsDto()
        {
            var workflowPostDto = new WorkflowPostDto("Test Workflow");

            WorkflowDto? dto = null;
            IActionResult result = _Controller.Post(workflowPostDto);
            if (result is CreatedResult createdResult)
            {
                dto = createdResult.Value as WorkflowDto;
            }

            Assert.IsType<WorkflowDto>(dto);
            Assert.Equal(workflowPostDto.Title, dto.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsDto()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var title = $"{workflow.Title} Updated";
            var workflowPatchDto = new WorkflowPatchDto(workflow.ID, title);

            IActionResult result = _Controller.Patch(workflow.ID, workflowPatchDto);
            WorkflowDto? dto = null;
            if (result is OkObjectResult okObjectResult)
            {
                dto = okObjectResult.Value as WorkflowDto;
            }

            Assert.IsType<WorkflowDto>(dto);
            Assert.Equal(workflowPatchDto.Title, dto.Title);
        }

        [Fact]
        public void Delete_WithId_ReturnsOkResult()
        {
            var workflow = _Fixtures.CreateWorkflow();

            IActionResult result = _Controller.Delete(workflow.ID);

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
