using Domain.Entities.ATIC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContext.Configuration
{
    internal class AticSalesTypeConfiguration : IEntityTypeConfiguration<AticSales>
    {
        public void Configure(EntityTypeBuilder<AticSales> builder)
        {
            builder
                .Property(s => s.QuantityType)
                .HasConversion<string>();
        }
    }
}
