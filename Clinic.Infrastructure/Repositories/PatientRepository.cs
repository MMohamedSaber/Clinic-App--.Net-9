
using System.Linq.Expressions;
using AutoMapper;
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Clinic.Core.Interfaces.Services;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly IMapper mapper;
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
            _context = context;
        }

        public async Task<IReadOnlyList<PatientResponse>> GetAllAsync(params Expression<Func<Patient, object>>[] Include)
        {
            var query = _context.Patients.AsNoTracking().AsQueryable();

            foreach (var item in Include)
            {
                query = query.Include(item);
            }

            var patinets=await query.ToListAsync();
            var PatientRespons=mapper.Map<IReadOnlyList< PatientResponse>>(patinets);

            return PatientRespons;
        }

        public  async Task<bool> AddAsync(AddPatientDTO patientDto)
        {
           var patient= mapper.Map<Patient>(patientDto);
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
