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

            builder
                .HasMany(u => u.TrainerAssignments)
                .WithOne(u => u.Trainer)
                .HasForeignKey(u => u.TrainerId);



            builder
               .HasMany(u => u.UnitHeadAssignments)
               .WithOne(u => u.UnitHead)
               .HasForeignKey(u => u.UnitHeadId);

            builder
                .Property(x => x.EmployementType)
                .HasConversion<string>();

            builder
                .Property(u => u.Gender)
                .HasConversion<string>();

            builder
                .Property(u => u.DateOfBirth)
                .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v)
                )
                .HasColumnType("date");

            builder
                .Property(u => u.DateOfJoining)
                .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v)
                )
                .HasColumnType("date");

         
        }
    }
}
