using Entities.Models;

namespace ServiceContracts
{
    public interface IMediaProductionGenresGetterService
    {
        Task<IEnumerable<Genre>> GetMediaProductionGenres(int id);
    }
}
