using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace QuickPostApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new InvalidOperationException("IMediator service not found.");

    }
}
