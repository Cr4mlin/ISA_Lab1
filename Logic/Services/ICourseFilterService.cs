using Model;

namespace Logic.Services
{
    /// <summary>
    /// Интерфейс для фильтрации курсов по различным критериям
    /// </summary>
    public interface ICourseFilterService
    {
        /// <summary>
        /// Возвращает список активных курсов
        /// </summary>
        /// <returns>Список активных курсов</returns>
        List<Course> GetActiveCourses();

        /// <summary>
        /// Возвращает список курсов в указанном ценовом диапазоне
        /// </summary>
        /// <param name="minPrice">Минимальная цена</param>
        /// <param name="maxPrice">Максимальная цена</param>
        /// <returns>Список курсов в заданном ценовом диапазоне</returns>
        List<Course> GetCoursesInPriceRange(decimal minPrice, decimal maxPrice);
    }
}
