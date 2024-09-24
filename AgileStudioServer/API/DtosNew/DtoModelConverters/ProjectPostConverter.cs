using AgileStudioServer.Application.Services;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Exceptions;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class ProjectPostConverter : AbstractDtoModelConverter, IDtoModelConverter<ProjectPostDto, Project>
    {
        private BacklogItemTypeSchemaService _backlogItemTypeSchemaService;

        public ProjectPostConverter(BacklogItemTypeSchemaService backlogItemTypeSchemaService)
        {
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(ProjectPostDto) &&
                model == typeof(Project);
        }

        public Project ConvertToModel(ProjectPostDto dto)
        {
            Project model = new(dto.Title) {
                Description = dto.Description,
                BacklogItemTypeSchema = _backlogItemTypeSchemaService
                    .Get(dto.BacklogItemTypeSchemaId) ??
                    throw new ModelNotFoundException(
                        nameof(BacklogItemTypeSchema), dto.BacklogItemTypeSchemaId.ToString()
                    )
            };

            return model;
        }

        public ProjectPostDto ConvertToDto(Project model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
