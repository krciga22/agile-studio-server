using AgileStudioServer.API.ApiResources;
using AgileStudioServer.API.Dtos;
using AgileStudioServer.Application.Services.DataProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WorkflowStateController : ControllerBase
    {
        private readonly WorkflowStateDataProvider _DataProvider;

        public WorkflowStateController(WorkflowStateDataProvider workflowStateDataProvider)
        {
            _DataProvider = workflowStateDataProvider;
        }

        [HttpGet("{id}", Name = "GetWorkflowState")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(WorkflowStateApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource == null)
            {
                return NotFound();
            }

            return Ok(apiResource);
        }

        [HttpPost(Name = "CreateWorkflowState")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowStateApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(WorkflowStatePostDto workflowStatePostDto)
        {
            var apiResource = _DataProvider.Create(workflowStatePostDto);

            var workflowStateUrl = "";
            if (Url != null)
            {
                workflowStateUrl = Url.Action(nameof(Get), new { id = apiResource.ID }) ?? workflowStateUrl;
            }
            return Created(workflowStateUrl, apiResource);
        }

        [HttpPatch("{id}", Name = "UpdateWorkflowState")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowStateApiResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, WorkflowStatePatchDto workflowStatePatchDto)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            apiResource = _DataProvider.Update(id, workflowStatePatchDto);

            return new OkObjectResult(apiResource);
        }

        [HttpDelete("{id}", Name = "DeleteWorkflowState")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            _DataProvider.Delete(id);

            return new OkResult();
        }
    }
}