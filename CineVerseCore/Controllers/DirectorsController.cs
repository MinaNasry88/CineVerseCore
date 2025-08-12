using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace CineVerseCore.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorsGetterService _directorsGetterService;
        private readonly IMediaProductionsGetterService _mediaProductionsGetterService;

        public DirectorsController(IDirectorsGetterService starsGetterService, IMediaProductionsGetterService mediaProductionsGetterService)
        {
            _directorsGetterService = starsGetterService;
            _mediaProductionsGetterService = mediaProductionsGetterService;
        }

        [Route("directors")]
        public async Task<IActionResult> Directors(string searchString = "")
        {
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Controller = nameof(DirectorsController); ;
            ViewBag.Action = nameof(Directors);
            ViewBag.Title = "Directors";

            return View("People", (await _directorsGetterService.GetAllDirectors()).
                Where(s => s.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.Name));
        }

        [Route("director-details")]
        public async Task<IActionResult> DirectorDetails(int id)
        {
            var vm = new PersonDetailsViewModel()
            {
                Person = await _directorsGetterService.GetDirectorById(id),
                MediaProductions = await _mediaProductionsGetterService.GetDirectorAllMediaProductions(id)
            };

            ViewBag.Breadcrum1 = "Director";
            ViewBag.Breadcrum2 = "Info";

            return View("PersonDetails", vm);
        }
    }
}
