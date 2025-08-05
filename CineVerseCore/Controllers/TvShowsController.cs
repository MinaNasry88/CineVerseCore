using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    public class TvShowsController : Controller
    {
        private readonly ITvShowsGetterService _tvShowsGetterService;

        public TvShowsController(ITvShowsGetterService tvShowsGetterService)
        {
            _tvShowsGetterService = tvShowsGetterService;
        }

        [Route("tv-shows")]
        public async Task<IActionResult> Index(string searchString = "")
        {
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Action = nameof(Index);
            ViewBag.Controller = nameof(TvShowsController);
            ViewBag.Title = "Tv Shows";
            return View((await _tvShowsGetterService.GetAllTvShows())
                .Where(mp => mp.Title!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).OrderBy(mp => mp.Title));
        }
    }
}