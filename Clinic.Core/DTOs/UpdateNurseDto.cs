namespace Clinic.Core.DTOs
{
    public record UpdateNurseDto(string Specialization, int Salary, string Role, int No_Of_Hour,
         string Shift, string Qualifications, int dept_id);
}
