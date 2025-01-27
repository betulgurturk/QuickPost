using Application.Common.Interfaces;
using Domain.Helpers;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Likes.Commands
{
    public class LikePostCommand : IRequest<Result>
    {
        public Guid PostId { get; set; }
    }
    public class LikePostCommandHandler(IQuickpostDbContext context, IUserService userService) : IRequestHandler<LikePostCommand, Result>
    {
        private readonly IQuickpostDbContext _context = context;
        private readonly IUserService _userService = userService;

        public async Task<Result> Handle(LikePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);
                if (post == null)
                    return new Result(false, "post not found");
                if (await _context.Likes.AnyAsync(x => x.Postid == request.PostId && x.Userid == _userService.Id, cancellationToken))
                    return new Result(false, "you have already liked this post");
                var like = new Like
                {
                    Postid = request.PostId,
                    Userid = _userService.Id
                };
                await _context.Likes.AddAsync(like, cancellationToken);
                post.Likecount++;
                await _context.SaveChangesAsync(cancellationToken);
                return new Result(true, "post liked successfully");
            }
            catch (Exception)
            {
                return new Result(false, "an error occured");
            }
        }
    }
}
