using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс для записи данных в репозиторий
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IWriteRepository<T> where T : IDomainObject
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
        /// Обновляет существующую сущность в репозитории
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        void Update(T entity);
    }
}
