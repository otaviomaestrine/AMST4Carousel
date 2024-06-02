using AMST4_Carousel.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMST4_Carousel.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;

        public UserController(UserManager<User> userManager,SignInManager<User> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult>AddUser(User user)
        {
           await _userManager.CreateAsync(user,user.Password);
            return RedirectToAction(nameof(AddUser));
        }

        [HttpGet]
        public IActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> LoginUser(User user)
        {
           await _signManager.PasswordSignInAsync(user.UserName, user.Password,false, false);
            return RedirectToAction(nameof(AddUser));
        }
    }
}
