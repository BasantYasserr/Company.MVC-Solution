using Company.MVC.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.MVC.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        
        [Required(ErrorMessage = "Name is Required!")]
        public string Name { get; set; }
        
        
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int? Age { get; set; }
        
        
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{4,20}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}", ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        
        
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        
        
        //[DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        

        public bool IsActive { get; set; }
        
        public DateTime HiringDate { get; set; }
        
        public int? WorkForId { get; set; }          // Foreign Key to Department
        
        public Department? WorkFor { get; set; }     // Navigation Property to Department - Optional

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}
