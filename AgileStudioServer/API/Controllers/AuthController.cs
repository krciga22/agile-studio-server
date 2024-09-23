using AgileStudioServer.API.Dtos;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileStudioServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public IHostEnvironment HostEnvironment { get; }

        public AuthController(IHostEnvironment hostEnvironment)
        {
            HostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Primary authentication endpoint for the web application.
        /// 
        /// Unauthenticated users will be redirected to an Authentication 
        /// Provider to authenticate. Once authenticated, they will 
        /// then be redirected back to the "/" index route or to the route 
        /// they previously requrested and which required authorization.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Login", Name = "AuthLogin")]
        public async Task Login()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        /// <summary>
        /// Get the currently authenticated user's access token (for 
        /// use in the development environment only).
        /// </summary>
        /// <returns>The access token</returns>
        [HttpGet("GetAccessToken", Name = "AuthGetAccessToken")]
        [ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetAccessToken()
        {
            if (!HostEnvironment.IsDevelopment())
            {
                return Forbid();
            }

            var token = await HttpContext.GetTokenAsync(Auth0Constants.AuthenticationScheme, "access_token");
            if (token is null || token == string.Empty)
            {
                return NotFound();
            }

            return Ok(token);
        }
    }
}