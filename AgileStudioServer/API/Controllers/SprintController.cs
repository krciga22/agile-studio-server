using AgileStudioServer.API.DtosNew;
using AgileStudioServer.API.DtosNew.DtoModelConverters;
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SprintController : ControllerBase
    {
        private SprintService _sprintService;
        private SprintConverter _sprintConverter;
        private SprintPostConverter _sprintPostConverter;
        private SprintPatchConverter _sprintPatchConverter;

        public SprintController(
            SprintService sprintService,
            SprintConverter sprintConverter,
            SprintPostConverter sprintPostConverter,
            SprintPatchConverter sprintPatchConverter)
        {
            _sprintService = sprintService;
            _sprintConverter = sprintConverter;
            _sprintPostConverter = sprintPostConverter;
            _sprintPatchConverter = sprintPatchConverter;
        }


        [HttpGet("{id}", Name = "GetSprint")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var sprint = _sprintService.Get(id);
            if (sprint == null)
            {
                return NotFound();
            }

            return Ok(_sprintConverter.ConvertToDto(sprint));
        }

        [HttpPost(Name = "CreateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(SprintPostDto sprintPostDto)
        {
            Sprint sprint;
            try
            {
                sprint = _sprintPostConverter.ConvertToModel(sprintPostDto);
                sprint = _sprintService.Create(sprint);
            }
            catch (Exception)
            {
                // todo log exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var apiResourceUrl = "";
            if (Url != null)
            {
                apiResourceUrl = Url.Action(nameof(Get), new { id = sprint.ID }) ?? apiResourceUrl;
            }
            return Created(apiResourceUrl, _sprintConverter.ConvertToDto(sprint));
        }

        [HttpPatch("{id}", Name = "UpdateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, SprintPatchDto sprintPatchDto)
        {
            Sprint sprint;
            try
            {
                sprint = _sprintPatchConverter.ConvertToModel(sprintPatchDto);
                sprint = _sprintService.Update(sprint);
            }
            catch (ModelNotFoundException e)
            {
                // todo log exception
                return e.ModelClassName.Equals(nameof(Sprint)) ?
                    NotFound() : 
                    StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                // todo log exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkObjectResult(_sprintConverter.ConvertToDto(sprint));
        }

        [HttpDelete("{id}", Name = "DeleteSprint")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                Sprint? sprint = _sprintService.Get(id);
                if (sprint == null)
                {
                    return NotFound();
                }

                _sprintService.Delete(sprint);
            }
            catch (Exception)
            {
                // todo log exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkResult();
        }
    }
}