using Dapper;
using System.Data;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly string _connectionString;
        public DapperRepository(string connectionString);
        {
             _connectionString = connectionString;   
        }
        private IDbConnection Connection => new System.Data.SqlClient.SqlConnection(_connectionString);
        public void Add(T entity)
        {
            var tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id"); // Exclude Id for INSERT

            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));

            var sql = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(sql, entity);
            }
        }
        public void Delete(string id)
        {
            var tableName = typeof(T).Name;
            var sql = $"DELETE FROM {tableName} WHERE Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(sql, new { Id = id });
                dbConnection.Close();
            }

        }
        public void Update(T entity)
        {
            var tableName = typeof(T).Name;
            var proporties = typeof(T).GetProperties().Where(p => p.Name != "Id");
            var setClauses = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
            var sql = $"UPDATE {tableName} SET {setClauses} WHERE Id = @Id";
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(sql, entity);
            }
        }
        public List<T> ReadAll()
        {
            var tableName = typeof(T).Name;
            var sql = $"SELECT * FROM {tableName}";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>(sql).ToList();
            }
        }
        public T ReadById(string id)
        {
            var tableName = typeof(T).Name;
            var sql = $"SELECT * FROM {tableName} WHERE Id = @Id";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }
    }
}

