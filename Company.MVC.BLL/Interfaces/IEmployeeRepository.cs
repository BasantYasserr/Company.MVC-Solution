using Company.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByNameAsync(string name);

        //Department GetByName(string name);
        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);
        //int Add(Employee entity);
        //int Update(Employee entity);
        //int Delete(Employee entity);
    }
}
