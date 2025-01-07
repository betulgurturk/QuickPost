using Application.Common.Interfaces;
using DBGenerator.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public partial class QuickpostdbContext : DbContext, IQuickpostDbContext
    {
        public QuickpostdbContext()
        {
        }

        public QuickpostdbContext(DbContextOptions<QuickpostdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Follower> Followers { get; set; }

        public virtual DbSet<Like> Likes { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuickpostdbContext).Assembly);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

       
    }
}
