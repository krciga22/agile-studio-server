
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class WorkflowSummaryDtoHydrator : AbstractDtoHydrator
    {
        public WorkflowSummaryDto Hydrate(Application.Models.Workflow model, WorkflowSummaryDto? dto = null)
        {
            dto ??= new WorkflowSummaryDto(model.ID, model.Title);

            dto.ID = model.ID;
            dto.Title = model.Title;

            return dto;
        }
    }
}
