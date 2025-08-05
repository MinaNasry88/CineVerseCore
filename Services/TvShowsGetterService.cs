using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class TvShowsGetterService : ITvShowsGetterService
    {
        private readonly ApplicationDbContext _db;

        public TvShowsGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<MediaProduction>> GetAllTvShows()
        {
            return await _db.MediaProductions.Include(mp => mp.MediaProductionType).Where(mp => mp.MediaProductionType!.Name == "Tv Show").ToArrayAsync();
        }
    }
}
