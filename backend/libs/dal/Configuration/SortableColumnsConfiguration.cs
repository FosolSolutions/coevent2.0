namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class SortableColumnsConfiguration<TEntity, TKey> : CommonColumnsConfiguration<TEntity, TKey>
  where TEntity : SortableColumns<TKey>
  where TKey : notnull
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<TEntity> builder)
  {
    builder.Property(m => m.SortOrder).IsRequired().HasDefaultValue(0);

    base.Configure(builder);
  }
}
