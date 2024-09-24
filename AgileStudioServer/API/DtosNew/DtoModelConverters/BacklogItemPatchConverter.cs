
using AgileStudioServer.Application.Services;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Exceptions;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class BacklogItemPatchConverter : AbstractDtoModelConverter, IDtoModelConverter<BacklogItemPatchDto, BacklogItem>
    {
        private BacklogItemService _backlogItemService;
        private WorkflowStateService _workflowStateService;
        private SprintService _sprintService;
        private ReleaseService _releaseService;

        public BacklogItemPatchConverter(
            BacklogItemService backlogItemService,
            WorkflowStateService workflowStateService,
            SprintService sprintService,
            ReleaseService releaseService)
        {
            _backlogItemService = backlogItemService;
            _workflowStateService = workflowStateService;
            _sprintService = sprintService;
            _releaseService = releaseService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(BacklogItemPatchDto) &&
                model == typeof(BacklogItem);
        }

        public BacklogItem ConvertToModel(BacklogItemPatchDto dto)
        {
            BacklogItem? model = _backlogItemService.Get(dto.ID);
            if(model == null)
            {
                throw new ModelNotFoundException(
                    nameof(BacklogItem), dto.ID.ToString());
            }

            model.Title = dto.Title;
            model.Description = dto.Description;
            model.WorkflowState = _workflowStateService.Get(dto.WorkflowStateId) ??
                throw new ModelNotFoundException(
                    nameof(WorkflowState), dto.WorkflowStateId.ToString()
                );

            if (dto.SprintId != null)
            {
                int sprintId = (int)dto.SprintId;
                model.Sprint = _sprintService.Get(sprintId) ?? 
                    throw new ModelNotFoundException(
                        nameof(Sprint), sprintId.ToString()
                    );
            }

            if (dto.ReleaseId != null)
            {
                int releaseId = (int)dto.ReleaseId;
                model.Release = _releaseService.Get(releaseId) ??
                    throw new ModelNotFoundException(
                        nameof(Release), releaseId.ToString()
                    );
            }

            return model;
        }

        public BacklogItemPatchDto ConvertToDto(BacklogItem model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
