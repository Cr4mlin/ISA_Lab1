using Ninject.Modules;
using DataAccessLayer;
using Model;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using Logic.Validators;
using Logic.Converters;
using Logic.Search;
using Logic.Services;
using Logic.Factories;

namespace Logic
{
    /// <summary>
    /// Модуль конфигурации Ninject для регистрации зависимостей
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        private readonly string _connectionType;

        /// <summary>
        /// Инициализирует новый экземпляр модуля конфигурации
        /// </summary>
        /// <param name="connectionType">Тип подключения: "1" или "EntityFramework" для EF, иначе для Dapper</param>
        public SimpleConfigModule(string connectionType = "1")
        {
            _connectionType = connectionType;
        }

        /// <summary>
        /// Регистрирует зависимости в контейнере Ninject
        /// </summary>
        public override void Load()
        {
            const string connectionString = "Server=localhost\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;";

            if (_connectionType == "1" || _connectionType == "EntityFramework" || _connectionType == "EntityFrameWork")
            {
                // Регистрация ApplicationDbContext
                Bind<ApplicationDbContext>().ToSelf()
                    .InSingletonScope()
                    .WithConstructorArgument("options", new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseSqlServer(connectionString)
                        .Options);

                // Регистрация EntityRepository как Singleton
                Bind<IRepository<Course>>().To<EntityRepository<Course>>()
                    .InSingletonScope();
            }
            else
            {
                // Регистрация IDbConnection для Dapper
                Bind<IDbConnection>().To<SqlConnection>()
                    .InSingletonScope()
                    .WithConstructorArgument("connectionString", connectionString);

                // Регистрация DapperRepository как Singleton
                Bind<IRepository<Course>>().To<DapperRepository<Course>>()
                    .InSingletonScope();
            }

            // Регистрация валидаторов
            Bind<ITeacherNameValidator>().To<TeacherNameValidator>().InSingletonScope();
            Bind<IPriceValidator>().To<PriceValidator>().InSingletonScope();
            Bind<IDurationValidator>().To<DurationValidator>().InSingletonScope();
            Bind<IStatusValidator>().To<StatusValidator>().InSingletonScope();
            Bind<IPriceRangeValidator>().To<PriceRangeValidator>().InSingletonScope();

            // Регистрация композитного валидатора
            Bind<ICourseValidator>().To<CourseValidator>().InSingletonScope();

            // Регистрация конвертеров
            Bind<IStatusConverter>().To<StatusConverter>().InSingletonScope();

            // Регистрация фабрик
            Bind<ICourseFactory>().To<CourseFactory>().InSingletonScope();

            // Регистрация сервисов поиска
            Bind<ICourseSearchService>().To<CourseSearchService>().InSingletonScope();

            // Регистрация специализированных сервисов
            Bind<ICourseManagementService>().To<CourseManagementService>().InTransientScope();
            Bind<ICourseQueryService>().To<CourseQueryService>().InTransientScope();
            Bind<ICourseFilterService>().To<CourseFilterService>().InTransientScope();

            // Регистрация SchoolService (фасад)
            Bind<ISchoolService>().To<SchoolService>().InTransientScope();
            Bind<SchoolService>().To<SchoolService>().InTransientScope();
        }
    }
}
