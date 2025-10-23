using Model;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Services
{
    /// <summary>
    /// Сервис для фильтрации курсов
    /// </summary>
    public class CourseFilterService : ICourseFilterService
    {
        private readonly IRepository<Course> _repository;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса фильтрации курсов
        /// </summary>
        /// <param name="repository">Репозиторий для работы с курсами</param>
        public CourseFilterService(IRepository<Course> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Возвращает список активных курсов
        /// </summary>
        /// <returns>Список активных курсов</returns>
        public List<Course> GetActiveCourses()
        {
            var allCourses = _repository.ReadAll();
            return allCourses.Where(c => c.IsActive).ToList();
        }

        /// <summary>
        /// Возвращает список курсов в указанном ценовом диапазоне
        /// </summary>
        /// <param name="minPrice">Минимальная цена</param>
        /// <param name="maxPrice">Максимальная цена</param>
        /// <returns>Список курсов в заданном ценовом диапазоне</returns>
        /// <exception cref="InvalidPriceRangeException">Выбрасывается при неверном ценовом диапазоне</exception>
        public List<Course> GetCoursesInPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
            {
                throw new InvalidPriceRangeException(minPrice, maxPrice);
            }
            
            var allCourses = _repository.ReadAll();
            return allCourses.Where(c => c.Price >= minPrice && c.Price <= maxPrice).ToList();
        }
    }
}
