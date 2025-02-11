using Company.MVC.BLL.Interfaces;
using Company.MVC.BLL.Repositories;
using Company.MVC.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDepartmentRepository _departmentRepository;
        private IEmployeeRepository _employeeRepository;

        public UnitOfWork(AppDbContext context)
        {
            _departmentRepository = new DepartmentRepository(context);
            _employeeRepository = new EmployeeRepository(context);
            _context = context;
        }

        public IDepartmentRepository DepartmentRepository => _departmentRepository;
        public IEmployeeRepository EmployeeRepository => _employeeRepository;
    }
}
