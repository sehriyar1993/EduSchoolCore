using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using EduSchoolCore.EL.AllClass;
using EduSchoolCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EduSchoolCore.Controllers
{

	public class ProfileController : Controller
	{
		AppUsermanager appm = new AppUsermanager(new EFAppuserDal());
		private readonly UserManager<AppUser> _userManager;
		Context c = new Context();
		public ProfileController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> MyAccount()
		{
            var user1 = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.userimage = c.Users.Where(x => x.UserName == user1).Select(x => x.Image).FirstOrDefault();
            AppUserRegisterViewModel p = new AppUserRegisterViewModel();
			p.Email = user.Email;
			p.Surname = user.Surname;
			p.ImageUrl = user.Image;
			p.Username = user.UserName;
			p.Name = user.Name;
			p.About = user.About;
			return View(p);
		}
		[HttpPost]
		public async Task<IActionResult> MyAccount(AppUserRegisterViewModel p)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.Name = p.Name;
			user.Surname = p.Surname;
			user.About = p.About;
			user.Image = p.ImageUrl;
			user.Email = p.Email;
			user.UserName = p.Username;
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return RedirectToAction("SingIn");
			}
			return View();
		}
	}
}
