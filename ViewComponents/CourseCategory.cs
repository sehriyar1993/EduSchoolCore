using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
    public class CourseCategory : ViewComponent
    {
        CAtegoryManager am = new CAtegoryManager(new EFCategoryDal());
        public IViewComponentResult Invoke()
        {
            var values = am.TGetAll();
            return View(values);
        }
    }
}
