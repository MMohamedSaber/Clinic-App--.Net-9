using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Clinic.Core.Entities.demo.Models;

namespace Clinic.Core.Entities
{
    public class Department
    {
       
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Staff> StaffList { get; set; }
    }
}
