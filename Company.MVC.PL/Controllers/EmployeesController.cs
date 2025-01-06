using Company.MVC.BLL.Interfaces;
using Company.MVC.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.MVC.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                var Count = _employeeRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound();
            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Employee model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var Count = _employeeRepository.Update(model);
                    if (Count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Employee model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                var Count = _employeeRepository.Delete(model);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
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
