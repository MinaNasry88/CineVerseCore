using Entities.DTOs;
using Entities.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CineVerseCore.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            // Checking for validation errors
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage);
                return View(register);
            }

            // ToDo: Store user registeration details into Identity database
            ApplicationUser user = new ApplicationUser()
            {
                PersonName = register.Username,
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.Phone
            };

            // Creating a new user using user manager
            IdentityResult result = await _userManager.CreateAsync(user, register.Password!);

            if (result.Succeeded)
            {
                // Signing In (creating authentication cookie)
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(register);
            }    
        }
    }
}
