using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class SprintPatchConverter : AbstractDtoModelConverter, IDtoModelConverter<SprintPatchDto, Sprint>
    {
        private SprintService _sprintService;

        public SprintPatchConverter(SprintService sprintService)
        {
            _sprintService = sprintService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(SprintPatchDto) &&
                model == typeof(Sprint);
        }

        public Sprint ConvertToModel(SprintPatchDto dto)
        {
            Sprint? model = _sprintService.Get(dto.ID);
            if(model == null)
            {
                throw new ModelNotFoundException(
                    nameof(Sprint), dto.ID.ToString());
            }

            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;

            return model;
        }

        public SprintPatchDto ConvertToDto(Sprint model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
