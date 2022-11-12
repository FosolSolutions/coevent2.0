namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class ScheduleConfiguration : SortableColumnsConfigurationWithoutIndex<Schedule, long>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<Schedule> builder)
  {
    builder.ToTable("Schedule");
    builder.Property(m => m.StartOn).IsRequired();
    builder.Property(m => m.EndOn).IsRequired();
    builder.Property(m => m.AccountId).IsRequired();

    builder.HasOne(m => m.Account).WithMany(m => m.Schedules).HasForeignKey(m => m.AccountId).OnDelete(DeleteBehavior.Restrict);

    base.Configure(builder);
  }
}
