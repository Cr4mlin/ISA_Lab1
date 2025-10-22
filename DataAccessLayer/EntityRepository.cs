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

        /// <summary>
        /// Инициализирует новый экземпляр репозитория Entity Framework
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Добавляет новую сущность в базу данных через Entity Framework
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет сущность по идентификатору через Entity Framework
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления</param>
        public void Delete(int id)
        {
            var entity = ReadById(id);
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Возвращает все сущности из базы данных через Entity Framework
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public List<T> ReadAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Находит сущность по идентификатору через Entity Framework
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null, если не найдена</returns>
        public T ReadById(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
                //?? throw new Exception($"Объект с Id={id} не найден");
        }

        /// <summary>
        /// Обновляет существующую сущность в базе данных через Entity Framework
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
