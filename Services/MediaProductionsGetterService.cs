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
    }
}
