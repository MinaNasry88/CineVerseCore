using Entities.Models;

namespace ServiceContracts
{
    public interface IStarsGetterService
    {
        Task<IEnumerable<Person>> GetAllMediaProductionStars(int id);
        Task<IEnumerable<Person>> GetAllStars();
        Task<Person> GetStarById(int id);
    }
}
