using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContext.Configuration
{
    internal class OrganizationTypeConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder
                .HasOne(o => o.CreatedBy)
                .WithMany()
                .HasForeignKey(o => o.CreatedById)
                .IsRequired(false);

            builder
                .HasOne(o => o.UpdatedBy)
                .WithMany()
                .HasForeignKey(o => o.UpdatedById)
                .IsRequired(false);
        }
    }
}
