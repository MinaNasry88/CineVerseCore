using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IMediaProductionsGetterService _mediaProductionGetterService;
        private readonly IStarsGetterService _starsGetterService;
        private readonly IWritersGetterService _writersGetterService;
        private readonly IDirectorsGetterService _directorsGetterService;
        private readonly IPersonGetterService _personGetterService;

        public PersonsController(IMediaProductionsGetterService mediaProductionsGetterService, IPersonGetterService personGetterService, IStarsGetterService starsGetterService, IWritersGetterService writersGetterService, IDirectorsGetterService directorsGetterService)
        {
            _mediaProductionGetterService = mediaProductionsGetterService;
            _personGetterService = personGetterService;
            _starsGetterService = starsGetterService;
            _writersGetterService = writersGetterService;
            _directorsGetterService = directorsGetterService;
        }

        [Route("stars")]
        public async Task<IActionResult> Stars(string searchString = "")
        {
            ViewBag.CurrentSeachString = searchString;
            ViewBag.Controller = nameof(PersonsController); ;
            ViewBag.Action = nameof(Stars);
            ViewBag.Title = "Stars";

            return View("People", (await _starsGetterService.GetAllStars()).
                Where(s => s.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.Name));
        }

        [Route("writers")]
        public async Task<IActionResult> Writers(string searchString = "")
        {
            ViewBag.Action = ViewBag.Title = nameof(Writers);
            ViewBag.CurrentSearchStrig = searchString;
            ViewBag.Controller = nameof(PersonsController);

            return View("People", (await _writersGetterService.GetAllWriters())
                .Where(w => w.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .OrderBy(w => w.Name));
        }

        [Route("directors")]
        public async Task<IActionResult> Directors(string searchString = "")
        {
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Action = ViewBag.Title = nameof(Directors);
            ViewBag.Controller = nameof(PersonsController);
            return View("People", (await _directorsGetterService.GetAllDirectors()).
                Where(d => d.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).OrderBy(d => d.Name));
        }

        [Route("person-details")]
        public async Task<IActionResult> PersonDetails(int id)
        {
            var vm = new PersonDetailsViewModel()
            {
                Person = await _personGetterService.GetPersonById(id),
                MediaProductions = await _mediaProductionGetterService.GetPersonAllMediaProductions(id)
            };

            return View(vm);
        }

    }
}
