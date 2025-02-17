using Company.MVC.DAL.Models;
using Company.MVC.PL.ViewModels;
using Company.MVC.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.MVC.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		//Get, GetAll, Add, Update, Delete
		//Index, Details, Edit, Delete

		public UserController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(string InputSearch)
		{
			var users = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(InputSearch))
			{
				users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
			}
			else
			{
				users = await _userManager.Users.Where(U => U.FirstName.ToLower().Contains(InputSearch.ToLower())
																	|| U.LastName.ToLower().Contains(InputSearch.ToLower())
																	|| U.Email.ToLower().Contains(InputSearch.ToLower()))
												.Select(U => new UserViewModel()
												{
													Id = U.Id,
													FirstName = U.FirstName,
													LastName = U.LastName,
													Email = U.Email,
													Roles = _userManager.GetRolesAsync(U).Result
												}).ToListAsync();
			}
			return View(users);
		}
	}
}
