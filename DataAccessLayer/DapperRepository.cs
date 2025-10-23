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
    public class DapperRepository<T> : BaseRepository<T> where T : class, IDomainObject
    {
        private readonly IDbConnection _connection;
        private readonly SqlQueryBuilder<T> _queryBuilder;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория Dapper
        /// </summary>
        /// <param name="connection">Соединение с базой данных</param>
        /// <param name="queryBuilder">Построитель SQL-запросов</param>
        public DapperRepository(IDbConnection connection, SqlQueryBuilder<T> queryBuilder)
        {
            _connection = connection;
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Добавляет новую сущность в базу данных через Dapper
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        public override void Add(T entity)
        {
            ValidateEntity(entity);
            var sql = _queryBuilder.BuildInsertQuery();
            _connection.Execute(sql, entity);
        }

        /// <summary>
        /// Удаляет сущность по идентификатору через Dapper
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления</param>
        public override void Delete(int id)
        {
            if (!Exists(id))
                throw new ArgumentException($"Сущность с ID {id} не найдена");
            var sql = _queryBuilder.BuildDeleteQuery();
            _connection.Execute(sql, new { id });
        }

        /// <summary>
        /// Возвращает все сущности из базы данных через Dapper
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public override List<T> ReadAll()
        {
            var sql = _queryBuilder.BuildSelectAllQuery();
            return _connection.Query<T>(sql).ToList();
        }

        /// <summary>
        /// Находит сущность по идентификатору через Dapper
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null, если не найдена</returns>
        public override T ReadById(int id)
        {
            var sql = _queryBuilder.BuildSelectByIdQuery();
            return _connection.QuerySingleOrDefault<T>(sql, new { id });
        }

        /// <summary>
        /// Обновляет существующую сущность в базе данных через Dapper
        /// </summary>
        /// <param name="entity">Сущность для обновления</param>
        public override void Update(T entity)
        {
            ValidateEntity(entity);
            var sql = _queryBuilder.BuildUpdateQuery();
            _connection.Execute(sql, entity);
        }
    }
}

