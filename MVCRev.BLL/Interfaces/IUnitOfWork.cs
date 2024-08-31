﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRev.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public  IDepartmentRepository DepartmentRepository { get;  }
        public IEmployeeRepository  EmployeeRepository { get;  }

        Task< int> Copelete();

    }
}
