using Application.Authentication.Commands;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace QuickPostApi.Controllers
{
    /// <summary>
    /// Authentication controller
    /// </summary>
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly ILogger<AuthenticationController> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        public AuthenticationController(ILogger<AuthenticationController> logger) => _logger = logger;

        /// <summary>
        /// Authentication process
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate(LoginModel model)
        {
            return Ok(await Mediator.Send(new LoginCommand { model = model }));
        }
    }
}
