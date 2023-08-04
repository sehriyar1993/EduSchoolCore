using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.Abstract;
using EduSchoolCore.DL.ConCreate;
using EduSchoolCore.EL.AllClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
	{
		CommentManager commentManager = new CommentManager(new EFCommentDAl());
		private readonly UserManager<AppUser> _userManager;
		Context c = new Context();
		public CommentController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public PartialViewResult AddComment()
		{
  			return PartialView();
		}
		[HttpPost]
		public IActionResult AddComment(Comment p)
		{
   //         var user = User.Identity.Name;
			//p.AppUserId =  c.Users.Where(x => x.UserName == user).Select(y => y.Id).FirstOrDefault();
            p.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
			p.Status = true;
			commentManager.TInsert(p);
			return RedirectToAction("Index", "Blog");
		}
	}
}
