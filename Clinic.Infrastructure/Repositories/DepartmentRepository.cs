
using Clinic.Core.DTOs;
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Clinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department> ,IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<bool> AddAsync(DepartmentRequest dbtRequest)
        {
            var department = new Department
            {
                Name = dbtRequest.Name,
                Phone = dbtRequest.Phone,
            };

            await _context.Departments.AddAsync(department);
            return await _context.SaveChangesAsync() > 0;

        }
        public async Task<bool> UpdateAsync( int Id,DepartmentRequest dbtRequest)
        {
            var department = await _context.Departments.FindAsync(Id);

            if (department == null)
                return false;
            

            department.Name = dbtRequest.Name;
            department.Phone = dbtRequest.Phone;

           
                var affectedRows = await _context.SaveChangesAsync();
                    return affectedRows > 0;
           

        }

        async Task<IReadOnlyList<DepartmentResponse>> IDepartmentRepository.GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking()
                .Select(department => new DepartmentResponse
                {
                    Name = department.Name,
                    Phone = department.Phone
                })
                .ToListAsync();
        }

    }
}
