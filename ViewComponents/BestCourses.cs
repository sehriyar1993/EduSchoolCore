using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
    public class BestCourses : ViewComponent
    {
        CourcesManager am = new CourcesManager(new EFCourseDal());
        public IViewComponentResult Invoke()
        {
            var values = am.TListCourseWithCategory();
            return View(values);
        }
    }
}
