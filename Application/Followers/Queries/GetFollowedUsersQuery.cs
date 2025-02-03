using Application.Common.Interfaces;
using Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Followers.Queries
{
    public class GetFollowedUsersQuery : IRequest<IQueryable<FollowerDto>>
    {
    }

    public class GetFollowedUsersQueryHandler : IRequestHandler<GetFollowedUsersQuery, IQueryable<FollowerDto>>
    {
        private readonly IQuickpostDbContext _context;
        private readonly IUserService _userService;
        public GetFollowedUsersQueryHandler(IQuickpostDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<IQueryable<FollowerDto>> Handle(GetFollowedUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var followers = await _context.Followers.Include(x => x.Followinguser).Where(x => x.Userid == _userService.Id).Select(x => new FollowerDto
                {
                    Id = x.Followinguserid,
                    FullName = x.Followinguser.Firstname + " " + x.Followinguser.Lastname,
                    UserName = x.Followinguser.Username
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
