using Entities.Models;

namespace ServiceContracts
{
    public interface IMediaProductionsGetterService
    {
        Task<List<MediaProduction>> GetAllMediaProductions();
        Task<MediaProduction> GetMediaProductionById(int id);

        Task<IEnumerable<MediaProduction>> GetPersonAllMediaProductions(int personId);
    }
}
