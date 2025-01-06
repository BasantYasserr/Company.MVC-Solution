using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.DAL.Models
{
    public class Employee : BaseEntity
    {
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

        public bool IsDeleted { get; set; }

        public DateTime HiringDate { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}
