using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ahrenburg.city.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Registrierung deaktiviert - zeige nur die Nachricht
            return Page();
        }

        public IActionResult OnPost()
        {
            // Registrierung komplett deaktiviert
            return RedirectToPage("./Login");
        }
    }
}
