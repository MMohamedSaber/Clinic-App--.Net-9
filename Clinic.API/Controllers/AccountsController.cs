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
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            string result = await _unitOfWork.AuthRepository.RegisterAsync(registerDTO);

            if (result == "done")
            {
                return Ok(new ResponseApi(200, result));
            }
            return BadRequest(new ResponseApi(400, result));
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result = await _unitOfWork.AuthRepository.LoginAsync(login);

            if (result == null || result.StartsWith("check"))
            {
                return BadRequest(new ResponseApi(400, result));
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

            return Ok(new ResponseApi(200, "done"));

        }

        [HttpPost("active-account")]
        public async Task<IActionResult> ActiveAccount(ActiveAccountDTO activeAccount)
        {
            if (string.IsNullOrWhiteSpace(activeAccount.Email) || !activeAccount.Email.Contains("@"))
                return BadRequest("Invalid email");

            var result = await _unitOfWork.AuthRepository.ActiveAccount(activeAccount);
            return result ? Ok(new ResponseApi(200, "Activation done")) : BadRequest(new ResponseApi(400));
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> resetPassword(ResetPasswordDTO reset)
        {

            var result = await _unitOfWork.AuthRepository.ResetPassword(reset);

            if (result == "Password Changed Successfuly")
            {
                return Ok(new ResponseApi(200, result));
            }

            return Ok(new ResponseApi(400, result));



        }

        [HttpGet("send-email-forget-password")]
        public async Task<IActionResult> forget(string email)
        {
            var result = await _unitOfWork.AuthRepository.SendEmailForForgetPassword(email);
            return result ? Ok(new ResponseApi(200)) : BadRequest(new ResponseApi(400));
        }
    }
}
