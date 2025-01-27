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

                using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
                {
                    try
                    {
                        var isLike = LikeStatus.Unliked.ToString();
                        var like = await _context.Likes.FirstOrDefaultAsync(x => x.Postid == request.PostId && x.Userid == _userService.Id, cancellationToken);
                        if (like != null)
                        {
                            //if not null remove the like 
                            _context.Likes.Remove(like);
                            post.Likecount--;
                        }
                        else
                        {
                            like = new Like
                            {
                                Postid = request.PostId,
                                Userid = _userService.Id
                            };
                            await _context.Likes.AddAsync(like, cancellationToken);
                            post.Likecount++;
                            isLike = LikeStatus.Liked.ToString();
                        }
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                        return new Result(true, $"post {isLike} successfully");
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return new Result(false, "an error occured on saving process");
                    }
                }
            }
            catch (Exception)
            {
                return new Result(false, "an error occured");
            }
        }
    }

    public enum LikeStatus
    {
        Liked,
        Unliked
    }
}
