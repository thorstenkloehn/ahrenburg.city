using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using ahrenburg.city.Data;
using ahrenburg.city.Models;
using Markdig;
using Microsoft.EntityFrameworkCore;

namespace ahrenburg.city.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly string _adminEmail;

        public BlogController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _adminEmail = _configuration["Blog:AdminEmail"];
        }

        // GET: /Blog
        public IActionResult Index()
        {
            var posts = _context.BlogPosts
                .AsNoTracking() // Performance: keine Change-Tracking-Overhead
                .OrderByDescending(b => b.CreatedAt)
                .ToList();
            return View(posts);
        }

        // GET: /Blog/Details/5
        public IActionResult Details(int id)
        {
            var post = _context.BlogPosts.FirstOrDefault(b => b.Id == id);
            if (post == null) return NotFound();
            // Markdown zu HTML konvertieren
            post.Content = Markdown.ToHtml(post.Content ?? "");
            return View(post);
        }

        // GET: /Blog/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Email != _adminEmail) return Forbid();
            return View();
        }

        // POST: /Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(BlogPost post)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Email != _adminEmail) return Forbid();
            if (ModelState.IsValid)
            {
                _context.BlogPosts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: /Blog/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Email != _adminEmail) return Forbid();
            var post = _context.BlogPosts.FirstOrDefault(b => b.Id == id);
            if (post == null) return NotFound();
            return View(post);
        }

        // POST: /Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, BlogPost post)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Email != _adminEmail) return Forbid();
            if (id != post.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: /Blog/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Email != _adminEmail) return Forbid();
            var post = _context.BlogPosts.FirstOrDefault(b => b.Id == id);
            if (post == null) return NotFound();
            return View(post);
        }

        // POST: /Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Email != _adminEmail) return Forbid();
            var post = _context.BlogPosts.FirstOrDefault(b => b.Id == id);
            if (post == null) return NotFound();
            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
