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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity

    {
        private protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
           

        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
           
        }


        public void Update(T entity)
        {
            _context.Update(entity);
           
        }



        public async  Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(e=>e.Department).ToListAsync();
            }
            else
            {
                return _context.Set<T>().ToList();  
            }
           
        }

        public async Task<T> GetById(int id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            return data;
        }



        
    }
}
