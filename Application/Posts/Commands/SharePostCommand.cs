using Application.Common.Interfaces;
using Domain.Helpers;
using Domain.Models;
using MediatR;

namespace Application.Posts.Commands
{
    public class SharePostCommand:IRequest<Result>
    {
        public required string Content { get; set; }
    }
    public class SharePostCommandHandler(IUserService userService, IQuickpostDbContext context) : IRequestHandler<SharePostCommand, Result>
    {
        private readonly IUserService _userService = userService;
        private readonly IQuickpostDbContext _context = context;

        public async Task<Result> Handle(SharePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Posts.AddAsync(new Post
                {
                    Content = request.Content,
                    Createddate = DateTime.Now,
                    Modifieddate = DateTime.Now,
                    Userid = _userService.Id,
                    Id = Guid.NewGuid()
                }, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                return new Result(true, "Post shared successfully");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
