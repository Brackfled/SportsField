using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AttiributeConfiguration : IEntityTypeConfiguration<Attiribute>
{
    public void Configure(EntityTypeBuilder<Attiribute> builder)
    {
        builder.ToTable("Attiributes").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.Name).HasColumnName("Name").IsRequired();
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(a => a.Name, name:"UK_Attiribute_Name").IsUnique();

        builder.HasMany(a => a.Courts);

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}