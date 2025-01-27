using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace QuickPostApi.Controllers
{
    /// <summary>
    /// Base controller for all controllers
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        /// <summary>
        /// mediator for handling requests
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new InvalidOperationException("IMediator service not found.");

    }
}
