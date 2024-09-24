
using AgileStudioServer.Application.Services;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Exceptions;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemPostConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemPostDto, BacklogItem>
    {
        private ProjectService _projectService;
        private BacklogItemTypeService _backlogItemTypeService;
        private WorkflowStateService _workflowStateService;
        private SprintService _sprintService;
        private ReleaseService _releaseService;

        public BacklogItemPostConverter(
            ProjectService projectService,
            BacklogItemTypeService backlogItemTypeService,
            WorkflowStateService workflowStateService,
            SprintService sprintService,
            ReleaseService releaseService)
        {
            _projectService = projectService;
            _backlogItemTypeService = backlogItemTypeService;
            _workflowStateService = workflowStateService;
            _sprintService = sprintService;
            _releaseService = releaseService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemPostDto) &&
                model == typeof(BacklogItem);
        }

        public BacklogItem ConvertToModel(BacklogItemPostDto dto)
        {
            Sprint? sprint = null;
            if (dto.SprintId != null)
            {
                int sprintId = (int)dto.SprintId;
                sprint = _sprintService.Get(sprintId) ??
                    throw new ModelNotFoundException(
                        nameof(Project), sprintId.ToString()
                    );
            }

            Release? release = null;
            if (dto.ReleaseId != null)
            {
                int releaseId = (int)dto.ReleaseId;
                release = _releaseService.Get((int) dto.ReleaseId) ??
                    throw new ModelNotFoundException(
                        nameof(Release), releaseId.ToString()
                    );
            }

            BacklogItem model = new(dto.Title) {
                Description = dto.Description,
                Project = _projectService.Get(dto.ProjectId) ??
                    throw new ModelNotFoundException(
                        nameof(Project), dto.ProjectId.ToString()
                    ),
                BacklogItemType = _backlogItemTypeService.Get(dto.BacklogItemTypeId) ??
                    throw new ModelNotFoundException(
                        nameof(BacklogItemType), dto.BacklogItemTypeId.ToString()
                    ),
                WorkflowState = _workflowStateService.Get(dto.WorkflowStateId) ??
                    throw new ModelNotFoundException(
                        nameof(WorkflowState), dto.WorkflowStateId.ToString()
                    ),
                Sprint = sprint,
                Release = release
            };

            return model;
        }

        public BacklogItemPostDto ConvertToDto(BacklogItem model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
