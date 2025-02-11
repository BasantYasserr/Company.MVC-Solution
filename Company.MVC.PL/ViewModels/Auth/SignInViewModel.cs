using System.ComponentModel.DataAnnotations;

namespace Company.MVC.PL.ViewModels.Auth
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email is Required !")]
		[EmailAddress(ErrorMessage = "Invalid Email Address !")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password is Required !")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		public bool RememberMe { get; set; }
	}
}
