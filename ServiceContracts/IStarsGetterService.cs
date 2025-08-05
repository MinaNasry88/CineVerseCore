using Entities.Models;

namespace ServiceContracts
{
    public interface IStarsGetterService
    {
        Task<IEnumerable<Person>> GetAllStars();
    }
}
