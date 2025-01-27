using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries
{
    public class GetPostsByCurrentUserQuery : IRequest<IQueryable<GetPostsByUserDto>>
    {
    }
    public class GetPostsByUserQueryHandler(IQuickpostDbContext context, IUserService userService) : IRequestHandler<GetPostsByCurrentUserQuery, IQueryable<GetPostsByUserDto>>
    {
        private readonly IQuickpostDbContext _context = context;
        private readonly IUserService _userService = userService;

        public Task<IQueryable<GetPostsByUserDto>> Handle(GetPostsByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Posts.AsNoTracking().Where(x => x.Userid == _userService.Id).Select(x => new GetPostsByUserDto
            {
                Content = x.Content,
                Createddate = x.Createddate,
                Modifieddate = x.Modifieddate,
                UserName = _userService.UserName,
                Id = x.Id,
                LikeCount = x.Likecount,
                ViewCount = x.Viewcount
            }));
        }
    }
    public class GetPostsByUserDto
    {
        public required string Content { get; set; }
        public required DateTime Createddate { get; set; }
        public required DateTime Modifieddate { get; set; }
        public required string UserName { get; set; }
        public required Guid Id { get; set; }
        public int? LikeCount { get; set; }
        public int? ViewCount { get; set; }
    }
}
