using Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace QuickPostApi.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        /// <summary>
        /// Gets user list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var users = await Mediator.Send(new GetUserListRequest());
            return Ok(users);
        }
    }
}
