using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using System.Linq;

namespace CineVerseCore.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IMediaProductionsService _mediaProductions;

        public HomeController(IMediaProductionsService mediaProductions)
        {
            _mediaProductions = mediaProductions;
        }

        [Route("/")]
        [Route("[action]")]
        public async Task<IActionResult> Index(string searchString = "")
        {
            return View((await _mediaProductions.GetAllMediaProductions()).Select(item => item.Title?.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
