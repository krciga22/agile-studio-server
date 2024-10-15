using AgileStudioServer.API.Dtos;
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;
using AgileStudioServer.Core.Hydrator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WorkflowStateController : ControllerBase
    {
        private readonly WorkflowStateService _WorkflowStateService;
        private readonly Hydrator _Hydrator;

        public WorkflowStateController(WorkflowStateService workflowStateService, Hydrator hydrator)
        {
            _WorkflowStateService = workflowStateService;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}", Name = "GetWorkflowState")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(WorkflowStateDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _WorkflowStateService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(HydrateWorkflowStateDto(model));
        }

        [HttpPost(Name = "CreateWorkflowState")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowStateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(WorkflowStatePostDto workflowStatePostDto)
        {
            var model = HydrateWorkflowStateModel(workflowStatePostDto);
            model = _WorkflowStateService.Create(model);

            string workflowStateUrl = "";
            if (Url != null)
            {
                workflowStateUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? workflowStateUrl;
            }

            var dto = HydrateWorkflowStateDto(model);

            return Created(workflowStateUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateWorkflowState")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowStateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, WorkflowStatePatchDto workflowStatePatchDto)
        {
            if (id != workflowStatePatchDto.ID)
            {
                return BadRequest();
            }

            WorkflowStateDto dto;
            try
            {
                WorkflowState model = HydrateWorkflowStateModel(workflowStatePatchDto);
                model = _WorkflowStateService.Update(model);
                dto = HydrateWorkflowStateDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(WorkflowState)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(dto);
        }

        [HttpDelete("{id}", Name = "DeleteWorkflowState")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var model = _WorkflowStateService.Get(id);
            if (model is null)
            {
                return NotFound();
            }

            _WorkflowStateService.Delete(model);

            return new OkResult();
        }

        private WorkflowStateDto HydrateWorkflowStateDto(WorkflowState workflowState, int depth = 1)
        {
            return (WorkflowStateDto)_Hydrator.Hydrate(
                workflowState, typeof(WorkflowStateDto), depth
            );
        }

        private WorkflowState HydrateWorkflowStateModel(WorkflowStatePostDto workflowStatePostDto, int depth = 3)
        {
            return (WorkflowState)_Hydrator.Hydrate(
                workflowStatePostDto, typeof(WorkflowState), depth
            );
        }

        private WorkflowState HydrateWorkflowStateModel(WorkflowStatePatchDto workflowStatePatchDto, int depth = 3)
        {
            return (WorkflowState) _Hydrator.Hydrate(
                workflowStatePatchDto, typeof(WorkflowState), depth
            );
        }
    }
}