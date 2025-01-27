using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces
{
    public interface IQuickpostDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<Follower> Followers { get; set; }

        DbSet<Like> Likes { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
