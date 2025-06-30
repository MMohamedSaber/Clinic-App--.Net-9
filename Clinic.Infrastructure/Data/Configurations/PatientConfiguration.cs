
using System.Reflection.Emit;
using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder.Property(p => p.Id)
      ;
            builder.HasKey(builder => builder.Id);

            builder.Property(builder => builder.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
