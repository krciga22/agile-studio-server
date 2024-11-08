using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests.Application.Services
{
    public class WorkflowStateServiceTest : AbstractServiceTest
    {
        private readonly WorkflowStateService _workflowStateService;

        public WorkflowStateServiceTest(
            DBContext dbContext,
            ModelFixtures fixtures,
            WorkflowStateService workflowStateService) : base(dbContext, fixtures)
        {
            _workflowStateService = workflowStateService;
        }

        [Fact]
        public void Create_ReturnsWorkflowState()
        {
            Workflow workflow = _Fixtures.CreateWorkflow();
            WorkflowState workflowState = new("Test WorkflowState", workflow.ID);

            workflowState = _workflowStateService.Create(workflowState);

            Assert.NotNull(workflowState);
            Assert.True(workflowState.ID > 0);
        }

        [Fact]
        public void Get_ReturnsWorkflowState()
        {
            var workflowState = _Fixtures.CreateWorkflowState();

            var returnedWorkflowState = _workflowStateService.Get(workflowState.ID);

            Assert.NotNull(returnedWorkflowState);
            Assert.Equal(workflowState.ID, returnedWorkflowState.ID);
        }

        [Fact]
        public void GetByWorkflowId_ReturnsWorkflowStates()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var workflowStates = new List<WorkflowState>
            {
                _Fixtures.CreateWorkflowState("Test WorkflowState 1", workflow: workflow),
                _Fixtures.CreateWorkflowState("Test WorkflowState 2", workflow: workflow)
            };

            List<WorkflowState> returnedWorkflowStates = _workflowStateService
                .GetByWorkflowId(workflow.ID);

            Assert.Equal(workflowStates.Count, returnedWorkflowStates.Count);
        }

        [Fact]
        public void Update_ReturnsUpdatedWorkflowState()
        {
            var workflowState = _Fixtures.CreateWorkflowState();
            var title = $"{workflowState.Title} Updated";

            workflowState.Title = title;
            workflowState = _workflowStateService.Update(workflowState);

            Assert.NotNull(workflowState);
            Assert.Equal(title, workflowState.Title);
        }

        [Fact]
        public void Delete_DeletesWorkflowState()
        {
            var workflowState = _Fixtures.CreateWorkflowState();

            _workflowStateService.Delete(workflowState);

            workflowState = _workflowStateService.Get(workflowState.ID);
            Assert.Null(workflowState);
        }
    }
}
