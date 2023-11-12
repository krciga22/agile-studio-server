using AgileStudioServer.Models;

namespace AgileStudioServerTest.Models
{
    public class Project
    {
        [Fact]
        public void TestConstructNewProject()
        {
            AgileStudioServer.Models.Project project = new AgileStudioServer.Models.Project("Test Project");
            Assert.Equal("Test Project", project.Title);
        }
    }
}