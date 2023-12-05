using System.ComponentModel.DataAnnotations;

namespace AgriculturePresentation.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username can not be null!")]
        public string userName { get; set; }
        [Required(ErrorMessage = "E-Mail can not be null!")]
        public string mail { get; set; }
        [Required(ErrorMessage = "Password can not be null!")]
        public string password { get; set; }
        [Required(ErrorMessage = "Password can not be null!")]
        [Compare("password",ErrorMessage ="Passwords are not the same!!!")]
        public string passwordConfirm { get; set; }
    }
}
