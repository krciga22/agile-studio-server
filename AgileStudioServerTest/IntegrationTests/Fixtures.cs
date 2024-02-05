using AgileStudioServer;
using AgileStudioServer.Models.Entities;

namespace AgileStudioServerTest.IntegrationTests
{
    /// <summary>
    /// Service to help create test data.
    /// </summary>
    public class Fixtures
    {
        private readonly DBContext _DBContext;

        public Fixtures(DBContext _dbContext)
        {
            _DBContext = _dbContext;
        }

        public Project CreateProject(
            string? title = null, 
            BacklogItemTypeSchema? 
            backlogItemTypeSchema = null)
        {
            if (title is null)
            {
                title = "Test Project";
            }

            if (backlogItemTypeSchema is null)
            {
                backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            }

            var project = new Project(title)
            {
                BacklogItemTypeSchema = backlogItemTypeSchema
            };
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }

        public BacklogItem CreateBacklogItem(
            string? title = null, 
            Project? project = null, 
            BacklogItemType? backlogItemType = null)
        {
            if (title is null)
            {
                title = "Test BacklogItem";
            }

            if (project is null)
            {
                project = CreateProject();
            }

            if (backlogItemType is null)
            {
                backlogItemType = CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            }

            var backlogItem = new BacklogItem(title)
            {
                Project = project,
                BacklogItemType = backlogItemType
            };
            _DBContext.BacklogItem.Add(backlogItem);
            _DBContext.SaveChanges();
            return backlogItem;
        }

        public BacklogItemTypeSchema CreateBacklogItemTypeSchema(string? title = null)
        {
            if(title is null)
            {
                title = "Test BacklogItemTypeSchema";
            }

            var backlogItemTypeSchema = new BacklogItemTypeSchema(title);
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        public BacklogItemType CreateBacklogItemType(
            string? title = null, 
            BacklogItemTypeSchema? backlogItemTypeSchema = null)
        {
            if (title is null)
            {
                title = "Test BacklogItemType";
            }

            if (backlogItemTypeSchema is null)
            {
                backlogItemTypeSchema = CreateBacklogItemTypeSchema();
            }

            var backlogItemType = new BacklogItemType(title)
            {
                BacklogItemTypeSchema = backlogItemTypeSchema
            };
            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }

        public Sprint CreateSprint(
            int? sprintNumber = null,
            Project? project = null)
        {
            int nextSprintNumber = sprintNumber ?? 1;

            if (project is null)
            {
                project = CreateProject();
            }

            var sprint = new Sprint(nextSprintNumber)
            {
                Project = project
            };
            _DBContext.Sprints.Add(sprint);
            _DBContext.SaveChanges();
            return sprint;
        }

        public Release CreateRelease(
            string? title = null,
            Project? project = null)
        {
            title ??= "v1.0.0";
            project ??= CreateProject();

            var release = new Release(title)
            {
                Project = project
            };
            _DBContext.Releases.Add(release);
            _DBContext.SaveChanges();
            return release;
        }

        /// <summary>
        /// Save any changes made to entity fixtures to the database.
        /// </summary>
        public void SaveChanges()
        {
            _DBContext.SaveChanges();
        }
    }
}
