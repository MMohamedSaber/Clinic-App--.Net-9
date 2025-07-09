namespace Clinic.Core.DTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string Blood_Type { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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


    public class ResetPasswordDTO : LoginDTO
    {

        public string Token { get; set; }


    }
}
