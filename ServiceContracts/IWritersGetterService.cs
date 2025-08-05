using Entities.Models;

namespace ServiceContracts
{
    public interface IWritersGetterService
    {
        Task<IEnumerable<Person>> GetAllWriters();
    }
}
