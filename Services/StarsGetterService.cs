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

        public async Task<IEnumerable<Person>> GetAllStars()
        {
            Person?[] stars = (await _db.Stars.Include(s => s.Performer).GroupBy(s => s.PerformerId)
                .Select(s => s.FirstOrDefault()).ToArrayAsync()).Select(s => s?.Performer).ToArray();
            return stars!;
        }
    }
}
