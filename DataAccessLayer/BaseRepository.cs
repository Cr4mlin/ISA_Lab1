using Model;
using System;

namespace DataAccessLayer
{
    /// <summary>
    /// Базовый класс для репозиториев с общим поведением
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Проверяет существование сущности по ID
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>true если сущность существует, иначе false</returns>
        protected virtual bool Exists(int id)
        {
            return ReadById(id) != null;
        }

        /// <summary>
        /// Валидирует сущность перед операциями
        /// </summary>
        /// <param name="entity">Сущность для валидации</param>
        /// <exception cref="ArgumentNullException">Выбрасывается если сущность равна null</exception>
        protected virtual void ValidateEntity(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Сущность не может быть null");
        }

        /// <summary>
        /// Абстрактные методы для реализации в наследниках
        /// </summary>
        public abstract void Add(T entity);
        public abstract void Delete(int id);
        public abstract List<T> ReadAll();
        public abstract T ReadById(int id);
        public abstract void Update(T entity);
    }
}
