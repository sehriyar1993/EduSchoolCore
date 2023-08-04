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
    public class BlogController : Controller
    {
        BlogManager am = new BlogManager(new EFBlogDal());
        private readonly UserManager<AppUser> _userManager;

        public BlogController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index( int page = 1)
        {

            var values = am.TListBlogWithAppUser();
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
            ViewBag.BlogID = id;
            var values = am.TGetByID(id);
            return View(values);
        }
    }
}
