using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IQuickpostDbContext
    {
        DbSet<Follower> Followers { get; set; }

        DbSet<Like> Likes { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
