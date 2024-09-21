using AgileStudioServer.API.Dtos;
using AgileStudioServer.API.ApiResources;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Application.Services.DataProviders;
using AgileStudioServer.Data;

namespace AgileStudioServerTest.IntegrationTests.Application.Services.DataProviders
{
    public class WorkflowDataProviderTest : AbstractDataProviderTest
    {
        private readonly WorkflowDataProvider _DataProvider;

        public WorkflowDataProviderTest(
            DBContext dbContext,
            Fixtures fixtures,
            WorkflowDataProvider workflowDataProvider) : base(dbContext, fixtures)
        {
            _DataProvider = workflowDataProvider;
        }

        [Fact]
        public void CreateWorkflow_WithPostDto_ReturnsApiResource()
        {
            var workflowPostDto = new WorkflowPostDto("Test Workflow");

            var apiResource = _DataProvider.Create(workflowPostDto);

            Assert.IsType<WorkflowApiResource>(apiResource);
        }

        [Fact]
        public void GetWorkflow_ById_ReturnsApiResource()
        {
            var workflow = _Fixtures.CreateWorkflow();

            var apiResource = _DataProvider.Get(workflow.ID);

            Assert.IsType<WorkflowApiResource>(apiResource);
        }

        [Fact]
        public void GetWorkflows_WithNoArguments_ReturnsApiResources()
        {
            var workflows = new List<Workflow>
            {
                _Fixtures.CreateWorkflow("Test Workflow 1"),
                _Fixtures.CreateWorkflow("Test Workflow 2")
            };

            List<WorkflowApiResource> apiResources = _DataProvider.List();

            Assert.Equal(workflows.Count, apiResources.Count);
        }

        [Fact]
        public void UpdateWorkflow_WithValidPatchDto_ReturnsApiResource()
        {
            var workflow = _Fixtures.CreateWorkflow();
            var title = $"{workflow.Title} Updated";
            var workflowPatchDto = new WorkflowPatchDto(title);

            var apiResource = _DataProvider.Update(workflow.ID, workflowPatchDto);

            Assert.IsType<WorkflowApiResource>(apiResource);
            Assert.Equal(workflowPatchDto.Title, apiResource.Title);
        }

        [Fact]
        public void DeleteWorkflow_WithValidId_ReturnsTrue()
        {
            var workflow = _Fixtures.CreateWorkflow();

            _DataProvider.Delete(workflow.ID);

            var result = _DataProvider.Get(workflow.ID);
            Assert.Null(result);
        }
    }
}
