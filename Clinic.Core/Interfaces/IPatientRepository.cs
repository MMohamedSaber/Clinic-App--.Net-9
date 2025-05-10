
using Clinic.Core.DTOs;
using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces
{
    public interface IPatientRepository 
    {
         Task<string> RegisterAsync(RegisterPatientDTO entity);

    }
}
