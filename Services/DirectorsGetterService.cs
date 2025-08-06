using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class DirectorsGetterService : IDirectorsGetterService
    {
        private readonly ApplicationDbContext _db;

        public DirectorsGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Person>> GetAllDirectors()
        {
            Person?[] directors = (await _db.Directors.Include(d => d.Person).
                GroupBy(d => d.PersonId).Select(d => d.FirstOrDefault()).ToArrayAsync())
                .Select(d => d?.Person).ToArray();

            return directors!;
        }

        public async Task<IEnumerable<Person>> GetAllMediaProductionDirectors(int id)
        {
            Person?[] mediaProductionDirectors = await _db.Directors.Include(d => d.Person)
                .Where(d => d.MediaProductionId == id).Select(d => d.Person).ToArrayAsync();
            return mediaProductionDirectors!;
        }
    }
}
