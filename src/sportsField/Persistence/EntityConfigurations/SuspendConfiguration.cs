using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SuspendConfiguration : IEntityTypeConfiguration<Suspend>
{
    public void Configure(EntityTypeBuilder<Suspend> builder)
    {
        builder.ToTable("Suspends").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.UserId).HasColumnName("UserId");
        builder.Property(s => s.SuspensionPeriod).HasColumnName("SuspensionPeriod").IsRequired();
        builder.Property(s => s.Reason).HasColumnName("Reason").IsRequired();
        builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(s => s.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(s => s.User);

        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}