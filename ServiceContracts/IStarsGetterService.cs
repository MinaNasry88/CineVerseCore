using Entities.Models;

namespace ServiceContracts
{
    public interface IStarsGetterService
    {
        Task<IEnumerable<Person>> GetAllStars();
        Task<IEnumerable<Person>> GetAllMediaProductionStars(int id);
        Task<Person> GetStarById(int id);
    }
}
