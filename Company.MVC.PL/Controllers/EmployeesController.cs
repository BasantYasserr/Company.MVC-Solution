using AutoMapper;
using Company.MVC.BLL.Interfaces;
using Company.MVC.DAL.Models;
using Company.MVC.PL.Helper;
using Company.MVC.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.MVC.PL.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesController(
                                    //IEmployeeRepository employeeRepository,
                                    //IDepartmentRepository departmentRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string InputSearch)
        {
            var employees = Enumerable.Empty<Employee>();
            //IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(InputSearch))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();    
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(InputSearch);
            }

            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Image is not null)
                {
                    model.ImageName = DocumentSettings.Upload(model.Image, "images");
                }

                //Casting  EmployeeViewModel (ViewModel) --> Employee (Model)
                //Mapping :- 
                //1. Manual Mapping
                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    Email = model.Email,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    PhoneNumber = model.PhoneNumber,
                //    Salary = model.Salary,
                //    WorkForId = model.WorkForId,
                //    WorkFor = model.WorkFor
                //};

                //2. Auto Maping
                var employee = _mapper.Map<Employee>(model);

                var Count = await _unitOfWork.EmployeeRepository.AddAsync(employee);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound();

            //Manual Mapping :  Employee(Model) --> EmployeeViewModel(ViewModel)
            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    HiringDate = employee.HiringDate,
            //    IsActive = employee.IsActive,
            //    PhoneNumber = employee.PhoneNumber,
            //    Salary = employee.Salary,
            //    WorkForId = employee.WorkForId,
            //    WorkFor = employee.WorkFor
            //};

            //2. Auto Maping
            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(viewName, employeeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
                ViewData["departments"] = departments;

                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSettings.Delete(model.ImageName, "images");
                    }
                    if (model.Image is not null)
                    {
                        model.ImageName = DocumentSettings.Upload(model.Image, "images");
                    }

                    //Mapping :  EmployeeViewModel(ViewModel) -->  Employee(Model)
                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Age = model.Age,
                    //    Email = model.Email,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    PhoneNumber = model.PhoneNumber,
                    //    Salary = model.Salary,
                    //    WorkForId = model.WorkForId,
                    //    WorkFor = model.WorkFor
                    //};

                    //2. Auto Maping
                    var employee = _mapper.Map<Employee>(model);

                    var Count = await _unitOfWork.EmployeeRepository.Update(employee);
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
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                //Mapping :  EmployeeViewModel(ViewModel) -->  Employee(Model)
                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    Email = model.Email,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    PhoneNumber = model.PhoneNumber,
                //    Salary = model.Salary,
                //    WorkForId = model.WorkForId,
                //    WorkFor = model.WorkFor
                //};

                //2. Auto Maping
                var employee = _mapper.Map<Employee>(model);

                var Count = await _unitOfWork.EmployeeRepository.Delete(employee);
                if (Count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSettings.Delete(model.ImageName, "images");
                    }
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
