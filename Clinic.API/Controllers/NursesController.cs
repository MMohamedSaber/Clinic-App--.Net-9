using Clinic.API.Helper;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{

    public class NursesController : BaseController
    {
        public NursesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        [HttpPost("add")]
      
        public async Task<IActionResult> AddNurse( Nurse nurse)
        {
            if (nurse == null)
                return BadRequest(new ResponseApi(400, "Nurse data is null."));
            
            var result = await _unitOfWork.NurseRepository.AddAsync(nurse);
            if (result)
                return Ok( new ResponseApi(200,"Nurse added successfully."));
            else
                return Ok(new ResponseApi(400, " added failed."));

        }

    }
}
