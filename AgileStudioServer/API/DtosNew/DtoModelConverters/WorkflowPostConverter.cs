using AgileStudioServer.Application.Models;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class WorkflowPostConverter : AbstractDtoModelConverter, IDtoModelConverter<WorkflowPostDto, Workflow>
    {
        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(WorkflowPostDto) &&
                model == typeof(Workflow);
        }

        public Workflow ConvertToModel(WorkflowPostDto dto)
        {
            Workflow model = new(dto.Title) {
                Description = dto.Description,
            };

            return model;
        }

        public WorkflowPostDto ConvertToDto(Workflow model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
