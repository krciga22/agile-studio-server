
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class WorkflowStateDtoHydrator : AbstractDtoHydrator
    {
        private WorkflowSummaryDtoHydrator _workflowSummaryDtoHydrator;
        private UserSummaryDtoHydrator _userSummaryDtoHydrator;

        public WorkflowStateDtoHydrator(
            WorkflowSummaryDtoHydrator workflowSummaryDtoHydrator,
            UserSummaryDtoHydrator userSummaryDtoHydrator)
        {
            _workflowSummaryDtoHydrator = workflowSummaryDtoHydrator;
            _userSummaryDtoHydrator = userSummaryDtoHydrator;
        }

        public WorkflowStateDto Hydrate(Application.Models.WorkflowState model, WorkflowStateDto? dto = null)
        {
            WorkflowSummaryDto workflowSummaryDto = _workflowSummaryDtoHydrator.Hydrate(model.Workflow);

            dto ??= new WorkflowStateDto(model.ID, model.Title, workflowSummaryDto, model.CreatedOn);

            dto.ID = model.ID;
            dto.Title = model.Title;
            dto.Description = model.Description;
            dto.CreatedOn = model.CreatedOn;

            if (model.CreatedBy != null)
            {
                dto.CreatedBy = _userSummaryDtoHydrator.Hydrate(model.CreatedBy);
            }

            return dto;
        }
    }
}
