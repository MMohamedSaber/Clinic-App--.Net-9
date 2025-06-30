
using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces
{
    public interface INurseRepository
    {
        Task<bool> AddAsync(Nurse nurse);

    }
}
