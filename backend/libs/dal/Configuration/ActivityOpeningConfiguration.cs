namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class ActivityOpeningConfiguration : SortableColumnsConfigurationWithoutIndex<ActivityOpening, long>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<ActivityOpening> builder)
  {
    builder.ToTable("ActivityOpening");
    builder.Property(m => m.Limit).IsRequired();
    builder.Property(m => m.ActivityId).IsRequired();
    builder.Property(m => m.Question).IsRequired().HasMaxLength(1000).HasDefaultValueSql("''");
    builder.Property(m => m.ResponseRequired).IsRequired();

    builder.HasOne(m => m.Activity).WithMany(m => m.Openings).HasForeignKey(m => m.ActivityId).OnDelete(DeleteBehavior.Cascade);

    base.Configure(builder);
  }
}
