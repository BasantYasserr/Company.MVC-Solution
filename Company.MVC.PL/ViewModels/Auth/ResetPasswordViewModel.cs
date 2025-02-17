using System.ComponentModel.DataAnnotations;

namespace Company.MVC.PL.ViewModels.Auth
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required !")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "ConfirmPassword is Required !")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password and ConfirmPassword do not match !")]
		public string ConfirmPassword { get; set; }
	}
}
