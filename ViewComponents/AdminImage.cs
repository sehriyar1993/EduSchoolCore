using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
    public class AdminImage : ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            var user = User.Identity.Name;
            ViewBag.userimage = c.Users.Where(x => x.UserName == user).Select(x => x.Image).FirstOrDefault();
            return View();
        }
    }
}
