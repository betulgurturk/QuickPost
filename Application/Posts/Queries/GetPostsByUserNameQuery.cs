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
    /// <summary>
    /// A query to get all posts by a user
    /// </summary>
    public class GetPostsByUserNameQuery : IRequest<IQueryable<GetPostsByUserDto>>
    {
        /// <summary>
        /// The username of the user whose posts are to be retrieved
        /// </summary>
        public required string UserName { get; set; }
    }
    public class GetPostsByUserNameQueryHandler(IQuickpostDbContext context) : IRequestHandler<GetPostsByUserNameQuery, IQueryable<GetPostsByUserDto>>
    {
        private readonly IQuickpostDbContext _context = context;

        public Task<IQueryable<GetPostsByUserDto>> Handle(GetPostsByUserNameQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Posts.Include(x => x.User).AsNoTracking().Where(x => x.User.Username == request.UserName).Select(x => new GetPostsByUserDto
            {
                Content = x.Content,
                Createddate = x.Createddate,
                Modifieddate = x.Modifieddate,
                UserName = x.User.Username,
                Id = x.Id,
                LikeCount = x.Likecount,
                ViewCount = x.Viewcount
            }));
        }
    }
}
