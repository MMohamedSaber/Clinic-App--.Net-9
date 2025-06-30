
using Clinic.Core.DTOs;
using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces
{
    public interface IAuthRepository
    {

        Task<string> RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO loginDto);
        Task<bool> ActiveAccount(ActiveAccountDTO accountDto);
        Task SendEmail(string Email, string code, string componant, string subject, string message);
        Task<bool> SendEmailForForgetPassword(string email);
        Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO);
      
    }
}
