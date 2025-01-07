using DBGenerator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
