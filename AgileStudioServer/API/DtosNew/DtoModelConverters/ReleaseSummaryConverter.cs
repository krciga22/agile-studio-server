
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class ReleaseSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<ReleaseSummaryDto, Application.Models.Release>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(ReleaseSummaryDto) &&
                model == typeof(Application.Models.Release);
        }

        public Application.Models.Release ConvertToModel(ReleaseSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public ReleaseSummaryDto ConvertToDto(Application.Models.Release model)
        {
            var dto = new ReleaseSummaryDto(model.ID, model.Title);
            return dto;
        }
    }
}
