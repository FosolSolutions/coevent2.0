namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

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
    builder.Property(m => m.Key).IsRequired();

    builder.HasMany(m => m.Claims).WithMany(m => m.Roles).UsingEntity<RoleClaim>();

    builder.HasIndex(m => m.Key).IsUnique();

    base.Configure(builder);
  }
}
