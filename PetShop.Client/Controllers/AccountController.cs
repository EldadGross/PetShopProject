using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShop.Data.Model;

namespace PetShop.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(admin.Name, admin.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task< IActionResult> CreateAdmin()
        {
            IdentityUser admin = new IdentityUser
            {
                UserName = "Eldad"
            };

            var resulte = await userManager.CreateAsync(admin, "Eldad4099@");

            if (resulte.Succeeded)
                return Content("Admin created");

            return Content("failde to create admin");
        }
    }
}
