using AgileStudioServer.ApiResources;
using AgileStudioServer.DataProviders;
using AgileStudioServer.Dto;
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

        [HttpPost(Name = "CreateProject")]
        public ProjectApiResource Post(ProjectPostDto projectPostDto)
        {
            return _projectDataProvider.CreateProject(projectPostDto);
        }
    }
}