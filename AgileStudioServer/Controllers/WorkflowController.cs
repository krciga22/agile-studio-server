using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Services.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        private readonly WorkflowDataProvider _DataProvider;
        private readonly WorkflowStateDataProvider _WorkflowStateDataProvider;

        public WorkflowController(
            WorkflowDataProvider workflowDataProvider, 
            WorkflowStateDataProvider workflowStateDataProvider)
        {
            _DataProvider = workflowDataProvider;
            _WorkflowStateDataProvider = workflowStateDataProvider;
        }

        [HttpGet(Name = "GetWorkflows")]
        [ProducesResponseType(typeof(List<WorkflowApiResource>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_DataProvider.List());
        }

        [HttpGet("{id}", Name = "GetWorkflow")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(WorkflowApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource == null){
                return NotFound();
            }

            return Ok(apiResource);
        }

        [HttpGet("{id}/WorkflowStates", Name = "GetWorkflowWorkflowStates")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<WorkflowStateApiResource>), StatusCodes.Status200OK)]
        public IActionResult GetWorkflowStatesForWorkflow(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            return Ok(_WorkflowStateDataProvider.ListForWorkflowId(id));
        }

        [HttpPost(Name = "CreateWorkflow")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(WorkflowPostDto workflowPostDto)
        {
            var apiResource = _DataProvider.Create(workflowPostDto);

            var workflowUrl = "";
            if (Url != null){
                workflowUrl = Url.Action(nameof(Get), new { id = apiResource.ID }) ?? workflowUrl;
            }
            return Created(workflowUrl, apiResource);
        }

        [HttpPatch("{id}", Name = "UpdateWorkflow")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowApiResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, WorkflowPatchDto workflowPatchDto)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            apiResource = _DataProvider.Update(id, workflowPatchDto);
            
            return new OkObjectResult(apiResource);
        }

        [HttpDelete("{id}", Name = "DeleteWorkflow")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if(apiResource is null){
                return NotFound();
            }

            _DataProvider.Delete(id);
            
            return new OkResult();
        }
    }
}