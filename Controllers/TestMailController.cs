using Microsoft.AspNetCore.Mvc;
using ahrenburg.city.Services;
using System.Threading.Tasks;

namespace ahrenburg.city.Controllers
{
    public class TestMailController : Controller
    {
        public async Task<IActionResult> SendTestMail(string to)
        {
            var mailService = new EmailService();
            await mailService.SendMailAsync(to, "Test", "Dies ist ein Test.");
            return Content($"Testmail an {to} gesendet.");
        }
    }
}
