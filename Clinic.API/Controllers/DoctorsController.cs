using Clinic.Core.Interfaces;

namespace Clinic.API.Controllers
{
    
    public class DoctorsController : BaseController
    {
        public DoctorsController(IUnitOfWork unitOfWork) : base(unitOfWork) {}
    }
}
