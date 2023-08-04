using EduSchoolCore.BL.Concreate;
using EduSchoolCore.DL.ConCreate;
using Microsoft.AspNetCore.Mvc;

namespace EduSchoolCore.ViewComponents
{
    public class _CommentListBlog : ViewComponent
    {
        CommentManager am = new CommentManager(new EFCommentDAl());
        public IViewComponentResult Invoke()
        {
            var values = am.TListCommentWithAppUser();
            return View(values);
        }
    }
}
