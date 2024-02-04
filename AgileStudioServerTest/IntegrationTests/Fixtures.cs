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

        public Project CreateProject(string title = "Test Project")
        {
            var project = new Project(title)
            {
                BacklogItemTypeSchema = CreateBacklogItemTypeSchema()
            };
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }

        public BacklogItem CreateBacklogItem(Project? project = null, BacklogItemType? backlogItemType = null, string title = "Test BacklogItem")
        {
            if (project is null)
            {
                project = CreateProject();
            }

            if (backlogItemType is null)
            {
                backlogItemType = CreateBacklogItemType(project.BacklogItemTypeSchema);
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

        public BacklogItemTypeSchema CreateBacklogItemTypeSchema()
        {
            var backlogItemTypeSchema = new BacklogItemTypeSchema("Test BacklogItemTypeSchema");
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        public BacklogItemType CreateBacklogItemType(BacklogItemTypeSchema backlogItemTypeSchema)
        {
            var backlogItemType = new BacklogItemType("Test BacklogItemType")
            {
                BacklogItemTypeSchema = backlogItemTypeSchema
            };
            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
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
