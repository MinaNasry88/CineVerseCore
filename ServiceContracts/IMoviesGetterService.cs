using Entities.Models;

namespace ServiceContracts
{
    public interface IMoviesGetterService
    {
        Task<IEnumerable<MediaProduction>> GetAllMovies();
    }
}
