using AgileStudioServer.API.DtosNew;
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
    public class SprintController : ControllerBase
    {
        private SprintService _SprintService;
        private readonly Hydrator _Hydrator;

        public SprintController(
            SprintService sprintService,
            Hydrator hydrator)
        {
            _SprintService = sprintService;
            _Hydrator = hydrator;
        }


        [HttpGet("{id}", Name = "GetSprint")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _SprintService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateSprintDto(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(SprintPostDto sprintPostDto)
        {
            Sprint model = HydrateSprintModel(sprintPostDto);
            model = _SprintService.Create(model);

            var sprintUrl = "";
            if (Url != null)
            {
                sprintUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? sprintUrl;
            }

            var dto = HydrateSprintDto(model);

            return Created(sprintUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, SprintPatchDto sprintPatchDto)
        {
            SprintDto dto;
            try
            {
                Sprint model = HydrateSprintModel(sprintPatchDto);
                model = _SprintService.Update(model);
                dto = HydrateSprintDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(Sprint)))
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

        [HttpDelete("{id}", Name = "DeleteSprint")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Sprint? model = _SprintService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _SprintService.Delete(model);

            return new OkResult();
        }

        private Sprint HydrateSprintModel(SprintPostDto sprintPostDto, int depth = 1)
        {
            return (Sprint)_Hydrator.Hydrate(
                sprintPostDto, typeof(Sprint), depth
            );
        }

        private Sprint HydrateSprintModel(SprintPatchDto sprintPatchDto, int depth = 1)
        {
            return (Sprint)_Hydrator.Hydrate(
                sprintPatchDto, typeof(Sprint), depth
            );
        }

        private SprintDto HydrateSprintDto(Sprint sprint, int depth = 1)
        {
            return (SprintDto) _Hydrator.Hydrate(
                sprint, typeof(SprintDto), depth
            );
        }
    }
}