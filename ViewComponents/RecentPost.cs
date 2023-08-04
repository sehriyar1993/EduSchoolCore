using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
    public class RecentPost : ViewComponent
    {
        CourcesManager am = new CourcesManager(new EFCourseDal());
        public IViewComponentResult Invoke()
        {
            var values = am.TGetAll();
            return View(values);
        }
    }
}
