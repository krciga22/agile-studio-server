using AgileStudioServer;
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
            var fixtures = new Fixtures(dbContext);

            var workflow = fixtures.CreateWorkflow("Story & Defect Workflow");
            var workflowStateInBacklog = fixtures.CreateWorkflowState("In Backlog", workflow);
            fixtures.CreateWorkflowState("In Planning", workflow);
            fixtures.CreateWorkflowState("In Development", workflow);
            fixtures.CreateWorkflowState("In Testing", workflow);
            fixtures.CreateWorkflowState("In Release", workflow);
            fixtures.CreateWorkflowState("Cancelled", workflow);

            var backlogItemTypeSchema = fixtures.CreateBacklogItemTypeSchema("Agile Studio Backlog Item Type Schema");
            var backlogItemTypeStory = fixtures.CreateBacklogItemType("Story", backlogItemTypeSchema, workflow);
            fixtures.CreateBacklogItemType("Defect", backlogItemTypeSchema, workflow);

            var project = fixtures.CreateProject("Agile Studio", backlogItemTypeSchema);
            var sprint1 = fixtures.CreateSprint(1, project);
            var release1_0_0 = fixtures.CreateRelease("1.0.0", project);

            fixtures.CreateBacklogItem(
                title: "Test Story", 
                project: project,
                backlogItemType: backlogItemTypeStory,
                workflowState: workflowStateInBacklog, 
                sprint: sprint1, 
                release: release1_0_0);
        }
    }
}
