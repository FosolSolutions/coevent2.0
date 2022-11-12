namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class AccountConfiguration : CommonColumnsConfiguration<Account, int>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<Account> builder)
  {
    builder.ToTable("Account");
    builder.Property(m => m.AccountType).IsRequired();
    builder.Property(m => m.OwnerId).IsRequired();

    builder.HasOne(m => m.Owner).WithMany(m => m.OwnedAccounts).HasForeignKey(m => m.OwnerId).OnDelete(DeleteBehavior.Cascade);

    builder.HasIndex(m => new { m.AccountType, m.OwnerId, m.Name });

    base.Configure(builder);
  }
}
