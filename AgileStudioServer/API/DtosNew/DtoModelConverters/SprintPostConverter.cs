
using AgileStudioServer.Application.Services;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Exceptions;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class SprintPostConverter : AbstractDtoModelConverter, IDtoModelConverter<SprintPostDto, Sprint>
    {
        private SprintService _sprintService;
        private ProjectService _projectService;

        public SprintPostConverter(
            SprintService sprintService,
            ProjectService projectService)
        {
            _sprintService = sprintService;
            _projectService = projectService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(SprintPostDto) &&
                model == typeof(Sprint);
        }

        public Sprint ConvertToModel(SprintPostDto dto)
        {
            int nextSprintNumber = _sprintService.GetNextSprintNumber();

            Sprint model = new(nextSprintNumber)
            {
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Project = _projectService.Get(dto.ProjectId) ??
                    throw new ModelNotFoundException(
                        nameof(Project), dto.ProjectId.ToString()
                    )
            };

            return model;
        }

        public SprintPostDto ConvertToDto(Sprint model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
