
using AutoMapper;
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Services;
using Clinic.Core.Shared;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Core.Interfaces
{
    public class AuthRepository : IAuthRepository
    {
        protected readonly UserManager<AppUser> _userManager;
        protected readonly SignInManager<AppUser> _signInManager;
        protected readonly IGenerateToken _generateToken;
        protected readonly AppDbContext _context;
        protected readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AuthRepository(UserManager<AppUser> userManager, AppDbContext context, IGenerateToken generateToken, SignInManager<AppUser> signInManager, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _generateToken = generateToken;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            if (loginDto is null) return null;

            AppUser? findUser = await _userManager.FindByEmailAsync(loginDto.Email);

            // is it have this email
            if (!findUser.EmailConfirmed)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
                await SendEmail(findUser.Email, token, "Active", "ActiveMail", "Please Active your email by click on the button");
                return "Please confirm your email first ,we have send activate code to your email";
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                findUser,
                loginDto.Password,
                true);

            if (result.Succeeded)
            {
                return _generateToken.GetAndCreateToken(findUser);
            }
            return "check your email or password something went wrong.";
        }

        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return null;

            if (await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return "this Email is already Registerd";
            }

            AppUser appUser = _mapper.Map<AppUser>(registerDTO);



            var result = await _userManager.CreateAsync(appUser, registerDTO.Password);

            if (!result.Succeeded)
            {
                return string.Join(" | ", result.Errors.Select(e => e.Description));
            }

            return "done";
        }
        public async Task<bool> ActiveAccount(ActiveAccountDTO accountDto)
        {

            var findUser = await _userManager.FindByEmailAsync(accountDto.Email);

            if (findUser is null)
                return false;

            var decodedToken = Uri.UnescapeDataString(accountDto.Token);
            var result = await _userManager.ConfirmEmailAsync(findUser, decodedToken);


            if (result.Succeeded)
                return true;


            // send email to ,if not active
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
            await SendEmail(findUser.Email, token, "active-account", "ActiveMail", "Please Active your email by click on the button");

            return false;
        }
        public async Task SendEmail(string Email, string code, string componant, string subject, string message)
        {
            var emailDto = new EmailDTO(
               Email,
               "mohamedsabertamer1@gmail.com",
               subject,
               EmailStringBody.Send(Email, code, componant, message)
               );

            await _emailService.SendEmail(emailDto);
        }

        public async Task<bool> SendEmailForForgetPassword(string email)
        {

            var findUser = await _userManager.FindByEmailAsync(email);

            if (findUser is null)
            {
                return false;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(findUser);
            await SendEmail(findUser.Email, token, "reset-password", "resetPassword", " click on the button to reset password");
            return true;
        }
        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var findUser = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);

            if (findUser is null)
            {
                return null;
            }
            var decodedToken = Uri.UnescapeDataString(resetPasswordDTO.Token);

            var result = await _userManager.ResetPasswordAsync(findUser, decodedToken, resetPasswordDTO.Password);

            if (result.Succeeded)
            {
                return "Password Changed Successfuly";
            }

            return result.Errors.ToList()[0].Description;


        }

    }
}
