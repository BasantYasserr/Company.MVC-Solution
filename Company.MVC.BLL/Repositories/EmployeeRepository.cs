using Company.MVC.BLL.Interfaces;
using Company.MVC.DAL.Data.Contexts;
using Company.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL.Repositories
{

    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(AppDbContext context) : base (context) 
        {
        }

        
    }
}
