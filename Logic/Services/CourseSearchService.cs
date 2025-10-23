using Model;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Services
{
    /// <summary>
    /// Сервис для поиска курсов
    /// </summary>
    public class CourseSearchService : ICourseSearchService
    {
        private readonly IRepository<Course> _repository;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса поиска курсов
        /// </summary>
        /// <param name="repository">Репозиторий для работы с курсами</param>
        public CourseSearchService(IRepository<Course> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Выполняет поиск курсов по заданному тексту и свойствам
        /// </summary>
        /// <param name="searchText">Текст для поиска</param>
        /// <param name="searchProperties">Список свойств для поиска</param>
        /// <returns>Список найденных курсов</returns>
        /// <exception cref="ArgumentException">Выбрасывается если поле поиска пустое или не выбрано ни одного свойства</exception>
        public List<Course> SearchCourses(string searchText, List<string> searchProperties)
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

            var fieldNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Название", "Name" },
                { "Преподаватель", "TeacherName" },
                { "Идентификатор", "Id" }
            };

            var allCourses = _repository.ReadAll();
            List<Course> filteredCourses = allCourses.Where(course =>
            {
                foreach (string searchProperty in searchProperties)
                {
                    string actuallyproperty = fieldNameMap[searchProperty];
                    var property = courseType.GetProperty(actuallyproperty);

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
