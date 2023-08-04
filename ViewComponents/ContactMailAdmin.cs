using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
	public class ContactMailAdmin : ViewComponent
	{
		ContactManager cm = new ContactManager(new EFContactDal());
		public IViewComponentResult Invoke()
		{
			var values = cm.TGetAll();
			return View(values);
		}
	}
}
