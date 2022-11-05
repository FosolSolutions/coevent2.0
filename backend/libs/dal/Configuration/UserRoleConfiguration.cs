namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public virtual void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.HasKey(m => new { m.UserId, m.RoleId });
    builder.Property(m => m.UserId).IsRequired().ValueGeneratedNever();
    builder.Property(m => m.RoleId).IsRequired().ValueGeneratedNever();

    builder.HasOne(m => m.User).WithMany(m => m.RolesManyToMany).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Cascade);
    builder.HasOne(m => m.Role).WithMany(m => m.UsersManyToMany).HasForeignKey(m => m.RoleId).OnDelete(DeleteBehavior.Cascade);
  }
}