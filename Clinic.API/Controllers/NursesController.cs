using Clinic.API.Helper;
using Clinic.Core.DTOs;
using Clinic.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    [Authorize]
    public class NursesController : BaseController
    {
        public NursesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllNurses()
        {
            var nurses = await _unitOfWork.NurseRepository.GetAllAsync();
            if (nurses == null || !nurses.Any())
                return NotFound(new ResponseApi(404, "No nurses found."));

            return Ok(nurses);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNurse(NurseRequest nurseDto)
        {
            var result = await _unitOfWork.NurseRepository.AddAsync(nurseDto);
            if (result)
                return Ok(new ResponseApi(200, "Nurse added successfully."));
            else
                return BadRequest(new ResponseApi(400, " added failed."));

        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateNurse(int id, UpdateNurseDto nurseDto)
        {
            var result = await _unitOfWork.NurseRepository.UpdateAsync(id, nurseDto);

            if (result)
                return Ok(new ResponseApi(200, "Nurse updated successfully."));
            else
                return BadRequest(new ResponseApi(400, "Update failed."));

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNurse(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseApi(400, "Invalid Id"));

            await _unitOfWork.NurseRepository.DeleteAsync(id);
            return Ok(new ResponseApi(200, "Nurse Deleted Successfully"));
        }
    }
}
