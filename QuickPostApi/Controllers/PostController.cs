using Application.Likes.Commands;
using Application.Posts.Commands;
using Application.Posts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickPostApi.Controllers
{
    /// <summary>
    /// Post Controller
    /// </summary>

    [Route("api/[controller]/[action]")]
    public class PostController : BaseController
    {
        /// <summary>
        /// Get all posts for current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserPosts()
        {
            return Ok(await Mediator.Send(new GetPostsByCurrentUserQuery()));
        }

        /// <summary>
        /// Get all posts by a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPostsByUserName(string userName)
        {
            return Ok(await Mediator.Send(new GetPostsByUserNameQuery { UserName = userName }));
        }

        /// <summary>
        /// Save a post
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SharePost(SharePostCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete a post
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeletePost(DeletePostCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Like a post
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikePost(LikePostCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
