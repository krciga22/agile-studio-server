
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class SprintSummaryDtoHydrator : AbstractDtoHydrator
    {
        public SprintSummaryDto Hydrate(Application.Models.Sprint model, SprintSummaryDto? dto = null)
        {
            dto ??= new SprintSummaryDto(model.ID, model.SprintNumber);

            dto.ID = model.ID;
            dto.SprintNumber = model.SprintNumber;

            return dto;
        }
    }
}
