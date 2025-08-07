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

        public async Task<IEnumerable<Person>> GetAllMediaProductionWriters(int id)
        {
            Person?[] mediaProductionWriters = await _db.Writers.Include(w => w.Person)
                .Where(w => w.MediaProductionId == id)
                .Select(w => w.Person).ToArrayAsync();

            return mediaProductionWriters!;
        }

        public async Task<IEnumerable<Person>> GetAllWriters()
        {
            Person?[] writers = (await _db.Writers.Include(w => w.Person).GroupBy(w => w.PersonId).Select(w => w.FirstOrDefault()).ToArrayAsync()).Select(w => w?.Person).ToArray();
            return writers!;
        }

        public async Task<Person> GetWriterById(int id)
        {
            Person? writer = await _db.Persons.FirstOrDefaultAsync(w => w.Id == id);
            return writer!;
        }
    }
}
