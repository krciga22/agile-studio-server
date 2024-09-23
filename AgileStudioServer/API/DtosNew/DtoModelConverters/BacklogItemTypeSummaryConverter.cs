
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemTypeSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemTypeSummaryDto, Application.Models.BacklogItemType>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemTypeSummaryDto) &&
                model == typeof(Application.Models.BacklogItemType);
        }

        public Application.Models.BacklogItemType ConvertToModel(BacklogItemTypeSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public BacklogItemTypeSummaryDto ConvertToDto(Application.Models.BacklogItemType model)
        {
            var dto = new BacklogItemTypeSummaryDto(model.ID, model.Title, model.CreatedOn);
            return dto;
        }
    }
}
