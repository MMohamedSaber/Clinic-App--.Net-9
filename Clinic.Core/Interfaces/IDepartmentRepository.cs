
using Clinic.Core.DTOs;
using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<bool> AddAsync(DepartmentRequest dbtRequest);
        Task<bool> UpdateAsync(int Id,DepartmentRequest dbtRequest);
        Task<IReadOnlyList<DepartmentResponse>> GetAllAsync();
    }
}
