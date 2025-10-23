using Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Фабрика для создания репозиториев
    /// </summary>
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly bool _useEntityFramework;
        private readonly string _connectionString;

        /// <summary>
        /// Инициализирует новый экземпляр фабрики репозиториев
        /// </summary>
        /// <param name="useEntityFramework">true для Entity Framework, false для Dapper</param>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        public RepositoryFactory(bool useEntityFramework, string connectionString)
        {
            _useEntityFramework = useEntityFramework;
            _connectionString = connectionString;
        }

        /// <summary>
        /// Создает репозиторий для работы с курсами
        /// </summary>
        /// <returns>Репозиторий для курсов</returns>
        public IRepository<Course> CreateCourseRepository()
        {
            if (_useEntityFramework)
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(_connectionString)
                    .Options;

                var context = new ApplicationDbContext(options);
                return new EntityRepository<Course>(context);
            }
            else
            {
                var connection = new SqlConnection(_connectionString);
                var queryBuilder = new CourseSqlQueryBuilder();
                return new DapperRepository<Course>(connection, queryBuilder);
            }
        }
    }
}
