namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class ScheduleEventConfiguration : SortableColumnsConfigurationWithoutIndex<ScheduleEvent, long>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<ScheduleEvent> builder)
  {
    builder.ToTable("ScheduleEvent");
    builder.Property(m => m.StartOn).IsRequired();
    builder.Property(m => m.EndOn).IsRequired();
    builder.Property(m => m.ScheduleId).IsRequired();

    builder.HasOne(m => m.Schedule).WithMany(m => m.Events).HasForeignKey(m => m.ScheduleId).OnDelete(DeleteBehavior.Cascade);

    base.Configure(builder);
  }
}
