using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Services.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SprintController : ControllerBase
    {
        private readonly SprintDataProvider _DataProvider;

        private readonly ProjectDataProvider _ProjectDataProvider;

        public SprintController(SprintDataProvider sprintDataProvider, ProjectDataProvider projectDataProvider)
        {
            _DataProvider = sprintDataProvider;
            _ProjectDataProvider = projectDataProvider;
        }

        [HttpGet("{id}", Name = "GetSprint")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SprintApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var sprint = _DataProvider.Get(id);
            if (sprint == null){
                return NotFound();
            }

            return Ok(sprint);
        }

        [HttpPost(Name = "CreateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(SprintPostDto sprintPostDto)
        {
            var sprintApiResource = _DataProvider.Create(sprintPostDto);
            if (sprintApiResource is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var sprintUrl = "";
            if (Url != null){
                sprintUrl = Url.Action(nameof(Get), new { id = sprintApiResource.ID }) ?? sprintUrl;
            }
            return Created(sprintUrl, sprintApiResource);
        }

        [HttpPatch("{id}", Name = "UpdateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintApiResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, SprintPatchDto sprintPatchDto)
        {
            var sprint = _DataProvider.Get(id);
            if (sprint is null)
            {
                return NotFound();
            }

            var sprintApiResource = _DataProvider.Update(id, sprintPatchDto);
            if (sprintApiResource is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkObjectResult(sprintApiResource);
        }

        [HttpDelete("{id}", Name = "DeleteSprint")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var sprint = _DataProvider.Get(id);
            if (sprint is null)
            {
                return NotFound();
            } 

            var result = _DataProvider.Delete(id);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkResult();
        }
    }
}