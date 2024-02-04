using AgileStudioServer.Exceptions;
using AgileStudioServer.Models.ApiResources;
using AgileStudioServer.Models.Dtos;
using AgileStudioServer.Models.Entities;

namespace AgileStudioServer.Services.DataProviders
{
    public class SprintDataProvider
    {
        private DBContext _DBContext;

        public SprintDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual List<SprintApiResource> ListForProject(int projectId)
        {
            List<Sprint> sprints = _DBContext.Sprints.Where(sprint => sprint.Project.ID == projectId).ToList();

            List<SprintApiResource> sprintApiResources = new();
            sprints.ForEach(sprint =>
            {
                LoadReferences(sprint);

                sprintApiResources.Add(
                    new SprintApiResource(sprint)
                );
            });

            return sprintApiResources;
        }

        public virtual SprintApiResource? Get(int id)
        {
            Sprint? sprint = _DBContext.Sprints.Find(id);
            if (sprint is null)
            {
                return null;
            }

            LoadReferences(sprint);

            return new SprintApiResource(sprint);
        }

        public virtual SprintApiResource Create(SprintPostDto sprintPostDto)
        {
            var project = _DBContext.Project.Find(sprintPostDto.ProjectId);
            if (project is null)
            {
                throw new EntityNotFoundException(nameof(Project), sprintPostDto.ProjectId.ToString());
            }

            var nextSprintNumber = GetNextSprintNumber();

            var sprint = new Sprint(nextSprintNumber)
            {
                Project = project,
                Description = sprintPostDto.Description,
                StartDate = sprintPostDto.StartDate,
                EndDate = sprintPostDto.EndDate
            };

            _DBContext.Add(sprint);
            _DBContext.SaveChanges();

            return new SprintApiResource(sprint);
        }

        public virtual SprintApiResource? Update(int id, SprintPatchDto sprintPatchDto)
        {
            var sprint = _DBContext.Sprints.Find(id);
            if (sprint is null)
            {
                return null;
            }

            sprint.Description = sprintPatchDto.Description;
            sprint.StartDate = sprintPatchDto.StartDate;
            sprint.EndDate = sprintPatchDto.EndDate;
            _DBContext.SaveChanges();

            LoadReferences(sprint);

            return new SprintApiResource(sprint);
        }

        public virtual bool Delete(int id)
        {
            var sprint = _DBContext.Sprints.Find(id);
            if (sprint is null)
            {
                return false;
            }

            _DBContext.Sprints.Remove(sprint);
            _DBContext.SaveChanges();
            return true;
        }

        private int GetNextSprintNumber()
        {
            return GetLastSprintNumber() + 1;
        }

        private int GetLastSprintNumber()
        {
            var lastSprint = _DBContext.Sprints
                .OrderByDescending(sprint => sprint.SprintNumber)
                .FirstOrDefault();

            return lastSprint?.SprintNumber ?? 0;
        }

        private void LoadReferences(Sprint sprint)
        {
            _DBContext.Entry(sprint).Reference("Project").Load();
        }
    }
}
