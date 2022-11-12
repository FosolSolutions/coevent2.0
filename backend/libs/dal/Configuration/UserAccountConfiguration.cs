namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class UserAccountConfiguration : AuditColumnsConfiguration<UserAccount>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<UserAccount> builder)
  {
    builder.ToTable("UserAccount");
    builder.HasKey(m => new { m.UserId, m.AccountId });
    builder.Property(m => m.UserId).IsRequired().ValueGeneratedNever();
    builder.Property(m => m.AccountId).IsRequired().ValueGeneratedNever();

    builder.HasOne(m => m.User).WithMany(m => m.AccountsManyToMany).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
    builder.HasOne(m => m.Account).WithMany(m => m.UsersManyToMany).HasForeignKey(m => m.AccountId).OnDelete(DeleteBehavior.Restrict);

    base.Configure(builder);
  }
}
