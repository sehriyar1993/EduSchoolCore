using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
    public class BlogCategory : ViewComponent
    {
        BlogManager am = new BlogManager(new EFBlogDal());
        public IViewComponentResult Invoke()
        {
            var values = am.TListBlogWithAppUser();
            return View(values);
        }
    }
}
