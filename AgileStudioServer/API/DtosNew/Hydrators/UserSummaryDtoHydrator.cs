
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class UserSummaryDtoHydrator : AbstractDtoHydrator
    {
        public UserSummaryDto Hydrate(Application.Models.User model, UserSummaryDto? dto = null)
        {
            dto ??= new UserSummaryDto(model.ID, model.FirstName, model.LastName);

            dto.ID = model.ID;
            dto.FirstName = model.FirstName;
            dto.LastName = model.LastName;

            return dto;
        }
    }
}
