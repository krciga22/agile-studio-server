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
    public class BacklogItemController : ControllerBase
    {
        private readonly BacklogItemService _BacklogItemService;

        private readonly Hydrator _Hydrator;

        public BacklogItemController(BacklogItemService dataProvider, Hydrator hydrator)
        {
            _BacklogItemService = dataProvider;
            _Hydrator = hydrator;
        }

        [HttpGet("{id}", Name = "GetBacklogItem")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BacklogItemDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var model = _BacklogItemService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = HydrateBacklogItemDto(model);
            return Ok(dto);
        }

        [HttpPost(Name = "CreateBacklogItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public CreatedResult Post(BacklogItemPostDto backlogItemPostDto)
        {
            BacklogItem model = HydrateBacklogItemModel(backlogItemPostDto);
            model = _BacklogItemService.Create(model);

            string backlogItemUrl = "";
            if (Url != null)
            {
                backlogItemUrl = Url.Action(nameof(Get), new { id = model.ID }) ?? backlogItemUrl;
            }

            var dto = HydrateBacklogItemDto(model);

            return Created(backlogItemUrl, dto);
        }

        [HttpPatch("{id}", Name = "UpdateBacklogItem")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BacklogItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Patch(int id, BacklogItemPatchDto backlogItemPatchDto)
        {
            if (id != backlogItemPatchDto.ID)
            {
                return BadRequest();
            }

            BacklogItemDto dto;
            try
            {
                BacklogItem model = HydrateBacklogItemModel(backlogItemPatchDto);
                model = _BacklogItemService.Update(model);
                dto = HydrateBacklogItemDto(model);
            }
            catch (ModelNotFoundException e)
            {
                if (e.ModelClassName.Equals(nameof(BacklogItem)))
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

        [HttpDelete("{id}", Name = "DeleteBacklogItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            BacklogItem? model = _BacklogItemService.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            _BacklogItemService.Delete(model);

            return new OkResult();
        }

        private List<BacklogItemDto> HydrateBacklogItemDtos(List<BacklogItem> backlogItems, int depth = 1)
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

        private BacklogItem HydrateBacklogItemModel(BacklogItemPostDto backlogItemPostDto, int depth = 1)
        {
            return (BacklogItem)_Hydrator.Hydrate(
                backlogItemPostDto, typeof(BacklogItem), depth
            );
        }

        private BacklogItem HydrateBacklogItemModel(BacklogItemPatchDto backlogItemPatchDto, int depth = 1)
        {
            return (BacklogItem)_Hydrator.Hydrate(
                backlogItemPatchDto, typeof(BacklogItem), depth
            );
        }
    }
}