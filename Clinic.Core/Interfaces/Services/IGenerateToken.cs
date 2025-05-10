
using Clinic.Core.Entities;

namespace Clinic.Core.Interfaces.Services
{
    public interface IGenerateToken
    {
        string GetAndCreateToken(AppUser appUser);
    }
}
