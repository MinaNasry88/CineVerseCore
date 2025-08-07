using Entities.AppDbContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;

namespace Services
{
    public class MediaProductionsGetterService : IMediaProductionsGetterService
    {
        private readonly ApplicationDbContext _db;
        private readonly IPersonGetterService _personGetterService;

        public MediaProductionsGetterService(ApplicationDbContext db, IPersonGetterService personGetterService)
        {
            _db = db;
            _personGetterService = personGetterService;
        }

        public async Task<List<MediaProduction>> GetAllMediaProductions()
        {
            return await _db.MediaProductions.ToListAsync();
        }

        public async Task<MediaProduction> GetMediaProductionById(int id)
        {
            return await _db.MediaProductions.FirstAsync(mp => mp.Id == id);
        }

        public async Task<IEnumerable<MediaProduction>> GetPersonAllMediaProductions(int personId)
        {
            MediaProduction?[] personMediaProductions;

            if (await _personGetterService.IsStar(personId))
            {
                personMediaProductions = await _db.Stars.Include(s => s.MediaProduction).Where(s => s.PerformerId == personId)
                .Select(s => s.MediaProduction).ToArrayAsync();
            }
            else if (await _personGetterService.IsDirector(personId)) 
            {
                personMediaProductions = await _db.Directors.Include(s => s.MediaProduction).Where(s => s.PersonId == personId)
               .Select(s => s.MediaProduction).ToArrayAsync();
            }
            else
            {
                personMediaProductions = await _db.Writers.Include(s => s.MediaProduction).Where(s => s.PersonId == personId)
               .Select(s => s.MediaProduction).ToArrayAsync();
            }
            
            return personMediaProductions!;
        }
    }
}
