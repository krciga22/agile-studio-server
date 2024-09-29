
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class ReleaseHydrator : AbstractModelHydrator
    {
        private DBContext _DBContext;
        private ProjectHydrator _projectHydrator;
        private UserHydrator _userHydrator;

        public ReleaseHydrator(
            DBContext dbContext,
            ProjectHydrator projectHydrator,
            UserHydrator userHydrator)
        {
            _DBContext = dbContext;
            _projectHydrator = projectHydrator;
            _userHydrator = userHydrator;
        }

        public Release Hydrate(Data.Entities.Release entity, Release? model = null)
        {
            if (model == null)
            {
                model = new Release(entity.Title);
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

            Data.Entities.Project? projectEntity = _DBContext.Project.Find(dto.ProjectId) ??
                throw new ModelNotFoundException(nameof(Project), dto.ProjectId.ToString());

            model.Project = _projectHydrator.Hydrate(projectEntity);

            return model;
        }

        public Release Hydrate(API.DtosNew.ReleasePatchDto dto, Release? model = null)
        {
            if (model == null)
            {
                Data.Entities.Release? releaseEntity = _DBContext.Release.Find(dto.ID) ??
                    throw new ModelNotFoundException(nameof(Release), dto.ID.ToString());

                model ??= Hydrate(releaseEntity);
            }

            model.Title = dto.Title;
            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;

            return model;
        }
    }
}
