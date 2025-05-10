
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Core.Entities
{
    public class Patient : BaseEntity<int>
    {
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
    
        public AppUser User { get; set; }

    }
    
}
