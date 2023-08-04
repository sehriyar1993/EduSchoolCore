using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using EduSchoolCore.EL.AllClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        NewsLetterManager nm = new NewsLetterManager(new EFNewsLetterDal());
        ContactManager cm = new ContactManager(new EFContactDal());
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact c)
        {
            c.Status = true;
            c.Date = DateTime.Parse(DateTime.Now.ToLongDateString());
            cm.TInsert(c);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult NewsLetterSend()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewsLetterSend(NewsLetter c)
        {
            c.Date = DateTime.Parse(DateTime.Now.ToLongDateString());
            c.Status = true;
            nm.TInsert(c);
            return RedirectToAction("Index", "Home");
        }
    }
}
