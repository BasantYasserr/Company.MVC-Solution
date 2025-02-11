using Company.MVC.BLL.Interfaces;
using Company.MVC.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.MVC.PL.Controllers
{
    public class DepartmentsController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository; //NULL
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork) //ASK CLR Create Object From DepartmentRepository
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)   // Server Side Validation
            {
                var Count = await _unitOfWork.DepartmentRepository.AddAsync(model);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();    // 404
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            return View(viewName, department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();    // 404
            //var department = _departmentRepository.Get(id.Value);
            //return View(department);

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id, Department model) 
        {
            try
            {
                if (id != model.Id) return BadRequest();    // 404
                if (ModelState.IsValid)   // Server Side Validation
                {
                    var Count = await _unitOfWork.DepartmentRepository.Update(model);
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
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest();    // 404
            //var department = _departmentRepository.Get(id.Value);
            //return View(department);

            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, Department model)
        {
            try
            {
                if (id != model.Id) return BadRequest();    // 404
                if (ModelState.IsValid)   // Server Side Validation
                {
                    var Count = await _unitOfWork.DepartmentRepository.Delete(model);  
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
