﻿using System.ComponentModel.DataAnnotations;

namespace Company.MVC.PL.ViewModels.Auth
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "UserName is Required !")]
		public string UserName { get; set; }
		

		[Required(ErrorMessage = "FirstName is Required !")]
		public string FirstName { get; set; }


		[Required(ErrorMessage = "LastName is Required !")]
		public string LastName { get; set; }


		[Required(ErrorMessage = "Email is Required !")]
		[EmailAddress(ErrorMessage = "Invalid Email Address !")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password is Required !")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "ConfirmPassword is Required !")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password and ConfirmPassword do not match !")]
		public string ConfirmPassword { get; set; }
		
		
		public bool IsAgree { get; set; }
	}
}
