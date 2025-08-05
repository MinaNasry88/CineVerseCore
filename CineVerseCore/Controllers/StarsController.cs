using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using System.Runtime.CompilerServices;

namespace CineVerseCore.Controllers
{
    public class StarsController : Controller
    {
        private readonly IStarsGetterService _starsGetterService;
        
        public StarsController(IStarsGetterService starsGetterService)
        {
            _starsGetterService = starsGetterService;
        }

        [Route("stars")]
        public async Task<IActionResult> Stars(string searchString = "")
        {
            ViewBag.CurrentSeachString = searchString;
            ViewBag.Controller = nameof(StarsController); ;
            ViewBag.Action = nameof(Stars);
            ViewBag.Title = "Stars";

            return View("People", (await _starsGetterService.GetAllStars()).Where(s => s.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
