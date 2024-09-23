using AgileStudioServer.API.Dtos;
using AgileStudioServer.Application.Services.DataProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ReleaseController : ControllerBase
    {
        private readonly ReleaseDataProvider _DataProvider;

        private readonly ProjectDataProvider _ProjectDataProvider;

        public ReleaseController(ReleaseDataProvider releaseDataProvider, ProjectDataProvider projectDataProvider)
        {
            _DataProvider = releaseDataProvider;
            _ProjectDataProvider = projectDataProvider;
        }

        [HttpGet("{id}", Name = "GetRelease")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource == null)
            {
                return NotFound();
            }

            return Ok(apiResource);
        }

        [HttpPost(Name = "CreateRelease")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(ReleasePostDto releasePostDto)
        {
            var apiResource = _DataProvider.Create(releasePostDto);
            if (apiResource is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var apiResourceUrl = "";
            if (Url != null)
            {
                apiResourceUrl = Url.Action(nameof(Get), new { id = apiResource.ID }) ?? apiResourceUrl;
            }
            return Created(apiResourceUrl, apiResource);
        }

        [HttpPatch("{id}", Name = "UpdateRelease")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, ReleasePatchDto releasePatchDto)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            apiResource = _DataProvider.Update(id, releasePatchDto);

            return new OkObjectResult(apiResource);
        }

        [HttpDelete("{id}", Name = "DeleteRelease")]
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