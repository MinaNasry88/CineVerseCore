using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediaProductionsGetterService _mediaProductionsGetterService;

        public HomeController(IMediaProductionsGetterService mediaProductions)
        {
            _mediaProductionsGetterService = mediaProductions;
        }

        [Route("/")]
        [Route("home")]
        public async Task<IActionResult> Index(string searchString = "")
        {
            
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Controller = nameof(HomeController);
            ViewBag.Action = nameof(Index);
            ViewBag.Title = "Home";
            return View((await _mediaProductionsGetterService.GetAllMediaProductions())
                .Where(mp => mp.Title!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).OrderBy(mp => mp.Title));
        }
    }
}
