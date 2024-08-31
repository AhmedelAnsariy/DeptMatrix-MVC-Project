using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRev.DAL.Models
{
    // Extra Data I Want to Put It in Registration Form 

    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }

        public string NationalNumber {  get; set; }

        public bool IsAgree { get; set; }
        

    }
}
