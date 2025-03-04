using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries
{
    public class GetPostsByCurrentUserQuery : IRequest<IQueryable<GetPostsByUserDto>>
    {
        public int Page { get; set; }
        public int Take { get; set; }
    }
    public class GetPostsByUserQueryHandler(IQuickpostDbContext context, IUserService userService, TracerProvider tracerProvider, ICacheService cacheService) : IRequestHandler<GetPostsByCurrentUserQuery, IQueryable<GetPostsByUserDto>>
    {
        private readonly IQuickpostDbContext _context = context;
        private readonly IUserService _userService = userService;
        private readonly Tracer _tracer = tracerProvider.GetTracer("MainTracer");
        private readonly ICacheService _cacheService = cacheService;

        public async Task<IQueryable<GetPostsByUserDto>> Handle(GetPostsByCurrentUserQuery request, CancellationToken cancellationToken)
        {

            using var span = _tracer.StartActiveSpan("GetPostsByCurrentUserQuery");
            var postsFromCache = request.Page == 0 ? await _cacheService.GetValueAsync(_userService.Id.ToString()) : null;
            if (postsFromCache != null && request.Page == 0)
            {
                span.SetAttribute("get.currentuser.posts", "redis");
                return JsonConvert.DeserializeObject<List<GetPostsByUserDto>>(postsFromCache).AsQueryable();
            }
            else
            {
                span.SetAttribute("get.currentuser.posts", "postgres");

                if (request.Page == 0)
                {
                    var posts = _context.Posts.AsNoTracking().Where(x => x.Userid == _userService.Id).Select(x => new GetPostsByUserDto
                    {
                        Content = x.Content,
                        Createddate = x.Createddate,
                        Modifieddate = x.Modifieddate,
                        UserName = _userService.UserName,
                        Id = x.Id,
                        LikeCount = x.Likecount,
                        ViewCount = x.Viewcount
                    }).Take(request.Take);
                    await _cacheService.SetValueAsync(_userService.Id.ToString(), JsonConvert.SerializeObject(posts));
                    return posts;
                }
                else
                {
                    var posts = _context.Posts.AsNoTracking().Where(x => x.Userid == _userService.Id).Select(x => new GetPostsByUserDto
                    {
                        Content = x.Content,
                        Createddate = x.Createddate,
                        Modifieddate = x.Modifieddate,
                        UserName = _userService.UserName,
                        Id = x.Id,
                        LikeCount = x.Likecount,
                        ViewCount = x.Viewcount
                    }).Skip(request.Page * request.Take).Take(request.Take);
                    return posts;
                }
            }
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
