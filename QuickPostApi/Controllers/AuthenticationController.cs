using Application.Authentication.Commands;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;


namespace QuickPostApi.Controllers
{
    /// <summary>
    /// Authentication controller
    /// </summary>
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly Tracer _tracer;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        public AuthenticationController(ILogger<AuthenticationController> logger, TracerProvider tracerProvider)
        {
            _logger = logger;
            _tracer = tracerProvider.GetTracer("MainTracer");
        }

        /// <summary>
        /// Authentication process
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate(LoginModel model)
        {
            using var span = _tracer.StartActiveSpan("PresentationLayer.Authenticate");
            return Ok(await Mediator.Send(new LoginCommand { model = model }));
        }
    }
}
