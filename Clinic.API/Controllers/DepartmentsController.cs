using Clinic.API.Helper;
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;

namespace Clinic.API.Controllers
{
    public class DepartmentsController : BaseController
    {
        public DepartmentsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpPost("create")]
        public async Task<IActionResult>  create(DepartmentRequest department)
        {

            if (department == null)
                return BadRequest(new ResponseApi(400, "Department Request is null"));

            var isAdded=  await _unitOfWork.DepartmentRepository.AddAsync(department);
               if(!isAdded)
                return BadRequest(new ResponseApi(400,"Saved Failed"));


            return Ok(new ResponseApi(200, "Department Saved Successfuly"));
            
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> delete(int id)
        {
            if (id <= 0)
                return BadRequest(new ResponseApi(400, "Invalid Id"));

            await _unitOfWork.DepartmentRepository.DeleteAsync(id);
            return Ok(new ResponseApi(200, "Deleted Successfully"));

        }



        [HttpPut("update")]
        public async Task<IActionResult> update(int id,DepartmentRequest departmentRequest)
        {
            if (id <= 0)
                return BadRequest(new ResponseApi(400, "Invalid Id"));

            if (string.IsNullOrEmpty(departmentRequest.Phone))
              return BadRequest(new ResponseApi(400, "Invalid phone number"));

            if (string.IsNullOrEmpty(departmentRequest.Name))
              return BadRequest(new ResponseApi(400, "Invalid Name "));

            await _unitOfWork.DepartmentRepository.UpdateAsync(id, departmentRequest);
            return Ok(new ResponseApi(200, "Updated Successfully"));

        }

        [HttpPut("get-all")]
        public async Task<IActionResult> getall()
        {
           var departments=await _unitOfWork.DepartmentRepository.GetAllAsync();

            if (departments == null || !departments.Any())
                return NotFound(new ResponseApi(404, "No Departments Found"));

            return Ok(departments) ;

        }
    }
}
