using System.ComponentModel.DataAnnotations;

namespace Company.MVC.PL.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required !")]
        [EmailAddress(ErrorMessage = "Invalid Email Address !")]
        public string Email { get; set; }
    }
}
