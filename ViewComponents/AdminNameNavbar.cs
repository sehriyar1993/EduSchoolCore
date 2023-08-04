using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
	public class AdminNameNavbar : ViewComponent
	{
		Context c = new Context();
		public IViewComponentResult Invoke()
		{
			var user = User.Identity.Name;
			ViewBag.username = c.Users.Where(x => x.UserName == user).Select(x => x.Name).FirstOrDefault();
			ViewBag.usersurname = c.Users.Where(x => x.UserName == user).Select(x => x.Surname).FirstOrDefault();
			ViewBag.userimage = c.Users.Where(x => x.UserName == user).Select(x => x.Image).FirstOrDefault();
			ViewBag.usermail = c.Users.Where(x => x.UserName == user).Select(x => x.Email).FirstOrDefault();
			return View();
		}
	}
}
