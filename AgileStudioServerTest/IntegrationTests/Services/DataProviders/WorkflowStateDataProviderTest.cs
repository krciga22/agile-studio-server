using AgileStudioServer;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Entities;
using AgileStudioServer.Services.DataProviders;

namespace AgileStudioServerTest.IntegrationTests.Services.DataProviders
{
    public class WorkflowStateDataProviderTest : AbstractDataProviderTest
    {
        private readonly WorkflowStateDataProvider _DataProvider;

        public WorkflowStateDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            WorkflowStateDataProvider workflowStateDataProvider) : base(dbContext, fixtures)
        {
            _DataProvider = workflowStateDataProvider;
        }

        [Fact]
        public void CreateWorkflowState_WithPostDto_ReturnsApiResource()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var workflowStatePostDto = new WorkflowStatePostDto("Test WorkflowState", workflow.ID);

            var apiResource = _DataProvider.Create(workflowStatePostDto);

            Assert.IsType<WorkflowStateApiResource>(apiResource);
        }

        [Fact]
        public void GetWorkflowState_ById_ReturnsApiResource()
        {
            var workflowState = _Fixtures.CreateWorkflowState();

            var apiResource = _DataProvider.Get(workflowState.ID);

            Assert.IsType<WorkflowStateApiResource>(apiResource);
        }

        [Fact]
        public void GetWorkflowStates_WithNoArguments_ReturnsApiResources()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var workflowStates = new List<WorkflowState>
            {
                _Fixtures.CreateWorkflowState(
                    title: "Test WorkflowState 1",
                    workflow: workflow),
                _Fixtures.CreateWorkflowState(
                    title: "Test WorkflowState 2",
                    workflow: workflow)
            };

            List<WorkflowStateApiResource> apiResources = _DataProvider.ListForWorkflowId(workflow.ID);

            Assert.Equal(workflowStates.Count, apiResources.Count);
        }

        [Fact]
        public void UpdateWorkflowState_WithValidPatchDto_ReturnsApiResource()
        {
            var workflowState = _Fixtures.CreateWorkflowState();
            var title = $"{workflowState.Title} Updated";
            var workflowStatePatchDto = new WorkflowStatePatchDto(title);

            var apiResource = _DataProvider.Update(workflowState.ID, workflowStatePatchDto);

            Assert.IsType<WorkflowStateApiResource>(apiResource);
            Assert.Equal(workflowStatePatchDto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteWorkflowState_WithValidId_ReturnsTrue()
        {
            var workflowState = _Fixtures.CreateWorkflowState();

            _DataProvider.Delete(workflowState.ID);

            var result = _DataProvider.Get(workflowState.ID);
            Assert.Null(result);
        }
    }
}
