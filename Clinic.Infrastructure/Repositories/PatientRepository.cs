
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {

        private readonly UserManager<AppUser> _userManager;



        public PatientRepository(UserManager<AppUser> userManager) 
        {
            _userManager = userManager;
        }



        public async  Task<string> RegisterAsync(RegisterPatientDTO PatientDTO)
        {
            if (PatientDTO is null) 
                return null;

            if ( await _userManager.FindByEmailAsync(PatientDTO.Email) !=  null)
            {
                return "this Email is already Registerd";
            }

            AppUser appUser = new AppUser()
            {
                DisplayName = PatientDTO.FullName,
                UserName = PatientDTO.UserName,
                Gender=PatientDTO.Gender,
                DateOfBirth=PatientDTO.DateOfBearth,
                Email = PatientDTO.Email,
                Address=PatientDTO.Address
            };

            var result = await _userManager.CreateAsync(appUser, PatientDTO.Password);
            if (!result.Succeeded)
            {
                return result.Errors.ToList()[0].Description;
            }

            return "done";

        }
    }
}
