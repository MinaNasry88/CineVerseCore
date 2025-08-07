using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class StarsGetterService : IStarsGetterService
    {
        private readonly ApplicationDbContext _db;

        public StarsGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Person>> GetAllMediaProductionStars(int id)
        {
            Person?[] mediaProductionStars =  await _db.Stars.Include(mp => mp.Performer)
                .Where(mp => mp.MediaProductionId == id).Select(mp => mp.Performer).ToArrayAsync();
            return mediaProductionStars!;
        }

        public async Task<IEnumerable<Person>> GetAllStars()
        {
            Person?[] stars = (await _db.Stars.Include(s => s.Performer).GroupBy(s => s.PerformerId)
                .Select(s => s.FirstOrDefault()).ToArrayAsync()).Select(s => s?.Performer).ToArray();
            return stars!;
        }

        public async Task<Person> GetStarById(int id)
        {
            Person? star = await _db.Persons.Where(p => p.Id == id).SingleOrDefaultAsync();
            return star!;
        }
    }
}
