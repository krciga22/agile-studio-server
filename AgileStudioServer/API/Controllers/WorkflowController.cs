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
    public class WorkflowController : ControllerBase
    {
        private readonly WorkflowService _WorkflowService;
        private readonly WorkflowStateService _WorkflowStateService;
        private readonly Hydrator _Hydrator;

        public WorkflowController(WorkflowService workflowService, 
            WorkflowStateService workflowStateService, Hydrator hydrator)
        {
            _WorkflowService = workflowService;
            _WorkflowStateService = workflowStateService;
            _Hydrator = hydrator;
        }

        [HttpGet(Name = "GetWorkflows")]
        [ProducesResponseType(typeof(List<WorkflowDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var models = _WorkflowService.GetAll();
            var dtos = HydrateWorkflowDtos(models);
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetWorkflow")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(WorkflowDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _WorkflowService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateWorkflowDto(model);
            return Ok(dto);
        }

        [HttpGet("{id}/WorkflowStates", Name = "GetWorkflowWorkflowStates")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<WorkflowStateDto>), StatusCodes.Status200OK)]
        public IActionResult GetWorkflowStatesForWorkflow(int id)
        {
            var workflow = _WorkflowService.Get(id);
            if(workflow == null)
            {
                return NotFound();
            }

            var models = _WorkflowStateService.GetByWorkflowId(workflow.ID);
            var dtos = HydrateWorkflowStateDtos(models);
            return Ok(dtos);
        }

        [HttpPost(Name = "CreateWorkflow")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(WorkflowPostDto workflowPostDto)
        {
            Workflow model = HydrateWorkflowModel(workflowPostDto);
            model = _WorkflowService.Create(model);

            string workflowUrl = "";
            if (Url != null)
            {
                workflowUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? workflowUrl;
            }

            var dto = HydrateWorkflowDto(model);

            return Created(workflowUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateWorkflow")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WorkflowDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, WorkflowPatchDto workflowPatchDto)
        {
            if(id != workflowPatchDto.ID)
            {
                return BadRequest();
            }

            WorkflowDto dto;
            try
            {
                Workflow model = HydrateWorkflowModel(workflowPatchDto);
                model = _WorkflowService.Update(model);
                dto = HydrateWorkflowDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(Workflow)))
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

        [HttpDelete("{id}", Name = "DeleteWorkflow")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Workflow? model = _WorkflowService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _WorkflowService.Delete(model);

            return new OkResult();
        }

        private List<WorkflowDto> HydrateWorkflowDtos(List<Workflow> workflows, int depth = 1)
        {
            List<WorkflowDto> dtos = new();

            workflows.ForEach(workflow => {
                WorkflowDto dto = HydrateWorkflowDto(workflow, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private WorkflowDto HydrateWorkflowDto(Workflow workflow, int depth = 1)
        {
            return (WorkflowDto)_Hydrator.Hydrate(
                workflow, typeof(WorkflowDto), depth
            );
        }

        private List<WorkflowStateDto> HydrateWorkflowStateDtos(List<WorkflowState> workflowStates, int depth = 1)
        {
            List<WorkflowStateDto> dtos = new();

            workflowStates.ForEach(workflowState => {
                WorkflowStateDto dto = HydrateWorkflowStateDto(workflowState, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private WorkflowStateDto HydrateWorkflowStateDto(WorkflowState workflowState, int depth = 1)
        {
            return (WorkflowStateDto)_Hydrator.Hydrate(
                workflowState, typeof(WorkflowStateDto), depth
            );
        }

        private Workflow HydrateWorkflowModel(WorkflowPostDto workflowPostDto, int depth = 3)
        {
            return (Workflow)_Hydrator.Hydrate(
                workflowPostDto, typeof(Workflow), depth
            );
        }

        private Workflow HydrateWorkflowModel(WorkflowPatchDto workflowPatchDto, int depth = 3)
        {
            return (Workflow)_Hydrator.Hydrate(
                workflowPatchDto, typeof(Workflow), depth
            );
        }

        private void HydrateWorkflowModel(WorkflowPatchDto workflowPatchDto, Workflow workflow, int depth = 3)
        {
            _Hydrator.Hydrate(
                workflowPatchDto, workflow, depth
            );
        }
    }
}