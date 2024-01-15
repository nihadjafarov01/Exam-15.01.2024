using Exam.Contexts;
using Exam.Models;
using Exam.ViewModels.AuthVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    public class AuthController : Controller
    {
        ExamDbContext _context {  get; }
        UserManager<AppUser> _userManager {  get; }
        SignInManager<AppUser> _signInManager { get; }

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ExamDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = new AppUser
            {
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
                UserName = vm.Username,
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                return View(vm);
            }
            _context.SaveChanges();
            return RedirectToAction("Index" ,"Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = null;
            if (vm.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password,false,false);
            if (!result.Succeeded)
            {
                return View(vm);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
