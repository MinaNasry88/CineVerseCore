using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class MediaProductionsGetterService : IMediaProductionsGetterService
    {
        private readonly ApplicationDbContext _db;

        public MediaProductionsGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<MediaProduction>> GetAllMediaProductions()
        {
            return await _db.MediaProductions.ToListAsync();
        }

        public async Task<MediaProduction> GetMediaProductionById(int id)
        {
            return await _db.MediaProductions.FirstAsync(mp => mp.Id == id);
        }

        public async Task<IEnumerable<MediaProduction>> GetStarAllMediaProductions(int personId)
        {
            MediaProduction?[] starMediaProductions = await _db.Stars.Include(s => s.MediaProduction).Where(s => s.PerformerId == personId)
                .Select(s => s.MediaProduction).ToArrayAsync();
            return starMediaProductions!;
        }

        public async Task<IEnumerable<MediaProduction>> GetWriterAllMediaProductions(int personId)
        {
            MediaProduction?[] writerMediaProductions = await _db.Writers.Include(s => s.MediaProduction).Where(s => s.PersonId == personId)
                .Select(s => s.MediaProduction).ToArrayAsync();
            return writerMediaProductions!;
        }
        public async Task<IEnumerable<MediaProduction>> GetDirectorAllMediaProductions(int personId)
        {
            MediaProduction?[] directorMediaProductions = await _db.Directors.Include(s => s.MediaProduction).Where(s => s.PersonId== personId)
                .Select(s => s.MediaProduction).ToArrayAsync();
            return directorMediaProductions!;
        }
    }
}
