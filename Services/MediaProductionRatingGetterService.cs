using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class MediaProductionRatingGetterService : IMediaProductionRatingGetterService
    {
        private readonly ApplicationDbContext _db;

        public MediaProductionRatingGetterService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Rating> GetMediaProductionRating(int id)
        {
            Rating rating = await _db.Ratings.Where(mp => mp.MediaProductionId == id).SingleAsync();
            return rating;
        }
    }
}
