
using Clinic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations
{
    public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {

            builder.HasKey(builder => builder.Id);

            builder.HasOne(d => d.Staff)
                    .WithOne(s => s.Nurce)
                    .HasForeignKey<Nurse>(d => d.Id);


        }
    }
}
