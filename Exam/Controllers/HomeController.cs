using Exam.Contexts;
using Exam.Models;
using Exam.ViewModels.LatestNewsAndBlogVMs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        ExamDbContext _context {  get; }

        public HomeController(ExamDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<LatestNewsAndBlogListItemVM> data = _context.LatestNewsAndBlogs.Select(l => new LatestNewsAndBlogListItemVM
            {
                AuthorId = l.AuthorId,
                Title = l.Title,
                Description = l.Description,
                Id = l.Id,
                ImageUrl = l.ImageUrl,
            }).ToList();
            return View(data);
        }
    }
}