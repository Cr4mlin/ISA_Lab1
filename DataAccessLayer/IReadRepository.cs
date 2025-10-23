using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс для чтения данных из репозитория
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IReadRepository<T> where T : IDomainObject
    {
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
    }
}
