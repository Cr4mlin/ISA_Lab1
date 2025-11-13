using Model;

namespace Logic.Search
{
    /// <summary>
    /// Интерфейс для поиска курсов
    /// </summary>
    public interface ICourseSearchService
    {
        /// <summary>
        /// Выполняет поиск курсов по заданному тексту и свойствам
        /// </summary>
        /// <param name="courses">Список курсов для поиска</param>
        /// <param name="searchText">Текст для поиска</param>
        /// <param name="searchProperties">Список свойств для поиска</param>
        /// <returns>Список найденных курсов</returns>
        /// <exception cref="ArgumentException">Выбрасывается если поле поиска пустое или не выбрано ни одного свойства</exception>
        List<Course> Search(List<Course> courses, string searchText, List<string> searchProperties);
    }
}

