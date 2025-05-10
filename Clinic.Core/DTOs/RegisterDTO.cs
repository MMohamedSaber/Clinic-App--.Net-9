
using System.ComponentModel.DataAnnotations;

namespace Clinic.Core.DTOs
{
    public class  User
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public string DateOfBearth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }


    public class RegisterPatientDTO : User
    {
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    } 

    public class ActiveAccountDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }

}
