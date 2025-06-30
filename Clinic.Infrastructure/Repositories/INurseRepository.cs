
using Clinic.Core.Entities;
using Clinic.Core.Interfaces;
using Clinic.Infrastructure.Data;

namespace Clinic.Infrastructure.Repositories
{
    public class NurseRepository : BaseRepository<Nurse>, INurseRepository
    {
        private readonly AppDbContext _context;

        public NurseRepository(AppDbContext context) : base(context)
        {
        }

        //public NurseRepository(AppDbContext context)
        //{
        //    _context = context;
        //}

        public async Task<bool> AddAsync(Nurse nurse)
        {
          
            await _context.Nurses.AddAsync(nurse);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
