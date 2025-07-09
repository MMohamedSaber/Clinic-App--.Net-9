
using Clinic.Core.Entities.demo.Models;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string Blood_Type { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }

        public Staff Staff { get; set; }
        public Patient Patient { get; set; }
    }
}
