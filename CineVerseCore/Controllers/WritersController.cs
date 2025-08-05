using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CineVerseCore.Controllers
{
    public class WritersController : Controller
    {
        private readonly IWritersGetterService _writersGetterService;

        public WritersController(IWritersGetterService writersGetterService)
        {
            _writersGetterService = writersGetterService;
        }

        [Route("writers")]
        public async Task<IActionResult> Writers(string searchString = "")
        {
            ViewBag.Action = ViewBag.Title = nameof(Writers);
            ViewBag.CurrentSearchStrig = searchString;
            ViewBag.Controller = nameof(WritersController);
            
            return View("People", (await _writersGetterService.GetAllWriters())
                .Where(w => w.Name!.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .OrderBy(w => w.Name));
        }
    }
}
