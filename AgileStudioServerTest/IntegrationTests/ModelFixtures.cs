using AgileStudioServer.Data;
using AgileStudioServer.Application.Models;
using AgileStudioServer.Application.Services;

namespace AgileStudioServerTest.IntegrationTests
{
    /// <summary>
    /// Service to help create test data.
    /// </summary>
    public class ModelFixtures
    {
        private readonly ProjectService _projectService;
        private readonly BacklogItemService _backlogItemService;
        private readonly BacklogItemTypeService _backlogItemTypeService;
        private readonly BacklogItemTypeSchemaService _backlogItemTypeSchemaService;
        private readonly SprintService _sprintService;
        private readonly ReleaseService _releaseService;
        private readonly UserService _userService;
        private readonly WorkflowService _workflowService;
        private readonly WorkflowStateService _workflowStateService;

        public ModelFixtures(
            ProjectService projectService,
            BacklogItemService backlogItemService,
            BacklogItemTypeService backlogItemTypeService,
            BacklogItemTypeSchemaService backlogItemTypeSchemaService,
            SprintService sprintService,
            ReleaseService releaseService,
            UserService userService,
            WorkflowService workflowService,
            WorkflowStateService workflowStateService)
        {
            _projectService = projectService;
            _backlogItemService = backlogItemService;
            _backlogItemTypeService = backlogItemTypeService;
            _backlogItemTypeSchemaService = backlogItemTypeSchemaService;
            _sprintService = sprintService;
            _releaseService = releaseService;
            _userService = userService;
            _workflowService = workflowService;
            _workflowStateService = workflowStateService;
        }

        public Project CreateProject(
            string? title = null, 
            BacklogItemTypeSchema? backlogItemTypeSchema = null,
            User? createdBy = null)
        {
            title ??= "Test Project";
            backlogItemTypeSchema ??= CreateBacklogItemTypeSchema();
            createdBy ??= CreateUser();

            var project = new Project(title)
            {
                BacklogItemTypeSchema = backlogItemTypeSchema,
                CreatedBy = createdBy
            };
            project = _projectService.Create(project);
            return project;
        }

        public BacklogItem CreateBacklogItem(
            string? title = null,
            User? createdBy = null, 
            Project? project = null, 
            BacklogItemType? backlogItemType = null,
            WorkflowState? workflowState = null,
            Sprint? sprint = null,
            Release? release = null)
        {
            title ??= "Test BacklogItem";
            createdBy ??= CreateUser();
            project ??= CreateProject();
            backlogItemType ??= CreateBacklogItemType(
                    backlogItemTypeSchema: project.BacklogItemTypeSchema);
            workflowState ??= CreateWorkflowState();
            sprint ??= CreateSprint(project: project);
            release ??= CreateRelease(project: project);

            var backlogItem = new BacklogItem(title)
            {
                CreatedBy = createdBy,
                Project = project,
                BacklogItemType = backlogItemType,
                WorkflowState = workflowState,
                Sprint = sprint,
                Release = release
            };
            _backlogItemService.Create(backlogItem);
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
            _backlogItemTypeSchemaService.Create(backlogItemTypeSchema);
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

            var backlogItemType = new BacklogItemType(title)
            {
                CreatedBy = createdBy,
                BacklogItemTypeSchema = backlogItemTypeSchema,
                Workflow = workflow
            };
            _backlogItemTypeService.Create(backlogItemType);
            return backlogItemType;
        }

        public Sprint CreateSprint(
            int? sprintNumber = null,
            Project? project = null,
            User? createdBy = null)
        {
            int nextSprintNumber = sprintNumber ?? 1;
            project ??= CreateProject();
            createdBy ??= CreateUser();

            var sprint = new Sprint(nextSprintNumber)
            {
                Project = project,
                CreatedBy = createdBy
            };
            _sprintService.Create(sprint);
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

            var release = new Release(title)
            {
                Project = project,
                CreatedBy = createdBy
            };
            _releaseService.Create(release);
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
            _workflowService.Create(workflow);
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

            var workflowState = new WorkflowState(title)
            {
                Workflow = workflow,
                CreatedBy = createdBy
            };
            _workflowStateService.Create(workflowState);
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
            _userService.Create(user);
            return user;
        }
    }
}
