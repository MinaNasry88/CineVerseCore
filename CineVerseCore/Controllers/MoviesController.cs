using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    [Route("[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMoviesGetterService _moviesGetterService;

        public MoviesController(IMoviesGetterService moviesGetterService)
        {
            _moviesGetterService = moviesGetterService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index(string searchString = "")
        {
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Controller = nameof(MoviesController);
            ViewBag.Action = (nameof(Index));
            return View((await _moviesGetterService.GetAllMovies()).Where(m => m.Title!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).OrderBy(m => m.Title));
        }
    }
}
