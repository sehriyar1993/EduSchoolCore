using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        AboutManager am = new AboutManager(new EFAboutDal());
        public IActionResult Index()
        {
            var values = am.TGetAll();
            return View(values);
        }
    }
}
