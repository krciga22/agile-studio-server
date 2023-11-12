using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectDataProvider _projectDataProvider;

        public ProjectsController(ProjectDataProvider projectDataProvider)
        {
            _projectDataProvider = projectDataProvider;
        }

        [HttpGet(Name = "GetProjects")]
        public List<ProjectApiResource> Get()
        {
            return _projectDataProvider.GetProjects();
        }
    }
}