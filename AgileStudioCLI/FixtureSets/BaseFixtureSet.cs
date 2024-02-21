using AgileStudioServer;
using AgileStudioServerTest.IntegrationTests;

namespace AgileStudioCLI.FixtureSets
{
    internal class BaseFixtureSet : IFixtureSet
    {
        public void LoadFixtures(DBContext dbContext)
        {
            var fixtures = new Fixtures(dbContext);

            var workflow = fixtures.CreateWorkflow("Story & Defect Workflow");
            fixtures.CreateWorkflowState("In Backlog", workflow);
            fixtures.CreateWorkflowState("In Planning", workflow);
            fixtures.CreateWorkflowState("In Development", workflow);
            fixtures.CreateWorkflowState("In Testing", workflow);
            fixtures.CreateWorkflowState("In Release", workflow);
            fixtures.CreateWorkflowState("Cancelled", workflow);

            var backlogItemTypeSchema = fixtures.CreateBacklogItemTypeSchema("Agile Studio Backlog Item Type Schema");
            fixtures.CreateBacklogItemType("Story", backlogItemTypeSchema, workflow);
            fixtures.CreateBacklogItemType("Defect", backlogItemTypeSchema, workflow);

            fixtures.CreateProject("Agile Studio", backlogItemTypeSchema);
        }
    }
}
