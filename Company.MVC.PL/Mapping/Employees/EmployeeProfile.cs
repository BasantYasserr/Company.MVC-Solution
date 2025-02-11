using AutoMapper;
using Company.MVC.DAL.Models;
using Company.MVC.PL.ViewModels.Employees;

namespace Company.MVC.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile   //built-in Class
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
