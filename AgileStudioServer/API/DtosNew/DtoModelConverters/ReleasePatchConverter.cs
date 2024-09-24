using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.API.DtosNew.DtoModelConverters
{
    public class ReleasePatchConverter : AbstractDtoModelConverter, IDtoModelConverter<ReleasePatchDto, Release>
    {
        private ReleaseService _releaseService;

        public ReleasePatchConverter(ReleaseService releaseService)
        {
            _releaseService = releaseService;
        }

        public bool CanConvert(Type dto, Type model)
        {
            return dto == typeof(ReleasePatchDto) &&
                model == typeof(Release);
        }

        public Release ConvertToModel(ReleasePatchDto dto)
        {
            Release? model = _releaseService.Get(dto.ID) ?? 
                throw new ModelNotFoundException(
                    nameof(Release), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;

            return model;
        }

        public ReleasePatchDto ConvertToDto(Release model)
        {
            throw new Exception("This model doesn't convert to a Dto");
        }
    }
}
