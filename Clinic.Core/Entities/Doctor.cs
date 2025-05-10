
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Core.Entities
{
    public class Doctor : BaseEntity<int>
    {
        public string Specialization { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
    }
}
