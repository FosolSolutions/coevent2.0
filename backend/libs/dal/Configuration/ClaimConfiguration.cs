namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class ClaimConfiguration : AuditColumnsConfiguration<Claim>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<Claim> builder)
  {
    builder.ToTable("Claim");
    builder.HasKey(m => m.Id);
    builder.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
    builder.Property(m => m.ClaimType).IsRequired().HasMaxLength(50);
    builder.Property(m => m.Value).IsRequired().HasMaxLength(100);
    builder.Property(m => m.AccountId).IsRequired();

    builder.HasOne(m => m.Account).WithMany(m => m.Claims).HasForeignKey(m => m.AccountId).OnDelete(DeleteBehavior.Restrict);

    builder.HasIndex(m => new { m.ClaimType, m.Value }).IsUnique();

    base.Configure(builder);
  }
}
