using Clinic.Core.Entities.demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(builder => builder.Id);

            builder.Property(s => s.Id).HasColumnName("Id")
        .ValueGeneratedOnAdd();

        }
    }
}
