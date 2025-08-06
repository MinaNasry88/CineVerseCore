using Entities.Models;

namespace ServiceContracts
{
    public interface IMediaProductionRatingGetterService
    {
        Task<Rating> GetMediaProductionRating(int id);
    }
}
