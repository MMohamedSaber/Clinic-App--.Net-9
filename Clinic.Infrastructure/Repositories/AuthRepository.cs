
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces.Services;
using Clinic.Core.Shared;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;

namespace Clinic.Core.Interfaces
{
    public  class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IGenerateToken _generateToken;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public AuthRepository(UserManager<AppUser> userManager, AppDbContext context, IGenerateToken generateToken, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _context = context;
            _generateToken = generateToken;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            if (loginDto is null) return null;

            AppUser? findUser = await _userManager.FindByEmailAsync(loginDto.Email);

            // is it have this email
            //if (!findUser.EmailConfirmed)
            //{
            //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
            //   // await SendEmail(findUser.Email, token, "Active", "ActiveMail", "Please Active your email by click on the button");
            //    return "Please confirm your email first ,we have send activate code to your email";
            //}

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

        public async Task<string> RegisterPatientAsync(RegisterPatientDTO registerDTO)
        {
            if (registerDTO == null)
                return null;

            if (await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return "this Email is already Registerd";
            }

            AppUser appUser = new AppUser()
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.FullName,
            };

            var result = await _userManager.CreateAsync(appUser, registerDTO.Password);

            if (!result.Succeeded)
                return string.Join(" | ", result.Errors.Select(e => e.Description));

            // Add Role
            await _userManager.AddToRoleAsync(appUser, "Patient");

            // Add to Patient table
            var patient = new Patient
            {
                UserId = appUser.Id,
                // Add any other Patient-specific data from registerDTO if needed
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return "Patient registered successfully.";

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
    }
}
