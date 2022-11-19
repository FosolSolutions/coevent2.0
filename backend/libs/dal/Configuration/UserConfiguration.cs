namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class UserConfiguration : AuditColumnsConfiguration<User>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("User");
    builder.HasKey(m => m.Id);
    builder.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
    builder.Property(m => m.Username).IsRequired().HasMaxLength(50);
    builder.Property(m => m.Email).IsRequired().HasMaxLength(250).HasDefaultValueSql("''");
    builder.Property(m => m.Key).IsRequired();
    builder.Property(m => m.DisplayName).IsRequired().HasMaxLength(100).HasDefaultValueSql("''");
    builder.Property(m => m.FirstName).IsRequired().HasMaxLength(100).HasDefaultValueSql("''");
    builder.Property(m => m.LastName).IsRequired().HasMaxLength(100).HasDefaultValueSql("''");
    builder.Property(m => m.Phone).IsRequired().HasMaxLength(20).HasDefaultValueSql("''");
    builder.Property(m => m.IsEnabled);
    builder.Property(m => m.Status).HasDefaultValue(UserStatus.Registered);
    builder.Property(m => m.EmailVerified).IsRequired();
    builder.Property(m => m.EmailVerifiedOn);
    builder.Property(m => m.LastLoginOn);
    builder.Property(m => m.Gender);

    builder.HasMany(m => m.Roles).WithMany(m => m.Users).UsingEntity<UserRole>();
    builder.HasMany(m => m.Accounts).WithMany(m => m.Users).UsingEntity<UserAccount>();

    builder.HasIndex(m => m.Username).IsUnique();
    builder.HasIndex(m => m.Key).IsUnique();
    builder.HasIndex(m => m.Email);

    base.Configure(builder);
  }
}
