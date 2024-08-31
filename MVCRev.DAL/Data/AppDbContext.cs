﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCRev.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVCRev.DAL.Data
{
    public class AppDbContext : IdentityDbContext <ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options) { 

        }






        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
            base.OnModelCreating(modelBuilder);
        }


        

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }


    }
}
