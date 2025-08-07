using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class PersonGetterService : IPersonGetterService
    {
        private readonly ApplicationDbContext _db;

        public PersonGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Person> GetPersonById(int id)
        {
            Person? person =  await _db.Persons.FirstOrDefaultAsync(p => p.Id == id);
            return person!;
        }

        public async Task<bool> IsDirector(int id)
        {
            return await _db.Directors.AnyAsync(d => d.PersonId == id);
        }

        public async Task<bool> IsStar(int id)
        {
            return await _db.Stars.AnyAsync(s => s.PerformerId == id); 
        }

        public async Task<bool> IsWriter(int id)
        {
            return await _db.Writers.AnyAsync(w => w.PersonId == id);
        }
    }
}
