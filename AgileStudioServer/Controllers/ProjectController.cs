using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Services.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectDataProvider _DataProvider;

        private readonly BacklogItemDataProvider _BacklogItemDataProvider;

        private readonly SprintDataProvider _SprintDataProvider;

        private readonly ReleaseDataProvider _ReleaseDataProvider;

        public ProjectController(
            ProjectDataProvider projectDataProvider, 
            BacklogItemDataProvider backlogItemDataProvider,
            SprintDataProvider sprintDataProvider,
            ReleaseDataProvider releaseDataProvider)
        {
            _DataProvider = projectDataProvider;
            _BacklogItemDataProvider = backlogItemDataProvider;
            _SprintDataProvider = sprintDataProvider;
            _ReleaseDataProvider = releaseDataProvider;
        }

        [HttpGet(Name = "GetProjects")]
        [ProducesResponseType(typeof(List<ProjectApiResource>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_DataProvider.List());
        }

        [HttpGet("{id}", Name = "GetProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource == null){
                return NotFound();
            }

            return Ok(apiResource);
        }

        [HttpGet("{id}/BacklogItems", Name = "GetProjectBacklogItems")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemApiResource>), StatusCodes.Status200OK)]
        public IActionResult GetBacklogItemsForProject(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            return Ok(_BacklogItemDataProvider.ListForProjectId(id));
        }

        [HttpGet("{id}/Sprints", Name = "GetProjectSprints")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<SprintApiResource>), StatusCodes.Status200OK)]
        public IActionResult GetSprintsForProject(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            return Ok(_SprintDataProvider.ListForProjectId(id));
        }

        [HttpGet("{id}/Releases", Name = "GetProjectReleases")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ReleaseApiResource>), StatusCodes.Status200OK)]
        public IActionResult GetReleasesForProject(int id)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            return Ok(_ReleaseDataProvider.ListForProjectId(id));
        }

        [HttpPost(Name = "CreateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(ProjectPostDto projectPostDto)
        {
            var apiResource = _DataProvider.Create(projectPostDto);

            var projectUrl = "";
            if (Url != null){
                projectUrl = Url.Action(nameof(Get), new { id = apiResource.ID }) ?? projectUrl;
            }
            return Created(projectUrl, apiResource);
        }

        [HttpPatch("{id}", Name = "UpdateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectApiResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, ProjectPatchDto projectPatchDto)
        {
            var apiResource = _DataProvider.Get(id);
            if (apiResource is null)
            {
                return NotFound();
            }

            apiResource = _DataProvider.Update(id, projectPatchDto);
            
            return new OkObjectResult(apiResource);
        }

        [HttpDelete("{id}", Name = "DeleteProject")]
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