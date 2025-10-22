using Dapper;
using System.Data;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly IDbConnection _connection;
        /// <summary>
        /// Инициализирует новый экземпляр репозитория Dapper
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        public DapperRepository(IDbConnection connectionString)
        {
            _connection = connectionString;
        }

        /// <summary>
        /// Добавляет новую сущность в базу данных через Dapper
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        public void Add(T entity)
        {
            var tableName = typeof(T).Name + "s";
            var sql = $"INSERT INTO {tableName} (Name, Description, Duration, Price, TeacherName, IsActive) VALUES (@Name, @Description, @Duration, @Price, @TeacherName, @IsActive)";
            _connection.Execute(sql, entity);
        }

        /// <summary>
        /// Удаляет сущность по идентификатору через Dapper
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления</param>
        public void Delete(int id)
        {
            var tableName = typeof(T).Name + "s";
            _connection.Execute($"DELETE FROM {tableName} WHERE Id = @id", new { id });
        }

        /// <summary>
        /// Возвращает все сущности из базы данных через Dapper
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public List<T> ReadAll()
        {
            var tableName = typeof(T).Name + "s";
            return _connection.Query<T>($"SELECT * FROM {tableName}").ToList();
        }

        /// <summary>
        /// Находит сущность по идентификатору через Dapper
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null, если не найдена</returns>
        public T ReadById(int id)
        {
            var tableName = typeof(T).Name + "s";
            return _connection.QuerySingleOrDefault<T>($"SELECT * FROM {tableName} WHERE Id = @id", new { id });
        }

        /// <summary>
        /// Обновляет существующую сущность в базе данных через Dapper
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        public void Update(T entity)
        {
            var tableName = typeof(T).Name + "s";
            var sql = $@"UPDATE {tableName} 
                         SET Name=@Name, Description=@Description, Duration=@Duration, 
                             Price=@Price, TeacherName=@TeacherName, IsActive=@IsActive 
                         WHERE Id=@Id";
            _connection.Execute(sql, entity);
        }
    }
}

