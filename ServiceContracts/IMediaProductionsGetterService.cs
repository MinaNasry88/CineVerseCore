using Entities.Models;

namespace ServiceContracts
{
    public interface IMediaProductionsGetterService
    {
        Task<List<MediaProduction>> GetAllMediaProductions();

    }
}
