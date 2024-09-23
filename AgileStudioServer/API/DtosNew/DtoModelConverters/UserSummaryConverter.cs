
namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class UserSummaryConverter : AbstractDtoModelConverter, IDtoModelConverter<UserSummaryDto, Application.Models.User>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(UserSummaryDto) &&
                model == typeof(Application.Models.User);
        }

        public Application.Models.User ConvertToModel(UserSummaryDto dto)
        {
            throw new Exception("This Dto doesn't convert to a model");
        }

        public UserSummaryDto ConvertToDto(Application.Models.User model)
        {
            var dto = new UserSummaryDto(model.ID, model.FirstName, model.LastName);
            return dto;
        }
    }
}
