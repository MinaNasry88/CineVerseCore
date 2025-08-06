using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class MediaProductionGenresGetterService : IMediaProductionGenresGetterService
    {
        private readonly ApplicationDbContext _db;

        public MediaProductionGenresGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Genre>> GetMediaProductionGenres(int id)
        {
            Genre?[] mediaProductionGenres = await _db.MediaProductionGenres.Include(mp => mp.Genre)
                .Where(mp => mp.MediaProductionId == id)
                .Select(mp => mp.Genre).ToArrayAsync();
            return mediaProductionGenres!;
        }
    }
}
