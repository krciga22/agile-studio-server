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
    public class ReleaseController : ControllerBase
    {
        private readonly ReleaseService _ReleaseService;
        private readonly ProjectService _ProjectService;
        private readonly Hydrator _Hydrator;

        public ReleaseController(
            ReleaseService releaseService, 
            ProjectService projectService, 
            Hydrator hydrator)
        {
            _ReleaseService = releaseService;
            _ProjectService = projectService;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}", Name = "GetRelease")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _ReleaseService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateReleaseDto(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateRelease")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Post(ReleasePostDto releasePostDto)
        {
            Release model = HydrateReleaseModel(releasePostDto);
            model = _ReleaseService.Create(model);

            var releaseUrl = "";
            if (Url != null)
            {
                releaseUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? releaseUrl;
            }

            var dto = HydrateReleaseDto(model);

            return Created(releaseUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateRelease")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ReleaseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, ReleasePatchDto releasePatchDto)
        {
            if (id != releasePatchDto.ID)
            {
                return BadRequest();
            }

            ReleaseDto dto;
            try
            {
                Release model = HydrateReleaseModel(releasePatchDto);
                model = _ReleaseService.Update(model);
                dto = HydrateReleaseDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(Release)))
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

        [HttpDelete("{id}", Name = "DeleteRelease")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Release? model = _ReleaseService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _ReleaseService.Delete(model);

            return new OkResult();
        }

        private ReleaseDto HydrateReleaseDto(Release release, int depth = 1)
        {
            return (ReleaseDto)_Hydrator.Hydrate(
                release, typeof(ReleaseDto), depth
            );
        }

        private Release HydrateReleaseModel(ReleasePostDto releasePostDto, int depth = 3)
        {
            return (Release)_Hydrator.Hydrate(
                releasePostDto, typeof(Release), depth
            );
        }

        private Release HydrateReleaseModel(ReleasePatchDto releasePatchDto, int depth = 3)
        {
            return (Release)_Hydrator.Hydrate(
                releasePatchDto, typeof(Release), depth
            );
        }
    }
}