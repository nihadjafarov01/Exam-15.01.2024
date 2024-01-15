using Exam.Contexts;
using Exam.Models;
using Exam.ViewModels.LatestNewsAndBlogVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class LatestNewsAndBlogController : Controller
    {
        ExamDbContext _context {  get; }

        public LatestNewsAndBlogController(ExamDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
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
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LatestNewsAndBlogCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            LatestNewsAndBlog lnab = new LatestNewsAndBlog
            {
                AuthorId = vm.AuthorId,
                Description = vm.Description,
                ImageUrl = vm.ImageUrl,
                Title = vm.Title,
            };
            _context.LatestNewsAndBlogs.Add(lnab);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        { 
            var data = _context.LatestNewsAndBlogs.Find(id);
            LatestNewsAndBlogUpdateVM vm = new LatestNewsAndBlogUpdateVM
            {
                AuthorId = data.AuthorId,
                Description = data.Description,
                ImageUrl = data.ImageUrl,
                Title = data.Title,
            };
            return View(vm);
        }
        [HttpPost]
        public ActionResult Update(int id, LatestNewsAndBlogUpdateVM vm)
        {
            LatestNewsAndBlog data = _context.LatestNewsAndBlogs.Find(id);
            data.Description = vm.Description;
            data.ImageUrl = vm.ImageUrl;
            data.Title = vm.Title;
            data.AuthorId = vm.AuthorId;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var data = _context.LatestNewsAndBlogs.Find(id);
            _context.LatestNewsAndBlogs.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
