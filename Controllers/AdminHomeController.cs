using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using EduSchoolCore.EL.AllClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Drawing;
using X.PagedList;

namespace EduSchoolCore.Controllers
{
	[Authorize(Roles = "Admin")]

	public class AdminHomeController : Controller
    {
		CourcesManager am = new CourcesManager(new EFCourseDal());
		private readonly UserManager<AppUser> _userManager;
		BlogManager am1 = new BlogManager(new EFBlogDal());
		CAtegoryManager cm = new CAtegoryManager(new EFCategoryDal());
		AppUsermanager appm = new AppUsermanager(new EFAppuserDal());
		public AdminHomeController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(string searchString, int page = 1)
		{
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.UserID = value.Id;
			}
			else
			{
			}
			if (searchString != null)
				searchString = searchString.ToLower();
			ViewData["CurrentFilter"] = searchString;
			var values = from x in am.TListCourseWithCategory().OrderByDescending(x => x.CourseId) select x;
			if (!string.IsNullOrEmpty(searchString))
			{
				values = values.Where(m => m.Title.ToLower().Contains(searchString) || m.Description.ToLower().Contains(searchString));
			}
			//var values = pm.TGetAll();
			return View(values.ToPagedList(page, 12));

		}
		public IActionResult Index1(int page = 1)
		{

			var values = am1.TListBlogWithAppUser();
			return View(values.ToPagedList(page, 12));
		}
		public IActionResult Delete(int id)
		{
			var values = am.TGetByID(id);
			am.TDelete(values);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult AddCourse()
		{
			List<SelectListItem> values = (from x in cm.TGetAll()
										   select new SelectListItem
										   {
											   Text = x.CategoryName,
											   Value = x.CategoryId.ToString(),
										   }).ToList();
			ViewBag.cat = values;

			return View();
		}
		[HttpPost]
		public IActionResult AddCourse(Course product)
		{
			product.Status = true;
			am.TInsert(product);
			return RedirectToAction("Index");

		}
		[HttpGet]
		public IActionResult Update(int id)
		{
			List<SelectListItem> values = (from x in cm.TGetAll()
										   select new SelectListItem
										   {
											   Text = x.CategoryName,
											   Value = x.CategoryId.ToString(),
										   }).ToList();
			ViewBag.cat = values;
			var value = am.TGetByID(id);

			return View(value);
		}
		[HttpPost]
		public IActionResult Update(Course product)
		{
			am.TUpdate(product);
			return RedirectToAction("Index");

		}



		public IActionResult DeleteBlog(int id)
		{
			var values = am1.TGetByID(id);
			am1.TDelete(values);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> AddBlog()
		{
			if (User.Identity.IsAuthenticated)
			{
				var value = await _userManager.FindByNameAsync(User.Identity.Name);
				ViewBag.AppUserID = value.Id;
			}
			else
			{
			}

			return View();
		}
		[HttpPost]
		public IActionResult AddBlog(Blog product)
		{
			product.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
			product.Status = true;
			am1.TInsert(product);
			return RedirectToAction("Index");

		}
		[HttpGet]
		public IActionResult UpdateBlog(int id)
		{
		
			var value = am1.TGetByID(id);

			return View(value);
		}
		[HttpPost]
		public IActionResult UpdateBlog(Blog product)
		{
			am1.TUpdate(product);
			return RedirectToAction("Index");

		}
	}
}
