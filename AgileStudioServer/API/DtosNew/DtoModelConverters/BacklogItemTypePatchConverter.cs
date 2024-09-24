using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemTypePatchConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemTypePatchDto, BacklogItemType>
    {
        private BacklogItemTypeService _backlogItemTypeService;

        public BacklogItemTypePatchConverter(BacklogItemTypeService backlogItemTypeService)
        {
            _backlogItemTypeService = backlogItemTypeService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemTypePatchDto) &&
                model == typeof(BacklogItemType);
        }

        public BacklogItemType ConvertToModel(BacklogItemTypePatchDto dto)
        {
            BacklogItemType? model = _backlogItemTypeService.Get(dto.ID) ?? 
                throw new ModelNotFoundException(
                    nameof(BacklogItemType), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }

        public BacklogItemTypePatchDto ConvertToDto(BacklogItemType model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
