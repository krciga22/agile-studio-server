
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class BacklogItemTypeSchemaSummaryDtoHydrator : AbstractDtoHydrator
    {
        public BacklogItemTypeSchemaSummaryDto Hydrate(
            Application.Models.BacklogItemTypeSchema model, 
            BacklogItemTypeSchemaSummaryDto? dto = null)
        {
            dto ??= new BacklogItemTypeSchemaSummaryDto(model.ID, model.Title);

            dto.ID = model.ID;
            dto.Title = model.Title;

            return dto;
        }
    }
}
