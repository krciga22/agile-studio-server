
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class WorkflowStateSummaryDtoHydrator : AbstractDtoHydrator
    {
        public WorkflowStateSummaryDto Hydrate(
            Application.Models.WorkflowState model, 
            WorkflowStateSummaryDto? dto = null)
        {
            dto ??= new WorkflowStateSummaryDto(model.ID, model.Title);

            dto.ID = model.ID;
            dto.Title = model.Title;

            return dto;
        }
    }
}
