using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
	public class DashboardNewsletterList : ViewComponent
	{
		NewsLetterManager cm = new NewsLetterManager(new EFNewsLetterDal());
		public IViewComponentResult Invoke()
		{
			var values = cm.TGetAll();
			return View(values);
		}
	}
}
