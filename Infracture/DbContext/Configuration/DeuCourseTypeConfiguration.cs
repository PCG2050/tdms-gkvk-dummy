using Domain.Entities.DEU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContext.Configuration
{
    internal class DeuCourseTypeConfiguration : IEntityTypeConfiguration<DeuCourse>
    {
        public void Configure(EntityTypeBuilder<DeuCourse> builder)
        {
            builder
                .Property(c => c.Type)
                .HasConversion<string>();
        }
    }
}
