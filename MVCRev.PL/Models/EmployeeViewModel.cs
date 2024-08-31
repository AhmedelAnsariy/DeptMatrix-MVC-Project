using MVCRev.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Microsoft.AspNetCore.Http;

namespace MVCRev.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!!!")]
        public string Name { get; set; }

        [Range(20, 40)]
        public int? Age { get; set; }

        public decimal Salary { get; set; }


        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
     ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }



        [Phone]
        public string Phone { get; set; }


        [EmailAddress]
        public string Email { get; set; }

        public bool IsDeleted { get; set; }


        public bool IsActive { get; set; }

        [DisplayName("Date OF Creation")]
        public DateTime DateOfCreation { get; set; }

        [DisplayName("Date OF Hiring")]
        public DateTime HiringDate { get; set; }

        public Department Department { get; set; }

        public int? DepartmentId { get; set; }

        public string ImageName { get; set; }

        public IFormFile Image { get; set; } 
    }
}
