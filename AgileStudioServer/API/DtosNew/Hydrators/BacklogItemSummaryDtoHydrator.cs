
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class BacklogItemSummaryDtoHydrator : AbstractDtoHydrator
    {
        public BacklogItemSummaryDto Hydrate(
            Application.Models.BacklogItem model, 
            BacklogItemSummaryDto? dto = null)
        {
            dto ??= new BacklogItemSummaryDto(model.ID, model.Title);

            dto.ID = model.ID;
            dto.Title = model.Title;

            return dto;
        }
    }
}
