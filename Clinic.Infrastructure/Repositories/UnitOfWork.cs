using AutoMapper;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Clinic.Core.Interfaces.Services;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Clinic.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthRepository AuthRepository { get; }
        public IPatientRepository PatienRepository { get; }
        public INurseRepository NurseRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IGenerateToken _generateToken;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //  private readonly BaseRepository _baseRepository;
        public UnitOfWork(UserManager<AppUser> userManager,
            AppDbContext context,
            IGenerateToken generateToken,
            IConfiguration configuration,
            SignInManager<AppUser> signInManager,
            IEmailService emailService,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _generateToken = generateToken;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            AuthRepository = new AuthRepository(_userManager, _context, _generateToken, _signInManager, _emailService, _mapper);
            PatienRepository = new PatientRepository(_context, mapper);
            NurseRepository = new NurseRepository(_context, mapper, _httpContextAccessor);
            DepartmentRepository = new DepartmentRepository(_context);
        }
    }
}
