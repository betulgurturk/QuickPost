using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUserListRequest : IRequest<List<UserDto>>
    {
    }

    public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, List<UserDto>>
    {
        private readonly IQuickpostDbContext _context;

        public GetUserListRequestHandler(IQuickpostDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().Select(x => new UserDto { Username = x.Username, Id = x.Id }).ToListAsync(cancellationToken);
        }
    }


    public class UserDto
    {
        public required string Username { get; set; }
        public Guid Id { get; set; }
    }
}
