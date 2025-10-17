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
        public DapperRepository(IDbConnection connectionString)
        {
            _connection = connectionString;
        }

        public void Add(T entity)
        {
            var tableName = typeof(T).Name + "s";
            var sql = $"INSERT INTO {tableName} (Name, Description, Duration, Price, TeacherName, IsActive) VALUES (@Name, @Description, @Duration, @Price, @TeacherName, @IsActive)";
            _connection.Execute(sql, entity);
        }

        public void Delete(int id)
        {
            var tableName = typeof(T).Name + "s";
            _connection.Execute($"DELETE FROM {tableName} WHERE Id = @id", new { id });
        }

        public List<T> ReadAll()
        {
            var tableName = typeof(T).Name + "s";
            return _connection.Query<T>($"SELECT * FROM {tableName}").ToList();
        }

        public T ReadById(int id)
        {
            var tableName = typeof(T).Name + "s";
            return _connection.QuerySingleOrDefault<T>($"SELECT * FROM {tableName} WHERE Id = @id", new { id });
        }

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

