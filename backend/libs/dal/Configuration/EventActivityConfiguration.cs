namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class EventActivityConfiguration : SortableColumnsConfigurationWithoutIndex<EventActivity, long>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<EventActivity> builder)
  {
    builder.ToTable("EventActivity");
    builder.Property(m => m.StartOn).IsRequired();
    builder.Property(m => m.EndOn).IsRequired();
    builder.Property(m => m.EventId).IsRequired();
    builder.Property(m => m.Format).IsRequired().HasMaxLength(100).HasDefaultValueSql("''");

    builder.HasOne(m => m.Event).WithMany(m => m.Activities).HasForeignKey(m => m.EventId).OnDelete(DeleteBehavior.Cascade);

    base.Configure(builder);
  }
}
