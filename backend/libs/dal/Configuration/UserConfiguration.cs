namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class UserConfiguration : AuditColumnsConfiguration<User>
{
  public override void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(m => m.Id);
    builder.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
    builder.Property(m => m.Username).IsRequired().HasMaxLength(50);
    builder.Property(m => m.Email).IsRequired().HasMaxLength(250).HasDefaultValueSql("''");
    builder.Property(m => m.Key).IsRequired();
    builder.Property(m => m.DisplayName).IsRequired().HasMaxLength(100).HasDefaultValueSql("''");
    builder.Property(m => m.FirstName).IsRequired().HasMaxLength(100).HasDefaultValueSql("''");
    builder.Property(m => m.LastName).IsRequired().HasMaxLength(100).HasDefaultValueSql("''");
    builder.Property(m => m.IsEnabled);
    builder.Property(m => m.Status).HasDefaultValue(UserStatus.Preapproved);
    builder.Property(m => m.EmailVerified);
    builder.Property(m => m.LastLoginOn);
    builder.Property(m => m.Roles).IsRequired().HasMaxLength(500).HasDefaultValueSql("''");
    builder.Property(m => m.Note).IsRequired().HasMaxLength(2000).HasDefaultValueSql("''");
    builder.Property(m => m.Code).IsRequired().HasMaxLength(10).HasDefaultValueSql("''");


    builder.HasIndex(m => m.Username).IsUnique();
    builder.HasIndex(m => m.Key).IsUnique();
    builder.HasIndex(m => m.Email);

    base.Configure(builder);
  }
}
