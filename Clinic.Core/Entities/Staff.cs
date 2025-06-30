
namespace Clinic.Core.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    namespace demo.Models
    {
        public class Staff : Person
        {

            public int Salary { get; set; }
            public string Role { get; set; }
            public int No_Of_Hour { get; set; }
            public string Shift { get; set; }
            public string Qualifications { get; set; }

            [ForeignKey("Department")]
            public int dept_id { get; set; }
            public virtual Department Department { get; set; }
            public virtual Doctor Doctor { get; set; }
            public virtual Nurse Nurce { get; set; }

        }
    }
}
