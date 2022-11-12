namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class CommonColumnsConfigurationWithoutIndex<TEntity, TKey> : AuditColumnsConfiguration<TEntity>
  where TEntity : CommonColumns<TKey>
  where TKey : notnull
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<TEntity> builder)
  {
    builder.HasKey(m => m.Id);
    builder.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
    builder.Property(m => m.Name).IsRequired().HasMaxLength(50);
    builder.Property(m => m.Description).IsRequired().HasMaxLength(250).HasDefaultValueSql("''");
    builder.Property(m => m.IsEnabled).IsRequired();

    base.Configure(builder);
  }
}
