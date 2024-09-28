
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class BacklogItemTypeSummaryDtoHydrator : AbstractDtoHydrator
    {
        public BacklogItemTypeSummaryDto Hydrate(
            Application.Models.BacklogItemType model,
            BacklogItemTypeSummaryDto? dto = null)
        {
            dto ??= new BacklogItemTypeSummaryDto(model.ID, model.Title, model.CreatedOn);

            dto.ID = model.ID;
            dto.Title = model.Title;
            dto.Description = model.Description;
            dto.CreatedOn = model.CreatedOn;

            return dto;
        }
    }
}
