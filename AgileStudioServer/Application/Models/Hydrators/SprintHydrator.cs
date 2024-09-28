
using AgileStudioServer.Application.Exceptions;
using AgileStudioServer.Application.Services;

namespace AgileStudioServer.Application.Models.Hydrators
{
    public class SprintHydrator : AbstractModelHydrator
    {
        private SprintService _sprintService;
        private ProjectService _projectService;
        private ProjectHydrator _projectHydrator;
        private UserHydrator _userHydrator;

        public SprintHydrator(
            SprintService sprintService,
            ProjectService projectService,
            ProjectHydrator projectHydrator,
            UserHydrator userHydrator)
        {
            _sprintService = sprintService;
            _projectService = projectService;
            _projectHydrator = projectHydrator;
            _userHydrator = userHydrator;
        }

        public Sprint Hydrate(Data.Entities.Sprint entity, Sprint? model = null)
        {
            if (model == null)
            {
                if (entity.ID > 0)
                {
                    model = _sprintService.Get(entity.ID) ??
                       throw new ModelNotFoundException(
                           nameof(Sprint), entity.ID.ToString()
                       );
                }
                else
                {
                    model = new Sprint(entity.SprintNumber);
                }
            }

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
            if (model == null)
            {
                var nextSprintNumber = _sprintService.GetNextSprintNumber();
                model = new Sprint(nextSprintNumber);
            }

            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;
            
            model.Project = _projectService.Get(dto.ProjectId) ??
                throw new ModelNotFoundException(
                    nameof(Project), dto.ProjectId.ToString()
                );

            return model;
        }

        public Sprint Hydrate(API.DtosNew.SprintPatchDto dto, Sprint? model = null)
        {
            model ??= _sprintService.Get(dto.ID) ??
                throw new ModelNotFoundException(
                    nameof(Sprint), dto.ID.ToString()
                );

            model.Description = dto.Description;
            model.StartDate = dto.StartDate;
            model.EndDate = dto.EndDate;

            return model;
        }
    }
}
