
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class WorkflowDtoHydrator : AbstractDtoHydrator
    {
        private UserSummaryDtoHydrator _userSummaryDtoHydrator;

        public WorkflowDtoHydrator(UserSummaryDtoHydrator userSummaryDtoHydrator)
        {
            _userSummaryDtoHydrator = userSummaryDtoHydrator;
        }

        public WorkflowDto Hydrate(Application.Models.Workflow model, WorkflowDto? dto = null)
        {
            dto ??= new WorkflowDto(model.ID, model.Title, model.CreatedOn);

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
