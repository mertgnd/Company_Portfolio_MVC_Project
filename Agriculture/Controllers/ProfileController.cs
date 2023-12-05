using AgriculturePresentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgriculturePresentation.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel userEditViewModel = new();
            userEditViewModel.UserName = values.UserName;
            userEditViewModel.Mail = values.Email;
            userEditViewModel.Phone = values.PhoneNumber;
            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel user)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user.Password == user.ConfirmPassword)
            {
                values.UserName = user.UserName;
                values.Email = user.Mail;
                values.PhoneNumber = user.Phone;
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, user.Password);
                var result = await _userManager.UpdateAsync(values);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }
    }
}
