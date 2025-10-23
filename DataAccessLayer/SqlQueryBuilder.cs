using System.Reflection;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Абстрактный класс для построения SQL-запросов
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public abstract class SqlQueryBuilder<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Получает имя таблицы для типа сущности
        /// </summary>
        /// <returns>Имя таблицы</returns>
        protected virtual string GetTableName()
        {
            return typeof(T).Name + "s";
        }

        /// <summary>
        /// Строит SQL-запрос для вставки
        /// </summary>
        /// <returns>SQL-запрос для вставки</returns>
        public abstract string BuildInsertQuery();

        /// <summary>
        /// Строит SQL-запрос для обновления
        /// </summary>
        /// <returns>SQL-запрос для обновления</returns>
        public abstract string BuildUpdateQuery();

        /// <summary>
        /// Строит SQL-запрос для удаления
        /// </summary>
        /// <returns>SQL-запрос для удаления</returns>
        public virtual string BuildDeleteQuery()
        {
            return $"DELETE FROM {GetTableName()} WHERE Id = @id";
        }

        /// <summary>
        /// Строит SQL-запрос для получения всех записей
        /// </summary>
        /// <returns>SQL-запрос для получения всех записей</returns>
        public virtual string BuildSelectAllQuery()
        {
            return $"SELECT * FROM {GetTableName()}";
        }

        /// <summary>
        /// Строит SQL-запрос для получения записи по ID
        /// </summary>
        /// <returns>SQL-запрос для получения записи по ID</returns>
        public virtual string BuildSelectByIdQuery()
        {
            return $"SELECT * FROM {GetTableName()} WHERE Id = @id";
        }
    }
}
