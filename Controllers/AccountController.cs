using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ahrenburg.city.Models;
using System.Threading.Tasks;

namespace ahrenburg.city.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult CustomLogin()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CustomLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            model.ErrorMessage = "Login fehlgeschlagen. Bitte prüfen Sie Ihre Zugangsdaten.";
            return View(model);
        }
    }
}
