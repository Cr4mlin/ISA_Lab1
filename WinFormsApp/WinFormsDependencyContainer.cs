using DataAccessLayer;
using Logic;
using Logic.Validation;
using Logic.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace WinFormsApp
{
    /// <summary>
    /// Контейнер зависимостей для WinForms приложения
    /// </summary>
    public static class WinFormsDependencyContainer
    {
        private const string ConnectionString = "Server=DESKTOP-VLCID23\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Создает и настраивает все зависимости для WinForms приложения
        /// </summary>
        /// <param name="useEntityFramework">true для Entity Framework, false для Dapper</param>
        /// <returns>Настроенный SchoolService</returns>
        public static SchoolService CreateSchoolService(bool useEntityFramework = true)
        {
            // Создаем валидаторы
            var teacherNameValidator = new TeacherNameValidator();
            var priceValidator = new PriceValidator();
            var durationValidator = new DurationValidator();
            var statusValidator = new StatusValidator();

            // Создаем репозиторий через фабрику
            var repositoryFactory = new RepositoryFactory(useEntityFramework, ConnectionString);
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
    }
}
