using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> entity)
        {
            entity.HasKey(e => e.Id).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .HasColumnName("content");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Likecount)
                .HasDefaultValue(0)
                .HasColumnName("likecount");
            entity.Property(e => e.Modifieddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Viewcount)
                .HasDefaultValue(0)
                .HasColumnName("viewcount");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posts_userid_fkey");
            entity.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
