
using System.Linq.Expressions;
using Clinic.Core.DTOs;
using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces
{
    public interface IPatientRepository :IBaseRepository<Patient>
    {
        Task<IReadOnlyList<PatientResponse>> GetAllAsync(params Expression<Func<Patient, object>>[] Include);

        Task <bool> AddAsync(AddPatientDTO patientDto);
    }
}
