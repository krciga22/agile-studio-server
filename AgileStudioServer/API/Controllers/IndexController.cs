using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        [HttpGet("/", Name = "Index")]
        public IActionResult Login()
        {
            var cm = new ChunkingCookieManager();
            var cookie = cm.GetRequestCookie(HttpContext, ".AspNetCore.Cookies");

            var response = new
            {
                Cookie = cookie,
                User = HttpContext.User.Identity?.Name ?? "Anonymous"
            };

            return Ok(response);
        }
    }
}