using Exam.Contexts;
using Exam.Models;
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
            var data = _context.LatestNewsAndBlogs.ToList();
            return View(data);
        }
    }
}