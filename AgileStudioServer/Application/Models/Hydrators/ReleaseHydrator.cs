
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class ReleaseHydrator : AbstractModelHydrator
    {
        private ReleaseService _releaseService;
        private ProjectService _projectService;
        private ProjectHydrator _projectHydrator;
        private UserHydrator _userHydrator;

        public ReleaseHydrator(
            ReleaseService releaseService,
            ProjectService projectService,
            ProjectHydrator projectHydrator,
            UserHydrator userHydrator)
        {
            _releaseService = releaseService;
            _projectService = projectService;
            _projectHydrator = projectHydrator;
            _userHydrator = userHydrator;
        }

        public Release Hydrate(Data.Entities.Release entity, Release? model = null)
        {
            if (model == null)
            {
                if (entity.ID > 0)
                {
                    model = _releaseService.Get(entity.ID) ??
                       throw new ModelNotFoundException(
                           nameof(Release), entity.ID.ToString()
                       );
                }
                else
                {
                    model = new Release(entity.Title);
                }
            }

            model.Title = entity.Title;
            model.Description = entity.Description;
            model.CreatedOn = entity.CreatedOn;
            model.StartDate = entity.StartDate;
            model.EndDate = entity.EndDate;

            if (entity.CreatedBy != null)
            {
                model.CreatedBy = _userHydrator.Hydrate(entity.CreatedBy);
            }

            if (entity.Project != null)
            {
                model.Project = _projectHydrator.Hydrate(entity.Project);
            }

            return model;
        }

        public Release Hydrate(API.DtosNew.ReleasePostDto dto, Release? model = null)
        {
            model ??= new Release(dto.Title);

            model.Title = dto.Title;
            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;

            model.Project = _projectService.Get(dto.ProjectId) ??
                throw new ModelNotFoundException(
                    nameof(Project), dto.ProjectId.ToString()
                );

            return model;
        }

        public Release Hydrate(API.DtosNew.ReleasePatchDto dto, Release? model = null)
        {
            model ??= _releaseService.Get(dto.ID) ??
                throw new ModelNotFoundException(
                    nameof(Release), dto.ID.ToString()
                );

            model.Title = dto.Title;
            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;

            return model;
        }
    }
}
