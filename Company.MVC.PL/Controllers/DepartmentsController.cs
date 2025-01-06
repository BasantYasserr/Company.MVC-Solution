using Company.MVC.BLL.Interfaces;
using Company.MVC.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.MVC.PL.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository; //NULL

        public DepartmentsController(IDepartmentRepository departmentRepository) //ASK CLR Create Object From DepartmentRepository
        {
            _departmentRepository = departmentRepository; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)   // Server Side Validation
            {
                var Count = _departmentRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();    // 404
            var department = _departmentRepository.Get(id.Value);
            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();    // 404
            //var department = _departmentRepository.Get(id.Value);
            //return View(department);

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id, Department model) 
        {
            try
            {
                if (id != model.Id) return BadRequest();    // 404
                if (ModelState.IsValid)   // Server Side Validation
                {
                    var Count = _departmentRepository.Update(model);
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
            //if (id is null) return BadRequest();    // 404
            //var department = _departmentRepository.Get(id.Value);
            //return View(department);

            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();    // 404
                if (ModelState.IsValid)   // Server Side Validation
                {
                    var Count = _departmentRepository.Delete(model);
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
    }
}
