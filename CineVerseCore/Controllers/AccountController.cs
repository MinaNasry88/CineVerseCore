using Entities.DTOs;
using Entities.IdentityModels;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CineVerseCore.Controllers
{
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBookmarkService _bookmarkService;
        private readonly IMediaProductionsGetterService _mediaProductionGetterService; 

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IBookmarkService bookmarkService, IMediaProductionsGetterService mediaProductionsGetterService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bookmarkService = bookmarkService;
            _mediaProductionGetterService = mediaProductionsGetterService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto, [FromForm] string g_recaptcha_response, [FromServices] RecaptchaService recaptchaService)
        {
            // recaptcha validation
            bool isValid = await recaptchaService.VerifyTokenAsync(g_recaptcha_response);

            if (!isValid)
            {
                ModelState.AddModelError("Register", "Failed reCAPTCHA verification.");
                return View();
            }

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
        public async Task<IActionResult> Login(LoginDto loginDto, [FromForm] string g_recaptcha_response, [FromServices] RecaptchaService recaptchaService, string returnUrl = "Home/Index")
        {
            // recaptcha validation
            bool isValid = await recaptchaService.VerifyTokenAsync(g_recaptcha_response);

            if (!isValid)
            {
                ModelState.AddModelError("Login", "Failed reCAPTCHA verification.");
                return View();
            }

            // Checking for validation errors
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage);
                return View(loginDto);
            }

           
            SignInResult result = await _signInManager.PasswordSignInAsync(loginDto.Email!, loginDto.Password!, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            { 
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                
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

        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true); // valid: Email address isn't already registered
            }
            else
            {
                return Json(false); // invalid: Email address is already registered
            }
        }

        public async Task<IActionResult> AddBookmark(int id, int personId, string userName, string actionName, string controllerName)
        {
            await _bookmarkService.AddBookMark(id, userName);

            return RedirectToAction(actionName, controllerName, new { id = personId });
        }

        public async Task<IActionResult> DeleteBookmark(int id, string userName)
        {
            await _bookmarkService.DeleteBookMark(id, userName);

            return RedirectToAction(nameof(AccountController.MyProfile), new { userName = userName });
        }

        public async Task<IActionResult> MyProfile(string userName, string searchString = "")
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            UserProfileViewModel userProfile = new UserProfileViewModel()
            {
                User = user,
                BookmarkedMediaProductions = (await _mediaProductionGetterService.GetUserBookmarkedMediaProductions(user!.Id))
                .Where(mp => mp.Title!.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            };

            ViewBag.CurrentSearchString = searchString;

            return View(userProfile);
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
            {
                ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
            }

            return View(); // Views/Shared/Error.cshtml
        }
    }
}
