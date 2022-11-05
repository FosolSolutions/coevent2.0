namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class ClaimConfiguration : SortableColumnsConfiguration<Claim, int>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<Claim> builder)
  {
    builder.Property(m => m.Key).IsRequired();

    builder.HasIndex(m => m.Key).IsUnique();

    base.Configure(builder);
  }
}
