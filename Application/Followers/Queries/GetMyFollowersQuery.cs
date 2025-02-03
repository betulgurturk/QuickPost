using Application.Common.Interfaces;
using Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Followers.Queries
{
    public class GetMyFollowersQuery : IRequest<IQueryable<FollowerDto>>
    {
    }

    public class GetMyFollowersQueryHandler : IRequestHandler<GetMyFollowersQuery, IQueryable<FollowerDto>>
    {
        private readonly IQuickpostDbContext _context;
        private readonly IUserService _userService;
        public GetMyFollowersQueryHandler(IQuickpostDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<IQueryable<FollowerDto>> Handle(GetMyFollowersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var followers = await _context.Followers.Include(x => x.User).Where(x => x.Followinguserid == _userService.Id).Select(x => new FollowerDto
                {
                    Id = x.Userid,
                    FullName = x.User.Firstname + " " + x.User.Lastname,
                    UserName = x.User.Username
                }).ToListAsync(cancellationToken);
                return followers.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
