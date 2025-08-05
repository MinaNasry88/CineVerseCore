using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class WritersGetterService : IWritersGetterService
    {
        private readonly ApplicationDbContext _db;

        public WritersGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Person>> GetAllWriters()
        {
            Person?[] writers = (await _db.Writers.Include(w => w.Person).GroupBy(w => w.PersonId).Select(w => w.FirstOrDefault()).ToArrayAsync()).Select(w => w?.Person).ToArray();
            return writers!;
        }
    }
}
