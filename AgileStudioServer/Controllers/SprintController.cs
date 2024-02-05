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
            var apiResource = _DataProvider.Get(id);
            if (apiResource == null){
                return NotFound();
            }

            return Ok(apiResource);
        }

        [HttpPost(Name = "CreateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(SprintPostDto sprintPostDto)
        {
            var apiResource = _DataProvider.Create(sprintPostDto);
            if (apiResource is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var apiResourceUrl = "";
            if (Url != null){
                apiResourceUrl = Url.Action(nameof(Get), new { id = apiResource.ID }) ?? apiResourceUrl;
            }
            return Created(apiResourceUrl, apiResource);
        }

        [HttpPatch("{id}", Name = "UpdateSprint")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SprintApiResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, SprintPatchDto sprintPatchDto)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            apiResource = _DataProvider.Update(id, sprintPatchDto);
            if (apiResource is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkObjectResult(apiResource);
        }

        [HttpDelete("{id}", Name = "DeleteSprint")]
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

            var result = _DataProvider.Delete(id);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new OkResult();
        }
    }
}