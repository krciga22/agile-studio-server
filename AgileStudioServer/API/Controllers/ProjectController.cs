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
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _ProjectService;

        private readonly BacklogItemService _BacklogItemService;

        private readonly SprintService _SprintService;

        private readonly ReleaseService _ReleaseService;

        private readonly Hydrator _Hydrator;

        public ProjectController(
            ProjectService projectService,
            BacklogItemService backlogItemDataProvider,
            SprintService sprintDataProvider,
            ReleaseService releaseDataProvider,
            Hydrator Hydrator)
        {
            _ProjectService = projectService;
            _BacklogItemService = backlogItemDataProvider;
            _SprintService = sprintDataProvider;
            _ReleaseService = releaseDataProvider;
            _Hydrator = Hydrator;
        }

        [HttpGet(Name = "GetProjects")]
        [ProducesResponseType(typeof(List<ProjectDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var models = _ProjectService.GetAll();
            var dtos = HydrateProjectDtos(models);
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetProject")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _ProjectService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateProjectDto(model);
            return Ok(dto);
        }

        [HttpGet("{id}/BacklogItems", Name = "GetProjectBacklogItems")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemDto>), StatusCodes.Status200OK)]
        public IActionResult GetBacklogItemsForProject(int id)
        {
            var models = _BacklogItemService.GetByProjectId(id);
            var dtos = HydrateBacklogItemSummaryDtos(models);
            return Ok(dtos);
        }

        [HttpGet("{id}/Sprints", Name = "GetProjectSprints")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<SprintSummaryDto>), StatusCodes.Status200OK)]
        public IActionResult GetSprintsForProject(int id)
        {
            var models = _SprintService.GetByProjectId(id);
            var dtos = HydrateSprintSummaryDtos(models);
            return Ok(dtos);
        }

        [HttpGet("{id}/Releases", Name = "GetProjectReleases")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ReleaseSummaryDto>), StatusCodes.Status200OK)]
        public IActionResult GetReleasesForProject(int id)
        {
            var models = _ReleaseService.GetByProjectId(id);
            var dtos = HydrateReleaseSummaryDtos(models);
            return Ok(dtos);
        }

        [HttpPost(Name = "CreateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(ProjectPostDto projectPostDto)
        {
            Project model = HydrateProjectModel(projectPostDto);
            model = _ProjectService.Create(model);

            string projectUrl = "";
            if (Url != null)
            {
                projectUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? projectUrl;
            }

            var dto = HydrateProjectDto(model);

            return Created(projectUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateProject")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, ProjectPatchDto projectPatchDto)
        {
            if (id != projectPatchDto.ID)
            {
                return BadRequest();
            }

            ProjectDto dto;
            try
            {
                Project model = HydrateProjectModel(projectPatchDto);
                model = _ProjectService.Update(model);
                dto = HydrateProjectDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(Project)))
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

        [HttpDelete("{id}", Name = "DeleteProject")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Project? model = _ProjectService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _ProjectService.Delete(model);

            return new OkResult();
        }

        private List<ProjectDto> HydrateProjectDtos(List<Project> projects, int depth = 1)
        {
            List<ProjectDto> dtos = new();

            projects.ForEach(project => {
                ProjectDto dto = HydrateProjectDto(project, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private ProjectDto HydrateProjectDto(Project project, int depth = 1)
        {
            return (ProjectDto)_Hydrator.Hydrate(
                project, typeof(ProjectDto), depth
            );
        }

        private List<BacklogItemDto> HydrateBacklogItemSummaryDtos(List<BacklogItem> backlogItems, int depth = 1)
        {
            List<BacklogItemDto> dtos = new();

            backlogItems.ForEach(backlogItem => {
                BacklogItemDto dto = HydrateBacklogItemDto(backlogItem, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private BacklogItemDto HydrateBacklogItemDto(BacklogItem backlogItem, int depth = 1)
        {
            return (BacklogItemDto)_Hydrator.Hydrate(
                backlogItem, typeof(BacklogItemDto), depth
            );
        }

        private List<SprintSummaryDto> HydrateSprintSummaryDtos(List<Sprint> sprints, int depth = 1)
        {
            List<SprintSummaryDto> dtos = new();

            sprints.ForEach(sprint => {
                SprintSummaryDto dto = HydrateSprintSummaryDto(sprint, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private SprintSummaryDto HydrateSprintSummaryDto(Sprint sprint, int depth = 1)
        {
            return (SprintSummaryDto)_Hydrator.Hydrate(
                sprint, typeof(SprintSummaryDto), depth
            );
        }

        private List<ReleaseSummaryDto> HydrateReleaseSummaryDtos(List<Release> releases, int depth = 1)
        {
            List<ReleaseSummaryDto> dtos = new();

            releases.ForEach(release => {
                ReleaseSummaryDto dto = HydrateReleaseSummaryDto(release, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private ReleaseSummaryDto HydrateReleaseSummaryDto(Release release, int depth = 1)
        {
            return (ReleaseSummaryDto)_Hydrator.Hydrate(
                release, typeof(ReleaseSummaryDto), depth
            );
        }

        private Project HydrateProjectModel(ProjectPostDto projectPostDto, int depth = 1)
        {
            return (Project)_Hydrator.Hydrate(
                projectPostDto, typeof(Project), depth
            );
        }

        private Project HydrateProjectModel(ProjectPatchDto projectPatchDto, int depth = 1)
        {
            return (Project)_Hydrator.Hydrate(
                projectPatchDto, typeof(Project), depth
            );
        }
    }
}