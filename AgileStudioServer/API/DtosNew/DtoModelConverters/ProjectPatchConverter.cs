using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class ProjectPatchConverter : AbstractDtoModelConverter, IDtoModelConverter<ProjectPatchDto, Project>
    {
        private ProjectService _projectService;

        public ProjectPatchConverter(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(ProjectPatchDto) &&
                model == typeof(Project);
        }

        public Project ConvertToModel(ProjectPatchDto dto)
        {
            Project? model = _projectService.Get(dto.ID) ?? 
                throw new ModelNotFoundException(
                    nameof(Project), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;

            return model;
        }

        public ProjectPatchDto ConvertToDto(Project model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
