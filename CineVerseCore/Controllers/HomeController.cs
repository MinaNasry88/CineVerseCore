using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IMediaProductionsGetterService _mediaProductionsGetterService;

        public HomeController(IMediaProductionsGetterService mediaProductions)
        {
            _mediaProductionsGetterService = mediaProductions;
        }

        [Route("/")]
        [Route("[action]")]
        public async Task<IActionResult> Index(string searchString = "")
        {
            IEnumerable<MediaProduction> allMedia = (await _mediaProductionsGetterService.GetAllMediaProductions())
                .Where(mp => mp.Title!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).OrderBy(mp => mp.Title);
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Controller = nameof(HomeController);
            ViewBag.Action = nameof(Index);
            return View(allMedia);
        }
    }
}
