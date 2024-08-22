using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CourtReservationConfiguration : IEntityTypeConfiguration<CourtReservation>
{
    public void Configure(EntityTypeBuilder<CourtReservation> builder)
    {
        builder.ToTable("CourtReservations").HasKey(cr => cr.Id);

        builder.Property(cr => cr.Id).HasColumnName("Id").IsRequired();
        builder.Property(cr => cr.CourtId).HasColumnName("CourtId").IsRequired();
        builder.Property(cr => cr.UserId).HasColumnName("UserId");
        builder.Property(cr => cr.AvailableDate).HasColumnName("AvailableDate").IsRequired();
        builder.Property(cr => cr.StartTime).HasColumnName("StartTime").IsRequired();
        builder.Property(cr => cr.EndTime).HasColumnName("EndTime").IsRequired();
        builder.Property(cr => cr.CreatedTime).HasColumnName("CreatedTime").IsRequired();
        builder.Property(cr => cr.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(cr => cr.Price).HasColumnName("Price").IsRequired();
        builder.Property(cr => cr.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cr => cr.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cr => cr.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(cr => cr.Court).WithMany(cr => cr.CourtReservations).HasForeignKey(cr => cr.CourtId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(cr => cr.User).WithMany(cr => cr.CourtReservations).HasForeignKey(cr => cr.UserId).OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(cr => !cr.DeletedDate.HasValue);
    }
}