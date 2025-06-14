using Microsoft.AspNetCore.Mvc;
using DOTNET_DIARIES.Models;
using DOTNET_DIARIES.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

        // Update the Details action to use urlHandle instead of id
        [Route("Blogposts/Details/{urlHandle}")]
        public async Task<IActionResult> Details(string urlHandle)
        {
            if (string.IsNullOrEmpty(urlHandle))
            {
                return NotFound();
            }

            var blogpost = await _context.Blogposts
                .Include(bt => bt.BlogpostTags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

            if (blogpost == null)
            {
                return NotFound();
            }

            return View(blogpost);
        }

        // GET: Blogposts/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }


        // POST: Blogposts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Blogpost blogpost, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                blogpost.PostedDate = DateTime.Now;
                blogpost.BlogpostTags = selectedTags.Select(tagId => new BlogpostTag
                {
                    TagId = tagId
                }).ToList();

                _context.Add(blogpost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Tags = _context.Tags.ToList();
            return View(blogpost);
        }
    }
}
