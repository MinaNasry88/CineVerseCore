using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CineVerseCore.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage);
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}
