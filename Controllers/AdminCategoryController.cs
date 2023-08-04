using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using EduSchoolCore.EL.AllClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EduSchoolCore.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminCategoryController : Controller
	{
		CAtegoryManager cm = new CAtegoryManager(new EFCategoryDal());
		public IActionResult Index()
		{
			var values = cm.TGetAll();
			return View(values);
		}
		[HttpGet]
		public IActionResult AddCateqory()
		{

			return View();
		}
		[HttpPost]
		public IActionResult AddCateqory(Category c)
		{
			c.Status = true;
			cm.TInsert(c);
			return RedirectToAction("Index");

		}
		public IActionResult DeleteCateqory(int id)
		{
			var values = cm.TGetByID(id);
			cm.TDelete(values);
			return RedirectToAction("Index");

		}
		public IActionResult ChangeToFolse(int id)
		{
			Context c = new Context();
			var values = c.Categories.Find(id);
			values.Status = false;
			c.Update(values);
			c.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult ChangeToTrue(int id)
		{
			Context c = new Context();
			var value = c.Categories.Find(id);
			value.Status = true;
			c.Update(value);
			c.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]

		public IActionResult UpdateCateqory(int id)
		{
			var values = cm.TGetByID(id);
			return View(values);
		}
		[HttpPost]
		public IActionResult UpdateCateqory(Category c)
		{
			cm.TUpdate(c);
			return RedirectToAction("Index");

		}
	}
}
