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
    public class BacklogItemTypeController : ControllerBase
    {
        private readonly BacklogItemTypeService _BacklogItemTypeService;

        private readonly ChildBacklogItemTypeService _ChildBacklogItemTypeService;

        private readonly Hydrator _Hydrator;

        public BacklogItemTypeController(
            BacklogItemTypeService dataProvider, 
            Hydrator hydrator, 
            ChildBacklogItemTypeService childBacklogItemTypeService)
        {
            _BacklogItemTypeService = dataProvider;
            _Hydrator = hydrator;
            _ChildBacklogItemTypeService = childBacklogItemTypeService;
        }

        [HttpGet("{id}", Name = "GetBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _BacklogItemTypeService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateBacklogItemTypeDto(model);
            return Ok(dto);
        }

        [HttpGet("{id}/ChildTypes", Name = "GetChildTypesForBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<BacklogItemTypeDto>), StatusCodes.Status200OK)]
        public IActionResult GetForParentBacklogItemType(int id)
        {
            var backlogItemType = _BacklogItemTypeService.Get(id);
            if (backlogItemType == null)
            {
                return NotFound();
            }

            List<BacklogItemType> models = new();

            var childBacklogItemTypes = _ChildBacklogItemTypeService.GetByParentTypeId(id);
            childBacklogItemTypes.ForEach(
                childBacklogItemType => models.Add(childBacklogItemType.ChildType)
            );

            var dtos = HydrateBacklogItemTypeDtos(models);
            return Ok(dtos);
        }

        [HttpPost(Name = "CreateBacklogItemType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemTypePostDto backlogItemTypePostDto)
        {
            BacklogItemType model = HydrateBacklogItemTypeModel(backlogItemTypePostDto);
            model = _BacklogItemTypeService.Create(model);

            string backlogItemTypeUrl = "";
            if (Url != null)
            {
                backlogItemTypeUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemTypeUrl;
            }

            var dto = HydrateBacklogItemTypeDto(model);

            return Created(backlogItemTypeUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateBacklogItemType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemTypePatchDto backlogItemTypePatchDto)
        {
            if (id != backlogItemTypePatchDto.ID)
            {
                return BadRequest();
            }

            BacklogItemTypeDto dto;
            try
            {
                BacklogItemType model = HydrateBacklogItemTypeModel(backlogItemTypePatchDto);
                model = _BacklogItemTypeService.Update(model);
                dto = HydrateBacklogItemTypeDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItemType)))
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

        [HttpDelete("{id}", Name = "DeleteBacklogItemType")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            BacklogItemType? model = _BacklogItemTypeService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemTypeService.Delete(model);

            return new OkResult();
        }

        private List<BacklogItemTypeDto> HydrateBacklogItemTypeDtos(List<BacklogItemType> backlogItemTypes, int depth = 1)
        {
            List<BacklogItemTypeDto> dtos = new();

            backlogItemTypes.ForEach(backlogItemType => {
                BacklogItemTypeDto dto = HydrateBacklogItemTypeDto(backlogItemType, depth);
                dtos.Add(dto);
            });

            return dtos;
        }

        private BacklogItemTypeDto HydrateBacklogItemTypeDto(BacklogItemType backlogItemType, int depth = 1)
        {
            return (BacklogItemTypeDto)_Hydrator.Hydrate(
                backlogItemType, typeof(BacklogItemTypeDto), depth
            );
        }

        private BacklogItemType HydrateBacklogItemTypeModel(BacklogItemTypePostDto backlogItemTypePostDto, int depth = 1)
        {
            return (BacklogItemType)_Hydrator.Hydrate(
                backlogItemTypePostDto, typeof(BacklogItemType), depth
            );
        }

        private BacklogItemType HydrateBacklogItemTypeModel(BacklogItemTypePatchDto backlogItemTypePatchDto, int depth = 1)
        {
            return (BacklogItemType)_Hydrator.Hydrate(
                backlogItemTypePatchDto, typeof(BacklogItemType), depth
            );
        }
    }
}