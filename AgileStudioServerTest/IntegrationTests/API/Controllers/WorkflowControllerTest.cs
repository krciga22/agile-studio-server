using AgileStudioServer.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using AgileStudioServer.API.Controllers;
using AgileStudioServer.API.ApiResources;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.API.Controllers
{
    public class WorkflowControllerTest : AbstractControllerTest
    {
        private readonly WorkflowController _Controller;

        public WorkflowControllerTest(
            DBContext dbContext,
            Fixtures fixtures,
            WorkflowController controller) : base(dbContext, fixtures)
        {
            _Controller = controller;
        }

        [Fact]
        public void Get_WithNoArguments_ReturnsApiResources()
        {
            List<Workflow> workflows = new() {
                _Fixtures.CreateWorkflow("Test Workflow 1"),
                _Fixtures.CreateWorkflow("Test Workflow 2")
            };

            List<WorkflowApiResource>? apiResources = null;
            IActionResult result = _Controller.Get();
            if (result is OkObjectResult okResult)
            {
                apiResources = okResult.Value as List<WorkflowApiResource>;
            }

            Assert.IsType<List<WorkflowApiResource>>(apiResources);
            Assert.Equal(workflows.Count, apiResources.Count);
        }

        [Fact]
        public void Get_WithId_ReturnsApiResource()
        {
            var workflow = _Fixtures.CreateWorkflow();

            WorkflowApiResource? apiResource = null;
            IActionResult result = _Controller.Get(workflow.ID);
            if (result is OkObjectResult okResult)
            {
                apiResource = okResult.Value as WorkflowApiResource;
            }

            Assert.IsType<WorkflowApiResource>(apiResource);
            Assert.Equal(workflow.ID, apiResource.ID);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFoundResult()
        {
            IActionResult result = _Controller.Get(Constants.NonExistantId);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public void GetWorkflowStatesForWorkflow_WithId_ReturnsApiResources()
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

            List<WorkflowStateApiResource>? apiResources = null;
            IActionResult result = _Controller.GetWorkflowStatesForWorkflow(workflow.ID);
            if (result is OkObjectResult okResult)
            {
                apiResources = okResult.Value as List<WorkflowStateApiResource>;
            }

            Assert.IsType<List<WorkflowStateApiResource>>(apiResources);
            Assert.Equal(workflowStates.Count, apiResources.Count);
        }

        [Fact]
        public void Post_WithDto_ReturnsApiResource()
        {
            var workflowPostDto = new WorkflowPostDto("Test Workflow");

            WorkflowApiResource? apiResource = null;
            IActionResult result = _Controller.Post(workflowPostDto);
            if (result is CreatedResult createdResult)
            {
                apiResource = createdResult.Value as WorkflowApiResource;
            }

            Assert.IsType<WorkflowApiResource>(apiResource);
            Assert.Equal(workflowPostDto.Title, apiResource.Title);
        }

        [Fact]
        public void Patch_WithIdAndDto_ReturnsApiResource()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var title = $"{workflow.Title} Updated";
            var workflowPatchDto = new WorkflowPatchDto(title);

            IActionResult result = _Controller.Patch(workflow.ID, workflowPatchDto);
            WorkflowApiResource? apiResource = null;
            if (result is OkObjectResult okObjectResult)
            {
                apiResource = okObjectResult.Value as WorkflowApiResource;
            }

            Assert.IsType<WorkflowApiResource>(apiResource);
            Assert.Equal(workflowPatchDto.Title, apiResource.Title);
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
