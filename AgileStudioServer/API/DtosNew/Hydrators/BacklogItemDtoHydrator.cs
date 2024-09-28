
namespace AgileStudioServer.API.DtosNew.Hydrators
{
    public class BacklogItemDtoHydrator : AbstractDtoHydrator
    {
        private ProjectSummaryDtoHydrator _projectSummaryDtoHydrator;
        private BacklogItemTypeSummaryDtoHydrator _backlogItemTypeSummaryDtoHydrator;
        private WorkflowStateSummaryDtoHydrator _workflowStateSummaryDtoHydrator;
        private UserSummaryDtoHydrator _userSummaryDtoHydrator;
        private SprintSummaryDtoHydrator _sprintSummaryDtoHydrator;
        private ReleaseSummaryDtoHydrator _releaseSummaryDtoHydrator;

        public BacklogItemDtoHydrator(
            ProjectSummaryDtoHydrator projectSummaryDtoHydrator,
            BacklogItemTypeSummaryDtoHydrator backlogItemTypeSummaryDtoHydrator,
            WorkflowStateSummaryDtoHydrator workflowStateSummaryDtoHydrator,
            UserSummaryDtoHydrator userSummaryDtoHydrator,
            SprintSummaryDtoHydrator sprintSummaryDtoHydrator,
            ReleaseSummaryDtoHydrator releaseSummaryDtoHydrator)
        {
            _projectSummaryDtoHydrator = projectSummaryDtoHydrator;
            _backlogItemTypeSummaryDtoHydrator = backlogItemTypeSummaryDtoHydrator;
            _workflowStateSummaryDtoHydrator = workflowStateSummaryDtoHydrator;
            _userSummaryDtoHydrator = userSummaryDtoHydrator;
            _sprintSummaryDtoHydrator = sprintSummaryDtoHydrator;
            _releaseSummaryDtoHydrator = releaseSummaryDtoHydrator;
        }

        public BacklogItemDto Hydrate(Application.Models.BacklogItem model, BacklogItemDto? dto = null)
        {
            ProjectSummaryDto projectSummaryDto =
                _projectSummaryDtoHydrator.Hydrate(model.Project);

            BacklogItemTypeSummaryDto backlogItemTypeSummaryDto =
                _backlogItemTypeSummaryDtoHydrator.Hydrate(model.BacklogItemType);

            WorkflowStateSummaryDto workflowStateSummaryDto = 
                _workflowStateSummaryDtoHydrator.Hydrate(model.WorkflowState);

            dto ??= new BacklogItemDto(
                model.ID, model.Title, model.CreatedOn, 
                projectSummaryDto, backlogItemTypeSummaryDto, workflowStateSummaryDto
            );

            dto.ID = model.ID;
            dto.Title = model.Title;
            dto.Description = model.Description;
            dto.CreatedOn = model.CreatedOn;

            if (model.CreatedBy != null)
            {
                dto.CreatedBy = _userSummaryDtoHydrator.Hydrate(model.CreatedBy);
            }

            if (model.Sprint != null)
            {
                dto.Sprint = _sprintSummaryDtoHydrator.Hydrate(model.Sprint);
            }

            if (model.Release != null)
            {
                dto.Release = _releaseSummaryDtoHydrator.Hydrate(model.Release);
            }

            return dto;
        }
    }
}
