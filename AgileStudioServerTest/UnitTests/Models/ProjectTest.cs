using AgileStudioServer.Models;

namespace AgileStudioServerTest.UnitTests.Models
{
    public class ProjectTest
    {
        [Fact]
        public void TestConstructNewProject()
        {
            AgileStudioServer.Models.Project project = new AgileStudioServer.Models.Project("Test Project");
            Assert.Equal("Test Project", project.Title);
        }
    }
}