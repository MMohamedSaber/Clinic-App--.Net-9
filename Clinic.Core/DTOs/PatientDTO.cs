
using Clinic.Core.Attributes;

namespace Clinic.Core.DTOs
{
    public record class PatientDTO
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string DateOfBearth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

    }

    public record class AddPatientDTO
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateOnly BDate { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Blood_Type { get; set; }
        public int NurseId { get; set; }

    }

}
