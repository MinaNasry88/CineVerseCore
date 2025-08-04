using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class MediaProductionsService : IMediaProductionsService
    {
        private readonly ApplicationDbContext _db;

        public MediaProductionsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<MediaProduction>> GetAllMediaProductions()
        {
            return await _db.MediaProductions.OrderBy(mp => mp.Title).ToListAsync();
        }
    }
}
