
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemSummaryDto, Application.Models.BacklogItem>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemSummaryDto) &&
                model == typeof(Application.Models.BacklogItem);
        }

        public Application.Models.BacklogItem ConvertToModel(BacklogItemSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public BacklogItemSummaryDto ConvertToDto(Application.Models.BacklogItem model)
        {
            var dto = new BacklogItemSummaryDto(model.ID, model.Title);
            return dto;
        }
    }
}
