using Company.MVC.BLL.Interfaces;
using Company.MVC.DAL.Data.Contexts;
using Company.MVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)  // ASK CLR To Create Object From AppDbContext - Dependency Injection.
        {
            _context = context;
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.WorkFor).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }
        
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        
        public async Task<int> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
