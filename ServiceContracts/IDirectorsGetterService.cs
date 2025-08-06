using Entities.Models;

namespace ServiceContracts
{
    public interface IDirectorsGetterService
    {
        Task<IEnumerable<Person>> GetAllDirectors();
        Task<IEnumerable<Person>> GetAllMediaProductionDirectors(int id);
    }
}
