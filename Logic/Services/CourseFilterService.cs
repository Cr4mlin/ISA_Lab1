using DataAccessLayer;
using Logic.Validators;
using Model;
using Logic.Exceptions;

namespace Logic.Services
{
    /// <summary>
    /// Сервис для фильтрации курсов по различным критериям
    /// Отвечает за поиск курсов по определенным условиям
    /// </summary>
    public class CourseFilterService : ICourseFilterService
    {
        private readonly IRepository<Course> _repository;
        private readonly IPriceRangeValidator _priceRangeValidator;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса фильтрации курсов
        /// </summary>
        /// <param name="repository">Репозиторий для работы с курсами</param>
        /// <param name="priceRangeValidator">Валидатор ценового диапазона</param>
        public CourseFilterService(IRepository<Course> repository, IPriceRangeValidator priceRangeValidator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _priceRangeValidator = priceRangeValidator ?? throw new ArgumentNullException(nameof(priceRangeValidator));
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
            if (!_priceRangeValidator.IsValid(minPrice, maxPrice))
            {
                throw new InvalidPriceRangeException(minPrice, maxPrice);
            }

            var allCourses = _repository.ReadAll();
            return allCourses.Where(c => c.Price >= minPrice && c.Price <= maxPrice).ToList();
        }
    }
}
