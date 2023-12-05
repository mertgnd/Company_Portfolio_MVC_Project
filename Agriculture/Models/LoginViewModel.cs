using System.ComponentModel.DataAnnotations;

namespace AgriculturePresentation.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter the username.")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Please enter the password.")]
        public string password { get; set; }
    }
}
