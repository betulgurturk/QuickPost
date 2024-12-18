using Application.Authentication.Commands;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace QuickPostApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        /// <summary>
        /// Authentication işlemleri
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
