using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Основной интерфейс репозитория, объединяющий чтение и запись
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : IDomainObject
    {
    }
}
