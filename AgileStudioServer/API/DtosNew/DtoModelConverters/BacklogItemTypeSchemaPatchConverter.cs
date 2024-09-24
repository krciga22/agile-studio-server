using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemTypeSchemaPatchConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemTypeSchemaPatchDto, BacklogItemTypeSchema>
    {
        private BacklogItemTypeSchemaService _backlogItemTypeSchemaService;

        public BacklogItemTypeSchemaPatchConverter(BacklogItemTypeSchemaService backlogItemTypeSchemaService)
        {
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemTypeSchemaPatchDto) &&
                model == typeof(BacklogItemTypeSchema);
        }

        public BacklogItemTypeSchema ConvertToModel(BacklogItemTypeSchemaPatchDto dto)
        {
            BacklogItemTypeSchema? model = _backlogItemTypeSchemaService.Get(dto.ID) ?? 
                throw new ModelNotFoundException(
                    nameof(BacklogItemTypeSchema), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }

        public BacklogItemTypeSchemaPatchDto ConvertToDto(BacklogItemTypeSchema model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
