namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class RoleConfiguration : SortableColumnsConfiguration<Role, int>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.ToTable("Role");
    builder.Property(m => m.Key).IsRequired();
    builder.Property(m => m.AccountId).IsRequired();

    builder.HasOne(m => m.Account).WithMany(m => m.Roles).HasForeignKey(m => m.AccountId).OnDelete(DeleteBehavior.Restrict);
    builder.HasMany(m => m.Claims).WithMany(m => m.Roles).UsingEntity<RoleClaim>();

    builder.HasIndex(m => m.Key).IsUnique();

    base.Configure(builder);
  }
}
