
using System.Reflection.Emit;
using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(builder => builder.Id);
            

            builder.HasOne(d => d.Staff)
                    .WithOne(s => s.Doctor)
                    .HasForeignKey<Doctor>(d => d.Id);

        }
    }
}
