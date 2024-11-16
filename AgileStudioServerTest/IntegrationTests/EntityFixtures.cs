using AgileStudioServer.Data;
using AgileStudioServer.Data.Entities;

namespace AgileStudioServerTest.IntegrationTests
{
    /// <summary>
    /// Service to help create test data.
    /// </summary>
    public class EntityFixtures
    {
        private readonly DBContext _DBContext;

        public EntityFixtures(DBContext _dbContext)
        {
            _DBContext = _dbContext;
        }

        public Project CreateProject(
            string? title = null, 
            BacklogItemTypeSchema? backlogItemTypeSchema = null,
            User? createdBy = null)
        {
            title ??= "Test Project";
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            createdBy ??= CreateUser();

            var project = new Project(title, backlogItemTypeSchema.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.Project.Add(project);
            _DBContext.SaveChanges();
            return project;
        }

        public BacklogItem CreateBacklogItem(
            string? title = null,
            User? createdBy = null, 
            Project? project = null, 
            BacklogItemType? backlogItemType = null,
            WorkflowState? workflowState = null,
            Sprint? sprint = null,
            Release? release = null,
            BacklogItem? parentBacklogItem = null)
        {
            title ??= "Test BacklogItem";
            project ??= CreateProject();
            backlogItemType ??= CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            workflowState ??= CreateWorkflowState();

            var backlogItem = new BacklogItem(
                title, project.ID, backlogItemType.ID, workflowState.ID);

            if (createdBy != null)
            {
                backlogItem.CreatedBy = createdBy;
            }

            if (sprint != null)
            {
                backlogItem.Sprint = sprint;
            }

            if (release != null)
            {
                backlogItem.Release = release;
            }

            if (parentBacklogItem != null)
            {
                backlogItem.ParentBacklogItem = parentBacklogItem;
            }

            _DBContext.BacklogItem.Add(backlogItem);
            _DBContext.SaveChanges();
            return backlogItem;
        }

        public BacklogItemTypeSchema CreateBacklogItemTypeSchema(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test BacklogItemTypeSchema";
            createdBy ??= CreateUser();

            var backlogItemTypeSchema = new BacklogItemTypeSchema(title)
            {
                CreatedBy = createdBy
            };
            _DBContext.BacklogItemTypeSchema.Add(backlogItemTypeSchema);
            _DBContext.SaveChanges();
            return backlogItemTypeSchema;
        }

        public BacklogItemType CreateBacklogItemType(
            string? title = null,
            User? createdBy = null,
            BacklogItemTypeSchema? backlogItemTypeSchema = null,
            Workflow? workflow = null)
        {
            title ??= "Test BacklogItemType";
            createdBy ??= CreateUser();
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            workflow ??= CreateWorkflow();

            var backlogItemType = new BacklogItemType(title, backlogItemTypeSchema.ID, workflow.ID)
            {
                CreatedBy = createdBy,
            };
            _DBContext.BacklogItemType.Add(backlogItemType);
            _DBContext.SaveChanges();
            return backlogItemType;
        }

        public ChildBacklogItemType CreateChildBacklogItemType(
            BacklogItemType? parentType = null,
            BacklogItemType? childType = null,
            BacklogItemTypeSchema? schema = null,
            User? createdBy = null)
        {
            schema ??= CreateBacklogItemTypeSchema();
            parentType ??= CreateBacklogItemType("Story", backlogItemTypeSchema: schema);
            childType ??= CreateBacklogItemType("Task", backlogItemTypeSchema: schema);
            createdBy ??= CreateUser();

            var childBacklogItemType = new ChildBacklogItemType(childType.ID, parentType.ID, schema.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.ChildBacklogItemType.Add(childBacklogItemType);
            _DBContext.SaveChanges();
            return childBacklogItemType;
        }

        public Sprint CreateSprint(
            int? sprintNumber = null,
            Project? project = null,
            User? createdBy = null)
        {
            int nextSprintNumber = sprintNumber ?? 1;
            project ??= CreateProject();
            createdBy ??= CreateUser();

            var sprint = new Sprint(nextSprintNumber, project.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.Sprint.Add(sprint);
            _DBContext.SaveChanges();
            return sprint;
        }

        public Release CreateRelease(
            string? title = null,
            Project? project = null,
            User? createdBy = null)
        {
            title ??= "v1.0.0";
            project ??= CreateProject();
            createdBy ??= CreateUser();

            var release = new Release(title, project.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.Release.Add(release);
            _DBContext.SaveChanges();
            return release;
        }

        public Workflow CreateWorkflow(
            string? title = null,
            User? createdBy = null)
        {
            title ??= "Test Workflow";
            createdBy ??= CreateUser();

            var workflow = new Workflow(title) 
            {
                CreatedBy = createdBy
            };
            _DBContext.Workflow.Add(workflow);
            _DBContext.SaveChanges();
            return workflow;
        }

        public WorkflowState CreateWorkflowState(
            string? title = null,
            Workflow? workflow = null,
            User? createdBy = null)
        {
            title ??= "Test Workflow";
            workflow ??= CreateWorkflow();
            createdBy ??= CreateUser();

            var workflowState = new WorkflowState(title, workflow.ID)
            {
                CreatedBy = createdBy
            };
            _DBContext.WorkflowState.Add(workflowState);
            _DBContext.SaveChanges();
            return workflowState;
        }

        public User CreateUser(
            string? email = null,
            string? firstName = null,
            string? lastName = null)
        {
            firstName ??= "Test";
            lastName ??= "User";
            email ??= "testuser@local.agilestudio.dev";

            var user = new User(email, firstName, lastName);
            _DBContext.User.Add(user);
            _DBContext.SaveChanges();
            return user;
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
