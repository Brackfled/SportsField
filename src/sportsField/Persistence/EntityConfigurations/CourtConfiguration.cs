using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CourtConfiguration : IEntityTypeConfiguration<Court>
{
    public void Configure(EntityTypeBuilder<Court> builder)
    {
        builder.ToTable("Courts").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name").IsRequired();
        builder.Property(c => c.CourtType).HasColumnName("CourtType").IsRequired();
        builder.Property(c => c.Description).HasColumnName("Description").IsRequired();
        builder.Property(c => c.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(c => c.Lat).HasColumnName("Lat").IsRequired();
        builder.Property(c => c.Lng).HasColumnName("Lng").IsRequired();
        builder.Property(c => c.FormattedAddress).HasColumnName("FormattedAddress").IsRequired();
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression:c => c.Name, name:"UK_Courts_Name").IsUnique();
        builder.HasIndex(indexExpression:c => c.Lat, name:"UK_Courts_Lat").IsUnique();
        builder.HasIndex(indexExpression:c => c.Lng, name:"UK_Courts_Lng").IsUnique();
        builder.HasIndex(indexExpression:c => c.FormattedAddress, name:"UK_Courts_FormattedAddress").IsUnique();

        builder.HasOne(c => c.User);
        builder.HasMany(c => c.CourtImages).WithOne(c => c.Court).HasForeignKey(c => c.CourtId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(c => c.CourtReservations).WithOne(c => c.Court).HasForeignKey(c => c.CourtId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(c => c.Attiributes).WithMany(c => c.Courts)
            .UsingEntity<Dictionary<string, object>>(
                "CourtAttiributes",
                j => j.HasOne<Attiribute>().WithMany().HasForeignKey("AttiributeId"),
                j => j.HasOne<Court>().WithMany().HasForeignKey("CourtId")
            );

        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }
}