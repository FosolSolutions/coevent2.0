namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class UserClaimConfiguration : AuditColumnsConfiguration<UserClaim>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<UserClaim> builder)
  {
    builder.ToTable("UserClaim");
    builder.HasKey(m => new { m.UserId, m.AccountId, m.Name, m.Value });
    builder.Property(m => m.UserId).IsRequired().ValueGeneratedNever();
    builder.Property(m => m.AccountId).IsRequired().ValueGeneratedNever();
    builder.Property(m => m.Name).IsRequired().HasMaxLength(50);
    builder.Property(m => m.Value).IsRequired().HasMaxLength(250);

    builder.HasOne(m => m.User).WithMany(m => m.Claims).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);
    builder.HasOne(m => m.Account).WithMany(m => m.UserClaims).HasForeignKey(m => m.AccountId).OnDelete(DeleteBehavior.Restrict);

    base.Configure(builder);
  }
}
