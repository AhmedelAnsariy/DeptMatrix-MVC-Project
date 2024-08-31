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
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {

        public DepartmentRepository( AppDbContext context) : base(context) 
        {
            
        }


    }
}
