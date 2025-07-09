
using Clinic.Core.DTOs;
using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces
{
    public interface INurseRepository : IBaseRepository<Nurse>
    {
        Task<bool> AddAsync(NurseRequest nurse);
        Task<bool> UpdateAsync(int Id, UpdateNurseDto nurse);
    }
}
