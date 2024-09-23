
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemTypeSchemaSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemTypeSchemaSummaryDto, Application.Models.BacklogItemTypeSchema>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemTypeSchemaSummaryDto) &&
                model == typeof(Application.Models.BacklogItemTypeSchema);
        }

        public Application.Models.BacklogItemTypeSchema ConvertToModel(BacklogItemTypeSchemaSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public BacklogItemTypeSchemaSummaryDto ConvertToDto(Application.Models.BacklogItemTypeSchema model)
        {
            var dto = new BacklogItemTypeSchemaSummaryDto(model.ID, model.Title);
            return dto;
        }
    }
}
