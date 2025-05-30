using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DOTNET_DIARIES.Models;
using DOTNET_DIARIES.Data; // Add this
using Microsoft.EntityFrameworkCore; // Add this

namespace DOTNET_DIARIES.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DOTNET_DIARIES_Context _context; // Add this

    public HomeController(ILogger<HomeController> logger, DOTNET_DIARIES_Context context) // Add context parameter
    {
        _logger = logger;
        _context = context; // Assign context
    }

    public async Task<IActionResult> Index() // Make this async
    {
        var latestPosts = await _context.Blogposts
            .Include(b => b.BlogpostTags)
            .ThenInclude(bt => bt.Tag)
            .OrderByDescending(b => b.PostedDate)
            .Take(3)
            .ToListAsync();

        return View(latestPosts); // Pass posts to view
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}