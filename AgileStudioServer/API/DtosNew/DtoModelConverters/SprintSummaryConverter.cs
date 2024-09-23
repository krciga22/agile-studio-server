
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class SprintSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<SprintSummaryDto, Application.Models.Sprint>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(SprintSummaryDto) &&
                model == typeof(Application.Models.Sprint);
        }

        public Application.Models.Sprint ConvertToModel(SprintSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public SprintSummaryDto ConvertToDto(Application.Models.Sprint model)
        {
            var dto = new SprintSummaryDto(model.ID, model.SprintNumber);
            return dto;
        }
    }
}
