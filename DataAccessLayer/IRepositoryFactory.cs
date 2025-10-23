using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Фабрика для создания репозиториев
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Создает репозиторий для работы с курсами
        /// </summary>
        /// <returns>Репозиторий для курсов</returns>
        IRepository<Course> CreateCourseRepository();
    }
}
