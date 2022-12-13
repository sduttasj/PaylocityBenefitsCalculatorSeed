using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Api.Models;

namespace Api.Context
{
    public class SampleContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Dependent> Dependent { get; set; }

        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options)
        {

        }

        public SampleContext()
        {
        }

        /*
public SampleContext()
{
}
*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(x => x.Relationship).HasMaxLength(50)
                .HasConversion(new EnumToStringConverter<Relationship>());
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Dependents)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dependent_Employee");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}