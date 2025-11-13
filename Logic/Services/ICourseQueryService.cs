using Model;

namespace Logic.Services
{
    /// <summary>
    /// Интерфейс для выполнения запросов и получения информации о курсах
    /// </summary>
    public interface ICourseQueryService
    {
        /// <summary>
        /// Возвращает список всех курсов
        /// </summary>
        /// <returns>Список всех курсов</returns>
        List<Course> GetAllCourses();

        /// <summary>
        /// Находит курс по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор курса</param>
        /// <returns>Найденный курс или null, если не найден</returns>
        Course GetCourseById(int id);
    }
}
