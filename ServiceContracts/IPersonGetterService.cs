using Entities.Models;

namespace ServiceContracts
{
    public interface IPersonGetterService
    {
        Task<Person> GetPersonById(int id);
        Task<bool> IsWriter(int id);
        Task<bool> IsStar(int id);
        Task<bool> IsDirector(int id);
    }
}
