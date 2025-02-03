using Application.Common.Interfaces;
using Domain.Helpers;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Followers.Commands
{
    public class FollowCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
    }
    public class FollowCommandHandler : IRequestHandler<FollowCommand, Result>
    {
        private readonly IQuickpostDbContext _context;
        private readonly IUserService _userService;

        public FollowCommandHandler(IQuickpostDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Result> Handle(FollowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!(await _context.Users.AsNoTracking().AnyAsync(x => x.Id == request.UserId)))
                    return new Result(false, "user not found");
                if (request.UserId == _userService.Id)
                    return new Result(false, "you can't follow yourself");
                var follow = await _context.Followers.FirstOrDefaultAsync(x => x.Followinguserid == request.UserId && x.Userid == _userService.Id, cancellationToken);
                if (follow == null)
                {
                    follow = new Follower
                    {
                        Followinguserid = request.UserId,
                        Userid = _userService.Id,
                        Id = Guid.NewGuid(),
                        Createddate = DateTime.Now,
                        Modifieddate = DateTime.Now
                    };
                    await _context.Followers.AddAsync(follow, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new Result(true, "followed successfully");
                }
                else
                {
                    _context.Followers.Remove(follow);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new Result(true, "unfollowed successfully");
                }
            }
            catch (Exception)
            {
                return new Result(false, "an error occured");
            }
        }
    }
}
