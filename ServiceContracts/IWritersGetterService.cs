using Entities.Models;

namespace ServiceContracts
{
    public interface IWritersGetterService
    {
        Task<IEnumerable<Person>> GetAllWriters();
        Task<IEnumerable<Person>> GetAllMediaProductionWriters(int id);
        Task<Person> GetWriterById(int id);
    }
}
