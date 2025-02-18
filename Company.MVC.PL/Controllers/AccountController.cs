using Company.MVC.DAL.Models;
using Company.MVC.PL.Helper;
using Company.MVC.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Common;

namespace Company.MVC.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, 
								 SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region SignUp
		//SignUp 
		[HttpGet]    //  /Account/SignUp
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUpAsync(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByNameAsync(model.UserName);
					if (user is null)
					{
						user = await _userManager.FindByEmailAsync(model.Email);
						if (user is null)
						{
							user = new ApplicationUser
							{
								UserName = model.UserName,
								FirstName = model.FirstName,
								LastName = model.LastName,
								Email = model.Email,
								IsAgree = model.IsAgree,
							};
							var result = await _userManager.CreateAsync(user, model.Password);

							if (result.Succeeded)
							{
								return RedirectToAction("SignIn", "Account");
							}
							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
						}
						ModelState.AddModelError(string.Empty, "Email already exists !");
						return View(model);
					}
					ModelState.AddModelError(string.Empty, "UserName already exists !");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}
		#endregion

		#region SignIn

		//SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]    //  /Account/SignIn
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user != null)
					{
						//Check Password
						var Flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (Flag)
						{
							//SignIn
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
							if (result.Succeeded)
							{
								return RedirectToAction("Index", "Home");
							}
						}
						ModelState.AddModelError(string.Empty, "Invalid Login !");
					}
					ModelState.AddModelError(string.Empty, "Invalid Login !");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}

        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn", "Account");
		}
		#endregion

		#region ForgetPassword
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user != null)
				{
					//Create Token 
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					//Creare Reset PAssword URL 
					var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

					//Create Email
					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = url
					};

					//Send Email
					EmailSettings.SendEmail(email);


					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Invalid Reset Password!");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}

		#endregion

		#region ResetPassword
		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["Email"] = email;
			TempData["Token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["Token"] as string;

				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Operation! Please Try Again");

			}
			return View();
		} 
		#endregion

		public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

