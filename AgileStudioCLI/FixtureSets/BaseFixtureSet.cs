using AgileStudioServer.Data;
using AgileStudioServerTest.IntegrationTests;

namespace AgileStudioCLI.FixtureSets
{
    internal class BaseFixtureSet : IFixtureSet
    {
        public void LoadFixtures(DBContext dbContext)
        {
            LoadAgileStudioProject(dbContext);
        }

        private void LoadAgileStudioProject(DBContext dbContext)
        {
            var fixtures = new EntityFixtures(dbContext);

            var user = fixtures.CreateUser();

            var workflow = fixtures.CreateWorkflow(
                title: "Story & Defect Workflow",
                createdBy: user);

            var workflowStateInBacklog = fixtures.CreateWorkflowState(
                title: "In Backlog", 
                workflow: workflow,
                createdBy: user);
            fixtures.CreateWorkflowState(
                title: "In Planning",
                workflow: workflow,
                createdBy: user);
            fixtures.CreateWorkflowState(
                title: "In Development",
                workflow: workflow,
                createdBy: user);
            fixtures.CreateWorkflowState(
                title: "In Testing",
                workflow: workflow,
                createdBy: user);
            fixtures.CreateWorkflowState(
                title: "In Release",
                workflow: workflow,
                createdBy: user);
            fixtures.CreateWorkflowState(
                title: "Cancelled",
                workflow: workflow,
                createdBy: user);

            var taskWorkflow = fixtures.CreateWorkflow(
                title: "Task Workflow",
                createdBy: user);

            var taskWorkflowStateNotStarted = fixtures.CreateWorkflowState(
                title: "Not Started",
                workflow: taskWorkflow,
                createdBy: user);
            fixtures.CreateWorkflowState(
                title: "In Progress",
                workflow: taskWorkflow,
                createdBy: user);
            fixtures.CreateWorkflowState(
                title: "Complete",
                workflow: taskWorkflow,
                createdBy: user);

            var backlogItemTypeSchema = fixtures.CreateBacklogItemTypeSchema(
                title: "Agile Studio Backlog Item Type Schema",
                createdBy: user);

            var backlogItemTypeStory = fixtures.CreateBacklogItemType(
                title: "Story",
                createdBy: user,
                backlogItemTypeSchema: backlogItemTypeSchema,
                workflow: workflow);

            var backlogItemTypeDefect = fixtures.CreateBacklogItemType(
                title: "Defect",
                createdBy: user,
                backlogItemTypeSchema: backlogItemTypeSchema,
                workflow: workflow);

            var backlogItemTypeTask = fixtures.CreateBacklogItemType(
                title: "Task",
                createdBy: user,
                backlogItemTypeSchema: backlogItemTypeSchema,
                workflow: workflow);

            var backlogItemTypeTest = fixtures.CreateBacklogItemType(
                title: "Test",
                createdBy: user,
                backlogItemTypeSchema: backlogItemTypeSchema,
                workflow: workflow);

            fixtures.CreateChildBacklogItemType(
                parentType: backlogItemTypeStory,
                childType: backlogItemTypeTask,
                schema: backlogItemTypeSchema
            );

            fixtures.CreateChildBacklogItemType(
                parentType: backlogItemTypeStory,
                childType: backlogItemTypeTest,
                schema: backlogItemTypeSchema
            );

            fixtures.CreateChildBacklogItemType(
                parentType: backlogItemTypeDefect,
                childType: backlogItemTypeTask,
                schema: backlogItemTypeSchema
            );

            fixtures.CreateChildBacklogItemType(
                parentType: backlogItemTypeDefect,
                childType: backlogItemTypeTest,
                schema: backlogItemTypeSchema
            );

            var project = fixtures.CreateProject(
                title: "Agile Studio", 
                backlogItemTypeSchema: backlogItemTypeSchema,
                createdBy: user);

            var sprint1 = fixtures.CreateSprint(
                sprintNumber: 1, 
                project: project,
                createdBy: user);

            var release1_0_0 = fixtures.CreateRelease(
                title: "1.0.0",
                project: project,
                createdBy: user);

            var testStory = fixtures.CreateBacklogItem(
                title: "Test Story", 
                project: project,
                backlogItemType: backlogItemTypeStory,
                workflowState: workflowStateInBacklog, 
                sprint: sprint1, 
                release: release1_0_0,
                createdBy: user);

            for( var i = 0; i < 5; i++ )
            {
                fixtures.CreateBacklogItem(
                    title: $"Child Task {i}",
                    project: project,
                    backlogItemType: backlogItemTypeTask,
                    workflowState: taskWorkflowStateNotStarted,
                    parentBacklogItem: testStory,
                    createdBy: user);
            }
        }
    }
}
