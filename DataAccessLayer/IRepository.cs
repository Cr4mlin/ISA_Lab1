using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : IDomainObject
    {
        /// <summary>
        /// Добавляет новую сущность в репозиторий
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        void Add(T entity);
        
        /// <summary>
        /// Удаляет сущность по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления</param>
        void Delete(int id);
        
        /// <summary>
        /// Возвращает список всех сущностей из репозитория
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        List<T> ReadAll();
        
        /// <summary>
        /// Находит сущность по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null, если не найдена</returns>
        T ReadById(int id);
        
        /// <summary>
        /// Обновляет существующую сущность в репозитории
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        void Update(T entity);
    }
}
