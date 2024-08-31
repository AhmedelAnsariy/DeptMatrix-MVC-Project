using Microsoft.EntityFrameworkCore;
using MVCRev.BLL.Interfaces;
using MVCRev.DAL.Data;
using MVCRev.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRev.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {

        public EmployeeRepository(AppDbContext context) : base(context) { }

      

        public IEnumerable<Employee> GetByName(string Name)
        {
            return _context.Employees
                           .Where(E => E.Name.ToLower().Contains(Name.ToLower()))
                           .Include(E=>E.Department)
                           .ToList();
        }



    }
}
