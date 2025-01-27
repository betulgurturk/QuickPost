using Application.Common.Interfaces;
using Domain.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Commands
{
    public class DeletePostCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result>
    {
        private readonly IQuickpostDbContext _context;
        private readonly IUserService _userService;
        public DeletePostCommandHandler(IQuickpostDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var post = await _context.Posts.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (post == null)
                    return new Result(false, "post not found");
                if (post.IsDeleted)
                    return new Result(false, "post already deleted");
                if (post.User.Id != _userService.Id)
                    return new Result(false, "you are not authorized to delete this post");

                post.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);
                return new Result(true, "post deleted successfully");
            }
            catch (Exception ex)
            {
                return new Result(false, "an error occured");
            }
        }
    }
}
