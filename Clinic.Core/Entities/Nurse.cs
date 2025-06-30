using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Clinic.Core.Entities.demo.Models;

namespace Clinic.Core.Entities
{
    public class Nurse
    {
        [Key, ForeignKey("Staff")]
        public int Id { get; set; }
        public string Specialization { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }// reponsable about many patients
    }
}
