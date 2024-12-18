using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace QuickPostApi.Controllers
{
    public class AuthenticationController : BaseController
    {
        public IActionResult Authenticate(LoginModel model)
        {

            return Ok();
        }
    }
}
