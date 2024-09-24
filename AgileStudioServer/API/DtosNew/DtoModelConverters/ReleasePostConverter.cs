
using AgileStudioServer.Application.Services;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Exceptions;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class ReleasePostConverter : AbstractDtoModelConverter, IDtoModelConverter<ReleasePostDto, Release>
    {
        private ProjectService _projectService;

        public ReleasePostConverter(
            ProjectService projectService)
        {
            _projectService = projectService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(ReleasePostDto) &&
                model == typeof(Release);
        }

        public Release ConvertToModel(ReleasePostDto dto)
        {
            Release model = new(dto.Title)
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

        public ReleasePostDto ConvertToDto(Release model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
