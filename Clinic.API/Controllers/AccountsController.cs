using Clinic.API.Helper;
using Clinic.Core.DTOs;
using Clinic.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public AccountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterPatientDTO registerDTO)
        {
            var result=await _unitOfWork.PatienRepository.RegisterAsync(registerDTO);

            if (result == "done")
            {

                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result =await _unitOfWork.AuthRepository.LoginAsync(login);

            if (result == null || result.StartsWith("check"))
            {
                return BadRequest(new ResponsApi(400, result));
            }
            Response.Cookies.Append("token", result, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true,
                SameSite = SameSiteMode.Strict

            });

            return Ok(new ResponsApi(200, "done"));

        }
     }
}
