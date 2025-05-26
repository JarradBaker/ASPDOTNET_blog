using Microsoft.AspNetCore.Mvc;
using DOTNET_DIARIES.Models;
using DOTNET_DIARIES.Data;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_DIARIES.Controllers
{
    public class TagsController : Controller
    {
        private readonly DOTNET_DIARIES_Context _context;
        public TagsController(DOTNET_DIARIES_Context context)
        {
            _context = context;
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }

        // POST: Tags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Blogposts");
            }
            return View(tag);
        }
    }
}