using Model;
using System.Collections.Generic;

namespace Logic.Services
{
    /// <summary>
    /// Интерфейс для сервиса поиска курсов
    /// </summary>
    public interface ICourseSearchService
    {
        /// <summary>
        /// Выполняет поиск курсов по заданному тексту и свойствам
        /// </summary>
        /// <param name="searchText">Текст для поиска</param>
        /// <param name="searchProperties">Список свойств для поиска</param>
        /// <returns>Список найденных курсов</returns>
        List<Course> SearchCourses(string searchText, List<string> searchProperties);
    }
}
