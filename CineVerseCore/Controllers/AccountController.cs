using Entities.DTOs;
using Entities.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CineVerseCore.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
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
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            // Checking for validation errors
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage);
                return View(registerDto);
            }

            // ToDo: Store user registeration details into Identity database
            ApplicationUser user = new ApplicationUser()
            {
                PersonName = registerDto.Username,
                UserName = registerDto.Email,
                Email = registerDto.Email,
                PhoneNumber = registerDto.Phone
            };

            // Creating a new user using user manager
            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password!);

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

                return View(registerDto);
            }    
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // Checking for validation errors
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage);
                return View(loginDto);
            }

           
            SignInResult result = await _signInManager.PasswordSignInAsync(loginDto.Email!, loginDto.Password!, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            { 
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError("Login", "Invalid Email or Password!");
                return View(loginDto);
            }   
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
    }
}
