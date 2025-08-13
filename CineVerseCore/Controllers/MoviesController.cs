using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesGetterService _moviesGetterService;

        public MoviesController(IMoviesGetterService moviesGetterService)
        {
            _moviesGetterService = moviesGetterService;
        }

        [Route("movies")]
        public async Task<IActionResult> Index(string searchString = "")
        {
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Controller = "Movies";
            ViewBag.Action = (nameof(Index));
            ViewBag.Title = "Movies";
            return View((await _moviesGetterService.GetAllMovies()).Where(m => m.Title!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).OrderBy(m => m.Title));
        }
    }
}
