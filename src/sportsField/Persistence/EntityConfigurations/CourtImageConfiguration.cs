using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class CourtImageConfiguration : IEntityTypeConfiguration<CourtImage>
{
    public void Configure(EntityTypeBuilder<CourtImage> builder)
    {
        builder.HasOne(ci => ci.Court);
    }
}
