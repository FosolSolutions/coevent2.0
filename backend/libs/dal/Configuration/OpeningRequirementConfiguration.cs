namespace CoEvent.DAL.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class OpeningRequirementConfiguration : AuditColumnsConfiguration<OpeningRequirement>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="builder"></param>
  public override void Configure(EntityTypeBuilder<OpeningRequirement> builder)
  {
    builder.ToTable("OpeningRequirement");
    builder.HasKey(m => new { m.OpeningId, m.Name, m.Value });
    builder.Property(m => m.OpeningId).IsRequired().ValueGeneratedNever();
    builder.Property(m => m.Name).IsRequired().HasMaxLength(50);
    builder.Property(m => m.Value).IsRequired().HasMaxLength(250);

    builder.HasOne(m => m.Opening).WithMany(m => m.Requirements).HasForeignKey(m => m.OpeningId).OnDelete(DeleteBehavior.Cascade);

    base.Configure(builder);
  }
}
