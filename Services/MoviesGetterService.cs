using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class MoviesGetterService : IMoviesGetterService
    {
        private readonly ApplicationDbContext _db;

        public MoviesGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<MediaProduction>> GetAllMovies()
        {
            return await _db.MediaProductions.Include(mp => mp.MediaProductionType).Where(mp => mp.MediaProductionType!.Name == "Movie").ToArrayAsync();
        }
    }
}
