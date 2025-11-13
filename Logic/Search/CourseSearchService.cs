using Model;
using System.Reflection;

namespace Logic.Search
{
    /// <summary>
    /// Реализация сервиса поиска курсов
    /// </summary>
    public class CourseSearchService : ICourseSearchService
    {
        private readonly Dictionary<string, string> _fieldNameMap;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса поиска курсов
        /// </summary>
        public CourseSearchService()
        {
            _fieldNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Название", "Name" },
                { "Преподаватель", "TeacherName" },
                { "Идентификатор", "Id" }
            };
        }

        /// <summary>
        /// Выполняет поиск курсов по заданному тексту и свойствам
        /// </summary>
        /// <param name="courses">Список курсов для поиска</param>
        /// <param name="searchText">Текст для поиска</param>
        /// <param name="searchProperties">Список свойств для поиска</param>
        /// <returns>Список найденных курсов</returns>
        /// <exception cref="ArgumentException">Выбрасывается если поле поиска пустое или не выбрано ни одного свойства</exception>
        public List<Course> Search(List<Course> courses, string searchText, List<string> searchProperties)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                throw new ArgumentException("Поле для поиска не может быть пустым.");
            }

            if (searchProperties == null || !searchProperties.Any())
            {
                throw new ArgumentException("Не выбрано ни одно свойство для поиска");
            }

            searchText = searchText.Trim();
            var courseType = typeof(Course);

            List<Course> filteredCourses = courses.Where(course =>
            {
                foreach (string searchProperty in searchProperties)
                {
                    if (!_fieldNameMap.TryGetValue(searchProperty, out string? actualProperty))
                        continue;

                    var property = courseType.GetProperty(actualProperty);
                    if (property == null)
                        continue;

                    var value = property.GetValue(course)?.ToString();

                    if (value != null && value.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return true;
                    }
                }
                return false;
            }).ToList();

            return filteredCourses;
        }
    }
}

