using Clinic.API.Helper;
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{

    public class PatientsController : BaseController
    {
        public PatientsController(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    


        [HttpGet("get-all-patients")]
        public async Task<IActionResult> get()
        {
            var Patients = await _unitOfWork.PatienRepository.GetAllAsync(p => p.Nurse.Staff);
            if (Patients is null)
                return NotFound(new ResponseApi(404));

            return Ok(Patients);
        }


        [HttpPost("add-patient")]
        public async Task<IActionResult> add( AddPatientDTO patientDto)
        {
            if (patientDto == null)
            {
                return BadRequest(new ResponseApi(400,  "Patient data is null"));
            }
          var isSaved=  await _unitOfWork.PatienRepository.AddAsync(patientDto);

            if (isSaved)
                return Ok(new ResponseApi(200, "Patient has been added"));
            else
                return BadRequest(new ResponseApi(400, "saved failed"));
        }
        [HttpDelete("delete-patient/{id}")]

             public async Task<IActionResult> delete(int id)
        {

            if (id <= 0)
                return BadRequest(new ResponseApi(400, "Invalid patient ID"));
            
            
            await _unitOfWork.PatienRepository.DeleteAsync(id);
            return Ok(new ResponseApi(200, "Patient has been deleted"));
        }

    } 

}
