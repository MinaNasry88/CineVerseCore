using Entities.Models;

namespace ServiceContracts
{
    public interface IMediaProductionsGetterService
    {
        Task<List<MediaProduction>> GetAllMediaProductions();
        Task<MediaProduction> GetMediaProductionById(int id);
        Task<IEnumerable<MediaProduction>> GetStarAllMediaProductions(int personId);
        Task<IEnumerable<MediaProduction>> GetWriterAllMediaProductions(int personId);
        Task<IEnumerable<MediaProduction>> GetDirectorAllMediaProductions(int personId);

    }
}
