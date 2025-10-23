using DataAccessLayer;
using Logic;
using Logic.Validation;
using Logic.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp
{
    /// <summary>
    /// Контейнер зависимостей для настройки DI
    /// </summary>
    public static class DependencyContainer
    {
        private const string ConnectionString = "Server=DESKTOP-VLCID23\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Создает и настраивает все зависимости для приложения
        /// </summary>
        /// <param name="configuration">Конфигурация приложения</param>
        /// <returns>Настроенный SchoolService</returns>
        public static SchoolService CreateSchoolService(IConfiguration configuration)
        {
            // Создаем валидаторы
            var teacherNameValidator = new TeacherNameValidator();
            var priceValidator = new PriceValidator();
            var durationValidator = new DurationValidator();
            var statusValidator = new StatusValidator();

            // Создаем репозиторий через фабрику
            var repositoryFactory = new RepositoryFactory(configuration.UseEntityFramework, configuration.ConnectionString);
            var repository = repositoryFactory.CreateCourseRepository();

            // Создаем сервисы
            var searchService = new CourseSearchService(repository);
            var filterService = new CourseFilterService(repository);

            // Создаем основной сервис
            return new SchoolService(
                repository,
                teacherNameValidator,
                priceValidator,
                durationValidator,
                statusValidator,
                searchService,
                filterService);
        }

        /// <summary>
        /// Создает и настраивает все зависимости для приложения
        /// </summary>
        /// <param name="useEntityFramework">true для Entity Framework, false для Dapper</param>
        /// <returns>Настроенный SchoolService</returns>
        public static SchoolService CreateSchoolService(bool useEntityFramework = true)
        {
            var configuration = new AppConfiguration(ConnectionString, useEntityFramework);
            return CreateSchoolService(configuration);
        }
    }
}
