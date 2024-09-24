using AgileStudioServer.Application.Models;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemTypeSchemaPostConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemTypeSchemaPostDto, BacklogItemTypeSchema>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemTypeSchemaPostDto) &&
                model == typeof(BacklogItemTypeSchema);
        }

        public BacklogItemTypeSchema ConvertToModel(BacklogItemTypeSchemaPostDto dto)
        {
            BacklogItemTypeSchema model = new(dto.Title) {
                Description = dto.Description
            };

            return model;
        }

        public BacklogItemTypeSchemaPostDto ConvertToDto(BacklogItemTypeSchema model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
