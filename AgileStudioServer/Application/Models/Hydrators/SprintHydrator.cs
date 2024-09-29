
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class SprintHydrator : AbstractModelHydrator
    {
        private DBContext _DBContext;
        private ProjectHydrator _projectHydrator;
        private UserHydrator _userHydrator;

        public SprintHydrator(
            DBContext dbContext,
            ProjectHydrator projectHydrator,
            UserHydrator userHydrator)
        {
            _DBContext = dbContext;
            _projectHydrator = projectHydrator;
            _userHydrator = userHydrator;
        }

        public Sprint Hydrate(Data.Entities.Sprint entity, Sprint? model = null)
        {
            model ??= new Sprint(entity.SprintNumber);

            model.SprintNumber = entity.SprintNumber;
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

        public Sprint Hydrate(API.DtosNew.SprintPostDto dto, Sprint? model = null)
        {
            model ??= new Sprint(0);

            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;

            Data.Entities.Project? projectEntity = _DBContext.Project.Find(dto.ProjectId) ??
                throw new ModelNotFoundException(nameof(Project), dto.ProjectId.ToString());

            model.Project = _projectHydrator.Hydrate(projectEntity);

            return model;
        }

        public Sprint Hydrate(API.DtosNew.SprintPatchDto dto, Sprint? model = null)
        {
            if(model == null)
            {
                Data.Entities.Sprint? sprintEntity = _DBContext.Sprint.Find(dto.ID) ??
                    throw new ModelNotFoundException(nameof(Sprint), dto.ID.ToString());

                model ??= Hydrate(sprintEntity);
            }

            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;

            return model;
        }
    }
}
