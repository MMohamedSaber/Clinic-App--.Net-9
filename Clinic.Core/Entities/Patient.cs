
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Core.Entities
{
    public class Patient :Person
    {
        public string Blood_Type { get; set; }

        [ForeignKey("Nurse")]
        public int NurseId { get; set; }
        public virtual Nurse Nurse { get; set; }

     //   public virtual ICollection<MedicalRecord> MedicalRecord { get; set; }
     //   public virtual ICollection<Appointment> Appointments { get; set; }

    }
    
}
