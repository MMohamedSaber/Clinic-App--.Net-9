
using Clinic.Core.DTOs;

namespace Clinic.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailDTO emailDTO);
    }
}
