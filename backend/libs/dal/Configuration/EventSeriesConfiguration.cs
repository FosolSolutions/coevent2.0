namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class EventSeriesConfiguration : SortableColumnsConfigurationWithoutIndex<EventSeries, int>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<EventSeries> builder)
  {
    builder.ToTable("EventSeries");
    builder.Property(m => m.AccountId).IsRequired();

    builder.HasOne(m => m.Account).WithMany(m => m.Series).HasForeignKey(m => m.AccountId).OnDelete(DeleteBehavior.Restrict);

    builder.HasIndex(m => new { m.AccountId, m.Name }).IsUnique();

    base.Configure(builder);
  }
}
