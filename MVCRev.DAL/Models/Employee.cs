using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRev.DAL.Models
{
    public class Employee : BaseEntity
    {


        [Required] // InDataBase
        public string Name { get; set; }

        [Range(20,40)]
        public int? Age { get; set; }

        public decimal Salary { get; set; }

        public string Address { get; set; }
       
        public string Phone { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        
        public DateTime DateOfCreation { get; set; }

       
        public DateTime HiringDate { get; set; }

        public Department Department { get; set; }
        public int ? DepartmentId { get; set; }


        public string ImageName { get; set; }
        


    }
}
