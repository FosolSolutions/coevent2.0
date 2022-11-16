namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class ClaimConfiguration : SortableColumnsConfigurationWithoutIndex<Claim, int>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<Claim> builder)
  {
    builder.ToTable("Claim");
    builder.Property(m => m.AccountId).IsRequired();

    builder.HasOne(m => m.Account).WithMany(m => m.Claims).HasForeignKey(m => m.AccountId).OnDelete(DeleteBehavior.Restrict);

    builder.HasIndex(m => new { m.AccountId, m.Name }).IsUnique();

    base.Configure(builder);
  }
}
