using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Models;
using PhoneBook.Web.Services.Interfaces;

namespace PhoneBook.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn(string? ReturnUrl)
        {
            SigninInput signinInput = new SigninInput { ReturnUrl = ReturnUrl };
            return View(signinInput);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SigninInput signinInput)
        {
            if (!ModelState.IsValid)
                return View();
            var response = await _identityService.SignIn(signinInput);
            if (!response.IsSuccessful)
            {
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(signinInput.ReturnUrl))
                    return Redirect(signinInput.ReturnUrl);
                else
                    return RedirectToAction(nameof(Index), "Home");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshToken();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
