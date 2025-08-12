using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    public class StarsController : Controller
    {
        private readonly IStarsGetterService _starsGetterService;
        private readonly IMediaProductionsGetterService _mediaProductionsGetterService;

        public StarsController(IStarsGetterService starsGetterService, IMediaProductionsGetterService mediaProductionsGetterService)
        {
            _starsGetterService = starsGetterService;
            _mediaProductionsGetterService = mediaProductionsGetterService;
        }

        [Route("stars")]
        public async Task<IActionResult> Stars(string searchString = "")
        {
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Controller = nameof(StarsController); ;
            ViewBag.Action = nameof(Stars);
            ViewBag.Title = "Stars";

            return View("People", (await _starsGetterService.GetAllStars()).
                Where(s => s.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.Name));
        }

        [Route("star-details")]
        public async Task<IActionResult> StarDetails(int id)
        {
            var vm = new PersonDetailsViewModel()
            {
                Person = await _starsGetterService.GetStarById(id),
                MediaProductions = await _mediaProductionsGetterService.GetStarAllMediaProductions(id)
            };

            ViewBag.Breadcrum1 = "Star";
            ViewBag.Breadcrum2 = "Info";

            return View("PersonDetails", vm);
        }
    }
}
