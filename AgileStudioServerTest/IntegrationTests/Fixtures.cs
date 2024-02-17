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
            title ??= "Test Project";
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();

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
            BacklogItemType? backlogItemType = null,
            Sprint? sprint = null,
            Release? release = null)
        {
            title ??= "Test BacklogItem";
            project ??= CreateProject();
            backlogItemType ??= CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            sprint ??= CreateSprint(project: project);
            release ??= CreateRelease(project: project);

            var backlogItem = new BacklogItem(title)
            {
                Project = project,
                BacklogItemType = backlogItemType,
                Sprint = sprint,
                Release = release
            };
            _DBContext.BacklogItem.Add(backlogItem);
            _DBContext.SaveChanges();
            return backlogItem;
        }

        public BacklogItemTypeSchema CreateBacklogItemTypeSchema(string? title = null)
        {
            title ??= "Test BacklogItemTypeSchema";

            var backlogItemTypeSchema = new BacklogItemTypeSchema(title);
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        public BacklogItemType CreateBacklogItemType(
            string? title = null, 
            BacklogItemTypeSchema? backlogItemTypeSchema = null,
            Workflow? workflow = null)
        {
            title ??= "Test BacklogItemType";
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            workflow ??= CreateWorkflow();

            var backlogItemType = new BacklogItemType(title)
            {
                BacklogItemTypeSchema = backlogItemTypeSchema,
                Workflow = workflow
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
            project ??= CreateProject();

            var sprint = new Sprint(nextSprintNumber)
            {
                Project = project
            };
            _DBContext.Sprint.Add(sprint);
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
            _DBContext.Release.Add(release);
            _DBContext.SaveChanges();
            return release;
        }

        public Workflow CreateWorkflow(
            string? title = null)
        {
            title ??= "Test Workflow";

            var workflow = new Workflow(title);
            _DBContext.Workflow.Add(workflow);
            _DBContext.SaveChanges();
            return workflow;
        }

        public WorkflowState CreateWorkflowState(
            string? title = null,
            Workflow? workflow = null)
        {
            title ??= "Test Workflow";
            workflow ??= CreateWorkflow();

            var workflowState = new WorkflowState(title)
            {
                Workflow = workflow
            };
            _DBContext.WorkflowState.Add(workflowState);
            _DBContext.SaveChanges();
            return workflowState;
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
