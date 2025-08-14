using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace CineVerseCore.Controllers
{
    public class WritersController : Controller
    {
        private readonly IWritersGetterService _writersGetterService;
        private readonly IMediaProductionsGetterService _mediaProductionsGetterService;

        public WritersController(IWritersGetterService writersGetterService, IMediaProductionsGetterService mediaProductionsGetterService)
        {

            _mediaProductionsGetterService = mediaProductionsGetterService;
            _writersGetterService = writersGetterService;
        }

        [Route("writers")]
        public async Task<IActionResult> Writers(string searchString = "")
        {
            ViewBag.Action = ViewBag.Title = nameof(Writers);
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Controller = nameof(WritersController);

            return View("People", (await _writersGetterService.GetAllWriters())
                .Where(w => w.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .OrderBy(w => w.Name));
        }

        [Route("writer-details")]
        public async Task<IActionResult> WriterDetails(int id)
        {
            var vm = new PersonDetailsViewModel()
            {
                Person = await _writersGetterService.GetWriterById(id),
                MediaProductions = await _mediaProductionsGetterService.GetWriterAllMediaProductions(id)
            };

            ViewBag.Breadcrum1 = "Writer";
            ViewBag.Breadcrum2 = "Info";
            ViewBag.Action = nameof(WriterDetails);
            ViewBag.Controller = "Writers";

            return View("PersonDetails", vm);
        }
    }
}
