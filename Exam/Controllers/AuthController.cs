using Exam.Contexts;
using Exam.Helpers;
using Exam.Models;
using Exam.ViewModels.AuthVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    public class AuthController : Controller
    {
        UserManager<AppUser> _userManager {  get; }
        SignInManager<AppUser> _signInManager { get; }
        RoleManager<IdentityRole> _roleManager { get; }
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ExamDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
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
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password,vm.RememberMe,true);

            if (!result.Succeeded)
            {
                return View(vm);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<bool> CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item.ToString(),
                    });
                    if (!result.Succeeded)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
