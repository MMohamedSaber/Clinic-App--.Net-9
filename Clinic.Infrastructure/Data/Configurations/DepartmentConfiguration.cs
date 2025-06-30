
using AutoMapper;
using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.ToTable("Departments");
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.Id)
                .ValueGeneratedOnAdd();

            builder.Property(builder => builder.Name)
                .IsRequired()
                .HasMaxLength(150);

        }
    }
}
