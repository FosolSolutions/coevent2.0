namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public virtual void Configure(EntityTypeBuilder<RoleClaim> builder)
  {
    builder.HasKey(m => new { m.RoleId, m.ClaimId });
    builder.Property(m => m.RoleId).IsRequired().ValueGeneratedNever();
    builder.Property(m => m.ClaimId).IsRequired().ValueGeneratedNever();

    builder.HasOne(m => m.Role).WithMany(m => m.ClaimsManyToMany).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Cascade);
    builder.HasOne(m => m.Claim).WithMany(m => m.RolesManyToMany).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Cascade);
  }
}
