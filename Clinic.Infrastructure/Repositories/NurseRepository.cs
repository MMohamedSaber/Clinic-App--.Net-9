
using System.Security.Claims;
using AutoMapper;
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Entities.demo.Models;
using Clinic.Core.Interfaces;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories
{
    public class NurseRepository : BaseRepository<Nurse>, INurseRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public NurseRepository(AppDbContext context, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddAsync(NurseRequest nurse)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var staff = _mapper.Map<Staff>(nurse);
                staff.AppUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

                var IsUserExist = await _context.Staff.FirstOrDefaultAsync(u => u.AppUserId == staff.AppUserId);
                if (IsUserExist != null)
                {
                    return false;
                }

                await _context.Staff.AddAsync(staff);
                await _context.SaveChangesAsync();


                var nurseEntity = new Nurse
                {
                    Id = staff.Id,
                    Specialization = nurse.Specialization,
                };

                await _context.Nurses.AddAsync(nurseEntity);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; // Global Handlinge Exception will cach it 
            }
        }

        public async Task<bool> UpdateAsync(int Id, UpdateNurseDto nurse)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var staff = await _context.Staff.Include(Staff => Staff.Nurce)
                    .FirstOrDefaultAsync(Staff => Staff.Id == Id);

                if (staff == null || staff.Nurce == null)
                    return false;

                staff.Salary = nurse.Salary;

                staff.Shift = nurse.Shift;
                staff.No_Of_Hour = nurse.No_Of_Hour;
                staff.Qualifications = nurse.Qualifications;
                staff.dept_id = nurse.dept_id;
                staff.Nurce.Specialization = nurse.Specialization;

                _context.Staff.Update(staff);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; // global Handlinge Exception will catch it 
            }
        }
    }
}