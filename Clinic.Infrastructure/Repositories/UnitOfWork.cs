using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Clinic.Core.Interfaces.Services;
using Clinic.Infrastructure.Data;
using Clinic.Infrastructure.Repositories.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Clinic.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthRepository AuthRepository { get; }
        public IPatientRepository PatienRepository { get; }


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IGenerateToken _generateToken;
        private readonly IConfiguration _configuration;

        //  private readonly BaseRepository _baseRepository;
        public UnitOfWork(UserManager<AppUser> userManager,
            AppDbContext context,
            IGenerateToken generateToken,
            IConfiguration configuration,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _generateToken = generateToken;
            _configuration = configuration;
            _signInManager = signInManager;
            AuthRepository = new AuthRepository(_userManager, _context, _generateToken, _signInManager);
            PatienRepository = new PatientRepository(_userManager);
        }

    }
}
