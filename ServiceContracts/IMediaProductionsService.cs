using Entities.Models;

namespace ServiceContracts
{
    public interface IMediaProductionsService
    {
        Task<List<MediaProduction>> GetAllMediaProductions();

    }
}
