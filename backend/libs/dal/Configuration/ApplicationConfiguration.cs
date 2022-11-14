namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class ApplicationConfiguration : AuditColumnsConfiguration<Application>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<Application> builder)
  {
    builder.ToTable("Application");
    builder.HasKey(m => m.Id);
    builder.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
    builder.Property(m => m.UserId).IsRequired();
    builder.Property(m => m.OpeningId).IsRequired();
    builder.Property(m => m.Message).IsRequired().HasMaxLength(1000).HasDefaultValueSql("''");

    builder.HasOne(m => m.User).WithMany(m => m.Applications).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);
    builder.HasOne(m => m.Opening).WithMany(m => m.Applications).HasForeignKey(m => m.OpeningId).OnDelete(DeleteBehavior.Cascade);

    builder.HasIndex(m => new { m.OpeningId, m.UserId }).IsUnique();

    base.Configure(builder);
  }
}
