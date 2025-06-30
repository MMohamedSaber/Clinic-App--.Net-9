
using Clinic.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Core.DTOs
{
    public class PatientResponse
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string NurseName{ get; set; }
        public string Blood_Type { get; set; }
        public string Gender { get; set; }
        public DateOnly BDate { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
    }
}
