using Microsoft.AspNetCore.Mvc;
using DOTNET_DIARIES.Models;
using DOTNET_DIARIES.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogpostsController
{
    public class BlogpostsController : Controller
    {
        private readonly DOTNET_DIARIES_Context _context;
        public BlogpostsController(DOTNET_DIARIES_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogposts = await _context.Blogposts
                .Include(b => b.BlogpostTags)
                .ThenInclude(bt => bt.Tag) // if you want tag details, not just TagId
                .ToListAsync();

            return View(blogposts);
            //return View(await _context.Blogposts.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var blogpost = await _context.Blogposts
                .Include(bt => bt.BlogpostTags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (blogpost == null)
            {
                return NotFound();
            }

            return View(blogpost);
        }

    }
}
