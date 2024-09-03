using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RetentionConfiguration : IEntityTypeConfiguration<Retention>
{
    public void Configure(EntityTypeBuilder<Retention> builder)
    {
        builder.ToTable("Retentions").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(r => r.Name).HasColumnName("Name").IsRequired();
        builder.Property(r => r.Command).HasColumnName("Command").IsRequired();
        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(r => r.Name, name:"UK_Retention_Name").IsUnique();

        builder.HasOne(r => r.User);

        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);
    }
}