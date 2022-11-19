namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class OpeningMessageConfiguration : AuditColumnsConfiguration<OpeningMessage>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<OpeningMessage> builder)
  {
    builder.ToTable("OpeningMessage");
    builder.HasKey(m => m.Id);
    builder.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
    builder.Property(m => m.OpeningId).IsRequired();
    builder.Property(m => m.OwnerId).IsRequired();
    builder.Property(m => m.Message).IsRequired().HasMaxLength(2000).HasDefaultValueSql("''");

    builder.HasOne(m => m.Owner).WithMany(m => m.Messages).HasForeignKey(m => m.OwnerId).OnDelete(DeleteBehavior.Cascade);
    builder.HasOne(m => m.Opening).WithMany(m => m.Messages).HasForeignKey(m => m.OpeningId).OnDelete(DeleteBehavior.Restrict);

    base.Configure(builder);
  }
}
