using AgileStudioServer.API.Dtos;
using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;
using AgileStudioServer.Data.Exceptions;
using AgileStudioServer.Exceptions;

namespace AgileStudioServer.Application.Services.DataProviders
{
    public class SprintDataProvider
    {
        private readonly DBContext _DBContext;

        public SprintDataProvider(DBContext dbContext)
        {
            _DBContext = dbContext;
        }

        public virtual List<SprintDto> ListForProjectId(int projectId)
        {
            List<Sprint> sprints = _DBContext.Sprint.Where(sprint => sprint.Project.ID == projectId).ToList();

            List<SprintDto> sprintApiResources = new();
            sprints.ForEach(sprint =>
            {
                LoadReferences(sprint);

                sprintApiResources.Add(
                    new SprintDto(sprint)
                );
            });

            return sprintApiResources;
        }

        public virtual SprintDto? Get(int id)
        {
            Sprint? sprint = _DBContext.Sprint.Find(id);
            if (sprint is null)
            {
                return null;
            }

            LoadReferences(sprint);

            return new SprintDto(sprint);
        }

        public virtual SprintDto Create(SprintPostDto sprintPostDto)
        {
            var project = _DBContext.Project.Find(sprintPostDto.ProjectId) ??
                throw new EntityNotFoundException(nameof(Project), sprintPostDto.ProjectId.ToString());

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

            return new SprintDto(sprint);
        }

        public virtual SprintDto Update(int id, SprintPatchDto sprintPatchDto)
        {
            var sprint = _DBContext.Sprint.Find(id) ??
                throw new EntityNotFoundException(nameof(Sprint), id.ToString());

            sprint.Description = sprintPatchDto.Description;
            sprint.StartDate = sprintPatchDto.StartDate;
            sprint.EndDate = sprintPatchDto.EndDate;
            _DBContext.SaveChanges();

            LoadReferences(sprint);

            return new SprintDto(sprint);
        }

        public virtual void Delete(int id)
        {
            var sprint = _DBContext.Sprint.Find(id) ??
                throw new EntityNotFoundException(nameof(Sprint), id.ToString());

            _DBContext.Sprint.Remove(sprint);
            _DBContext.SaveChanges();
        }

        private int GetNextSprintNumber()
        {
            return GetLastSprintNumber() + 1;
        }

        private int GetLastSprintNumber()
        {
            var lastSprint = _DBContext.Sprint
                .OrderByDescending(sprint => sprint.SprintNumber)
                .FirstOrDefault();

            return lastSprint?.SprintNumber ?? 0;
        }

        private void LoadReferences(Sprint sprint)
        {
            _DBContext.Entry(sprint).Reference("Project").Load();
            _DBContext.Entry(sprint).Reference("CreatedBy").Load();
        }
    }
}
