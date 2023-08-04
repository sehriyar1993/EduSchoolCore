using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using EduSchoolCore.EL.AllClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace EduSchoolCore.Controllers
{
    [AllowAnonymous]
    public class CourseController : Controller
    {
        CourcesManager am = new CourcesManager(new EFCourseDal());
		private readonly UserManager<AppUser> _userManager;

		public CourseController(UserManager<AppUser> userManager)
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
        public async Task<IActionResult> Single(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var value = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserID = value.Id;
            }
            else
            {
            }
            ViewBag.CourseID = id;
            var values = am.TGetByID(id);
            return View(values);
        }
        public IActionResult ListForCategory(int id)
        {
            var values = am.TListForFilter(x => x.CategoryId == id);
            return View(values);
        }
    }
}
