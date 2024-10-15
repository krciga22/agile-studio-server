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
    public class BacklogItemTypeSchemaController : ControllerBase
    {
        private readonly BacklogItemTypeSchemaService _BacklogItemTypeSchemaService;

        private readonly BacklogItemTypeService _BacklogItemTypeService;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeSchemaController(
            BacklogItemTypeSchemaService dataProvider,
            BacklogItemTypeService backlogItemTypeDataProvider,
            Hydrator hydrator)
        {
            _BacklogItemTypeSchemaService = dataProvider;
            _BacklogItemTypeService = backlogItemTypeDataProvider;
            _Hydrator = hydrator;
        }

        [HttpGet(Name = "GetBacklogItemTypeSchemas")]
        [ProducesResponseType(typeof(List<BacklogItemTypeSchemaDto>), StatusCodes.Status200OK)]
        public IActionResult List()
        {
            var models = _BacklogItemTypeSchemaService.GetAll();
            var dtos = HydrateBacklogItemTypeSchemaDtos(models);
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetBacklogItemTypeSchema")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _BacklogItemTypeSchemaService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateBacklogItemTypeSchemaDto(model);
            return Ok(dto);
        }

        [HttpGet("{id}/BacklogItemTypes", Name = "ListBacklogItemTypeSchema_BacklogItemTypes")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemTypeSummaryDto>), StatusCodes.Status200OK)]
        public IActionResult ListBacklogItemTypes(int id)
        {
            var models = _BacklogItemTypeService.GetByBacklogItemTypeSchemaId(id);
            var dtos = HydrateBacklogItemTypeSummaryDtos(models);
            return Ok(dtos);
        }

        [HttpPost(Name = "CreateBacklogItemTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemTypeSchemaPostDto backlogItemTypeSchemaPostDto)
        {
            BacklogItemTypeSchema model = HydrateBacklogItemTypeSchemaModel(
                backlogItemTypeSchemaPostDto
            );
            model = _BacklogItemTypeSchemaService.Create(model);

            string backlogItemTypeSchemaUrl = "";
            if (Url != null)
            {
                backlogItemTypeSchemaUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemTypeSchemaUrl;
            }

            var dto = HydrateBacklogItemTypeSchemaDto(model);

            return Created(backlogItemTypeSchemaUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateBacklogItemTypeSchema")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeSchemaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemTypeSchemaPatchDto backlogItemTypeSchemaPatchDto)
        {
            if (id != backlogItemTypeSchemaPatchDto.ID)
            {
                return BadRequest();
            }

            BacklogItemTypeSchemaDto dto;
            try
            {
                BacklogItemTypeSchema model = HydrateBacklogItemTypeSchemaModel(backlogItemTypeSchemaPatchDto);
                model = _BacklogItemTypeSchemaService.Update(model);
                dto = HydrateBacklogItemTypeSchemaDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemTypeSchema)))
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

        [HttpDelete("{id}", Name = "DeleteBacklogItemTypeSchema")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            BacklogItemTypeSchema? model = _BacklogItemTypeSchemaService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemTypeSchemaService.Delete(model);

            return new OkResult();
        }

        private List<BacklogItemTypeSchemaDto> HydrateBacklogItemTypeSchemaDtos(List<BacklogItemTypeSchema> backlogItemTypeSchemas, int depth = 1)
        {
            List<BacklogItemTypeSchemaDto> dtos = new();

            backlogItemTypeSchemas.ForEach(backlogItemTypeSchema => {
                BacklogItemTypeSchemaDto dto = HydrateBacklogItemTypeSchemaDto(backlogItemTypeSchema, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private BacklogItemTypeSummaryDto HydrateBacklogItemTypeSummaryDto(BacklogItemType backlogItemType, int depth = 1)
        {
            return (BacklogItemTypeSummaryDto)_Hydrator.Hydrate(
                backlogItemType, typeof(BacklogItemTypeSummaryDto), depth
            );
        }

        private List<BacklogItemTypeSummaryDto> HydrateBacklogItemTypeSummaryDtos(List<BacklogItemType> backlogItemTypes, int depth = 1)
        {
            List<BacklogItemTypeSummaryDto> dtos = new();

            backlogItemTypes.ForEach(backlogItemType => {
                BacklogItemTypeSummaryDto dto = HydrateBacklogItemTypeSummaryDto(backlogItemType, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private BacklogItemTypeSchemaDto HydrateBacklogItemTypeSchemaDto(BacklogItemTypeSchema backlogItemTypeSchema, int depth = 1)
        {
            return (BacklogItemTypeSchemaDto)_Hydrator.Hydrate(
                backlogItemTypeSchema, typeof(BacklogItemTypeSchemaDto), depth
            );
        }

        private BacklogItemTypeSchema HydrateBacklogItemTypeSchemaModel(BacklogItemTypeSchemaPostDto backlogItemTypeSchemaPostDto, int depth = 3)
        {
            return (BacklogItemTypeSchema)_Hydrator.Hydrate(
                backlogItemTypeSchemaPostDto, typeof(BacklogItemTypeSchema), depth
            );
        }

        private BacklogItemTypeSchema HydrateBacklogItemTypeSchemaModel(BacklogItemTypeSchemaPatchDto backlogItemTypeSchemaPatchDto, int depth = 3)
        {
            return (BacklogItemTypeSchema)_Hydrator.Hydrate(
                backlogItemTypeSchemaPatchDto, typeof(BacklogItemTypeSchema), depth
            );
        }
    }
}