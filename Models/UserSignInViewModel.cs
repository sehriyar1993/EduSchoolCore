using System.ComponentModel.DataAnnotations;

namespace EduSchoolCore.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "İstifadəçi Adını Daxil Edin")]
        public string username { get; set; }

        [Required(ErrorMessage = "Parolunuzu Daxil Edin")]
        public string password { get; set; }
    }
}
