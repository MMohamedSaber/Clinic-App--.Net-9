
using Clinic.Core.Interfaces.Services;

namespace Clinic.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IAuthRepository AuthRepository { get; }
        public IPatientRepository PatienRepository { get; }
        public INurseRepository NurseRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
    }
}
