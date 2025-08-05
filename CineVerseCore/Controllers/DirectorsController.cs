using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using System.Runtime.CompilerServices;

namespace CineVerseCore.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorsGetterService _directorsGetterService;

        public DirectorsController(IDirectorsGetterService directorsGetterService)
        {
            _directorsGetterService = directorsGetterService;
        }

        [Route("directors")]
        public async Task<IActionResult> Directors(string searchString = "")
        {
            ViewBag.CurrentSearchString = searchString;
            ViewBag.Action = ViewBag.Title = nameof(Directors);
            ViewBag.Controller = nameof(DirectorsController);
            return View("People", (await _directorsGetterService.GetAllDirectors()).
                Where(d => d.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).OrderBy(d => d.Name));
        }
    }
}
