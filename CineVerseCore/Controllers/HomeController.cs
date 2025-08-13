using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediaProductionsGetterService _mediaProductionsGetterService;
        private readonly IMediaProductionGenresGetterService _mediaProductionGenresGetterService;
        private readonly IMediaProductionRatingGetterService _mediaProductionRatingGetterService;
        private readonly IStarsGetterService _starsGetterService;
        private readonly IWritersGetterService _writersGetterService;
        private readonly IDirectorsGetterService _DirectorsGetterService;

        public HomeController(IMediaProductionsGetterService mediaProductions,
            IMediaProductionGenresGetterService mediaProductionGenresGetterService,
            IMediaProductionRatingGetterService mediaProductionRatingGetterService,
            IStarsGetterService starsGetterService,
            IWritersGetterService writersGetterService,
            IDirectorsGetterService directorsGetterService)
        {
            _mediaProductionsGetterService = mediaProductions;
            _mediaProductionGenresGetterService = mediaProductionGenresGetterService;
            _mediaProductionRatingGetterService = mediaProductionRatingGetterService;
            _starsGetterService = starsGetterService;
            _writersGetterService = writersGetterService;
            _DirectorsGetterService = directorsGetterService;
        }

        [Route("/")]
        [Route("home")]
        public async Task<IActionResult> Index(string searchString = "")
        {
            
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Controller = "Home";
            ViewBag.Action = nameof(Index);
            ViewBag.Title = "Home";
            return View((await _mediaProductionsGetterService.GetAllMediaProductions())
                .Where(mp => mp.Title!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).OrderBy(mp => mp.Title));
        }

        [Route("media-production/details")]
        public async Task<IActionResult> Details(int id)
        {
            MediaProductionDetailsViewModel vm = new MediaProductionDetailsViewModel()
            {
                MediaProduction = await _mediaProductionsGetterService.GetMediaProductionById(id),
                Genres = (await _mediaProductionGenresGetterService.GetMediaProductionGenres(id)).OrderBy(g => g.GenreName),
                Rating = await _mediaProductionRatingGetterService.GetMediaProductionRating(id),
                Stars = (await _starsGetterService.GetAllMediaProductionStars(id)).OrderBy(s => s.Name),
                Writers = (await _writersGetterService.GetAllMediaProductionWriters(id)).OrderBy(w => w.Name),
                Directors = (await _DirectorsGetterService.GetAllMediaProductionDirectors(id)).OrderBy(d => d.Name)
            };

            return View("MediaProductionDetails", vm);
        }
    }
}
