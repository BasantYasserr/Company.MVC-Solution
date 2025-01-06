using Company.MVC.BLL.Interfaces;
using Company.MVC.DAL.Data.Contexts;
using Company.MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.MVC.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)  // ASK CLR To Create Object From AppDbContext - Dependency Injection.
        {
            _context = context;
        }


        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }
        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }
    }
}
