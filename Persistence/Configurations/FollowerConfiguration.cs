using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> entity)
        {
            entity.HasKey(e => e.Id).HasName("followers_pkey");

            entity.ToTable("followers");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Followinguserid).HasColumnName("followinguserid");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Followinguser).WithMany(p => p.FollowerFollowingusers)
                .HasForeignKey(d => d.Followinguserid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_flwuserid");

            entity.HasOne(d => d.User).WithMany(p => p.FollowerUsers)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userid");
        }
    }
}
