using Entities.Models;

namespace ServiceContracts
{
    public interface ITvShowsGetterService
    {
        Task<IEnumerable<MediaProduction>> GetAllTvShows();
    }
}
