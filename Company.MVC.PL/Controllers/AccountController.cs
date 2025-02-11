using Company.MVC.DAL.Models;
using Company.MVC.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company.MVC.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
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


		//SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
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
							return RedirectToAction("Index", "Home");
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

	}
}
