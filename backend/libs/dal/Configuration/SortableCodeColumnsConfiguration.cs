namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class SortableCodeColumnsConfiguration<TEntity, TKey> : SortableColumnsConfiguration<TEntity, TKey>
  where TEntity : SortableCodeColumns<TKey>
  where TKey : notnull
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<TEntity> builder)
  {
    builder.Property(m => m.Code).IsRequired().HasMaxLength(20);

    base.Configure(builder);
  }
}
