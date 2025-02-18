using Company.MVC.DAL.Models;
using Company.MVC.PL.Helper;
using Company.MVC.PL.ViewModels;
using Company.MVC.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.MVC.PL.Controllers
{
    [Authorize (Roles = "Admin")]
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

        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();  //400

            var userFromDb = await _userManager.FindByIdAsync(id);
			
            if (userFromDb is null) return NotFound();   //404

            var userViewModel = new UserViewModel()
            {
                Id = userFromDb.Id,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                Email = userFromDb.Email,
                Roles = _userManager.GetRolesAsync(userFromDb).Result
            };

            return View(ViewName, userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();  //400
                if (ModelState.IsValid)
                {
                    var userFromDb = await _userManager.FindByIdAsync(id);

                    if (userFromDb is null) return NotFound();   //404

                    userFromDb.FirstName = model.FirstName;
                    userFromDb.LastName = model.LastName;
                    userFromDb.Email = model.Email;

                    await _userManager.UpdateAsync(userFromDb); 

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel model)
        {
            try
            {
                if (id != model.Id)   return BadRequest();  //400

                if (ModelState.IsValid)
                {
                    var userFromDb = await _userManager.FindByIdAsync(id);

                    if (userFromDb is null) return NotFound();   //404

                    await _userManager.DeleteAsync(userFromDb);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

    }
}
