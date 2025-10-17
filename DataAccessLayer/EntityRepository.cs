using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using SQLitePCL;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = ReadById(id);
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public List<T> ReadAll()
        {
            return _dbSet.ToList();
        }

        public T ReadById(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
                //?? throw new Exception($"Объект с Id={id} не найден");
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
