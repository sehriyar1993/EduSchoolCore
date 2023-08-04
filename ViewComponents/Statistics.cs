using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
    public class Statistics : ViewComponent
    {
        AboutManager am = new AboutManager(new EFAboutDal());
        public IViewComponentResult Invoke()
        {
            var values = am.TGetAll();
            return View(values);
        }
    }
}
