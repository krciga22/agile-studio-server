
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class ReleaseSummaryDtoHydrator : AbstractDtoHydrator
    {
        public ReleaseSummaryDto Hydrate(Application.Models.Release model, ReleaseSummaryDto? dto = null)
        {
            dto ??= new ReleaseSummaryDto(model.ID, model.Title);

            dto.ID = model.ID;
            dto.Title = model.Title;

            return dto;
        }
    }
}
