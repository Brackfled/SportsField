using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class SFFileConfiguration : IEntityTypeConfiguration<SFFile>
{
    public void Configure(EntityTypeBuilder<SFFile> builder)
    {

        builder.ToTable("SFFiles").HasKey(sff => sff.Id);

        builder.Property(sff => sff.Id).HasColumnName("Id").IsRequired();
        builder.Property(sff => sff.FileName).HasColumnName("FileName").IsRequired();
        builder.Property(sff => sff.FilePath).HasColumnName("FilePath").IsRequired();
        builder.Property(sff => sff.FileUrl).HasColumnName("FileUrl").IsRequired();
        builder.Property(sff => sff.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sff => sff.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sff => sff.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(sff => sff.FileName, name:"UK_SFFile_FileName").IsUnique();

        builder.HasDiscriminator<string>("FileType")
            .HasValue<SFFile>("SFFile")
            .HasValue<CourtImage>("CourtImage")
            ;
    }
}
