using Model;
using System.Collections.Generic;

namespace Logic.Services
{
    /// <summary>
    /// Интерфейс для сервиса фильтрации курсов
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
