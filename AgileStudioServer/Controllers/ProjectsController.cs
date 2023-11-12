using AgileStudioServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace AgileStudioServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly DBContext _db;

        public ProjectsController(DBContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetProjects")]
        public ImmutableList<Project> Get()
        {
            ImmutableList<Project> projects = _db.Projects.ToImmutableList();

            return projects;
        }
    }
}