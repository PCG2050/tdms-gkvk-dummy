using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContext.Configuration
{
    internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.CreatedBy) // A User was created by another User
                .WithMany()               // No specific inverse collection for 'Users created by this user'
                .HasForeignKey(u => u.CreatedById)
                .IsRequired(false)        // CreatedById can be null (for the first user)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(u => u.UpdatedBy) // A User was updated by another User
                .WithMany()               // No specific inverse collection for 'Users updated by this user'
                .HasForeignKey(u => u.UpdatedById)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(u => u.Role)
                .HasConversion<string>();
        }
    }
}
