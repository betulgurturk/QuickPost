using Application.Followers.Commands;
using Application.Followers.Queries;
using Application.Users.Commands;
using Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickPostApi.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class UserController : BaseController
    {
        /// <summary>
        /// User listesini getirir
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserList()
        {
            var users = await Mediator.Send(new GetUserListRequest());
            return Ok(users);
        }

        /// <summary>
        /// User kaydetme işlemi
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveUser(SaveUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Follow user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FollowUser(FollowCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get followers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFollowers()
        {
            var result = await Mediator.Send(new GetMyFollowersQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get followed users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFollowedUsers()
        {
            var result = await Mediator.Send(new GetFollowedUsersQuery());
            return Ok(result);
        }
    }
}