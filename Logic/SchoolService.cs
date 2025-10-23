using DataAccessLayer;
using Model;
using Logic.Validation;
using Logic.Services;

namespace Logic
{
    public class SchoolService
    {
        private readonly IRepository<Course> _repository;
        private readonly IValidator<string> _teacherNameValidator;
        private readonly IValidator<decimal> _priceValidator;
        private readonly IValidator<int> _durationValidator;
        private readonly IValidator<string> _statusValidator;
        private readonly ICourseSearchService _searchService;
        private readonly ICourseFilterService _filterService;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса управления курсами
        /// </summary>
        /// <param name="repository">Репозиторий для работы с курсами</param>
        /// <param name="teacherNameValidator">Валидатор имени преподавателя</param>
        /// <param name="priceValidator">Валидатор цены</param>
        /// <param name="durationValidator">Валидатор продолжительности</param>
        /// <param name="statusValidator">Валидатор статуса</param>
        /// <param name="searchService">Сервис поиска курсов</param>
        /// <param name="filterService">Сервис фильтрации курсов</param>
        public SchoolService(
            IRepository<Course> repository,
            IValidator<string> teacherNameValidator,
            IValidator<decimal> priceValidator,
            IValidator<int> durationValidator,
            IValidator<string> statusValidator,
            ICourseSearchService searchService,
            ICourseFilterService filterService)
        {
            _repository = repository;
            _teacherNameValidator = teacherNameValidator;
            _priceValidator = priceValidator;
            _durationValidator = durationValidator;
            _statusValidator = statusValidator;
            _searchService = searchService;
            _filterService = filterService;
        }

        /// <summary>
        /// Создаёт новый курс с заданными параметрами
        /// </summary>
        /// <param name="courseName">Название курса</param>
        /// <param name="descripton">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        /// <returns>Созданный курс</returns>
        /// <exception cref="InvalidPriceException">Выбрасывается при отрицательном значении цены</exception>
        /// <exception cref="InvalidDurationException">Выбрасывается при отрицательном значении продолжительности</exception>
        /// <exception cref="InvalidTeacherNameException">Выбрасывается при невалидном имени преподавателя</exception>
        /// <exception cref="InvalidIsActiveException">Выбрасывается при неверном указании состояния</exception>
        public Course CreateCourse(string courseName, string descripton,
                                   int duration, decimal price, string teacherName, string status)
        {
            if (!_priceValidator.IsValid(price))
            {
                throw new InvalidPriceException(price);
            }

            if (!_durationValidator.IsValid(duration))
            {
                throw new InvalidDurationException(duration);
            }

            if (!_teacherNameValidator.IsValid(teacherName))
            {
                throw new InvalidTeacherNameException(teacherName);
            }

            if (!_statusValidator.IsValid(status))
            {
                throw new InvalidIsActiveException(status);
            }

            bool isActive = true;

            if (status == "да") { isActive = true; }
            if (status == "нет") { isActive = false; }

            var newCourse = new Course
            {
                Name = courseName,
                Description = descripton,
                Duration = duration,
                Price = price,
                TeacherName = teacherName,
                IsActive = isActive
            };

            _repository.Add(newCourse);
            return newCourse;
        }

        /// <summary>
        /// Обновляет существующий курс с заданными параметрами
        /// </summary>
        /// <param name="courseId">ID курса для обновления</param>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        /// <returns>Обновленный курс</returns>
        /// <exception cref="InvalidPriceException">Выбрасывается при отрицательном значении цены</exception>
        /// <exception cref="InvalidDurationException">Выбрасывается при отрицательном значении продолжительности</exception>
        /// <exception cref="InvalidTeacherNameException">Выбрасывается при невалидном имени преподавателя</exception>
        /// <exception cref="InvalidIsActiveException">Выбрасывается при неверном указании состояния</exception>
        public Course UpdateCourse(int courseId, string courseName, string description,
                                  int duration, decimal price, string teacherName, string status)
        {
            var existingCourse = _repository.ReadById(courseId);

            bool isActive = true;

            if (!_priceValidator.IsValid(price))
            {
                throw new InvalidPriceException(price);
            }

            if (!_durationValidator.IsValid(duration))
            {
                throw new InvalidDurationException(duration);
            }

            if (!_teacherNameValidator.IsValid(teacherName))
            {
                throw new InvalidTeacherNameException(teacherName);
            }

            if (!_statusValidator.IsValid(status))
            {
                throw new InvalidIsActiveException(status);
            }

            if (status == "да") { isActive = true; }
            if (status == "нет") { isActive = false; }

            existingCourse.Name = courseName;
            existingCourse.Description = description;
            existingCourse.Duration = duration;
            existingCourse.Price = price;
            existingCourse.TeacherName = teacherName;
            existingCourse.IsActive = isActive;

            _repository.Update(existingCourse);

            return existingCourse;
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
            return _searchService.SearchCourses(searchText, searchProperties);
        }

        /// <summary>
        /// Удаляет курс по указанному идентификатору
        /// </summary>
        /// <param name="courseId">Идентификатор курса для удаления</param>
        /// <returns>true если курс успешно удален, иначе false</returns>
        public bool DeleteCourse(int courseId)
        {
            var courseToRemove = _repository.ReadById(courseId);
            if (courseToRemove != null)
            {
                _repository.Delete(courseId);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает список всех курсов
        /// </summary>
        /// <returns>List<Course></returns>
        public List<Course> GetAllCourses() => _repository.ReadAll();

        /// <summary>
        /// Находит курс по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор курса</param>
        /// <returns>Найденный курс или null, если не найден</returns>
        public Course GetCourseById(int id) => _repository.ReadById(id);

        /// <summary>
        /// Возвращает список активных курсов
        /// </summary>
        /// <returns>List<Course></returns>
        public List<Course> GetActiveCourses()
        {
            return _filterService.GetActiveCourses();
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
            return _filterService.GetCoursesInPriceRange(minPrice, maxPrice);
        }

        /// <summary>
        /// Переключает статус активности курса (активный/неактивный)
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        public void ToggleCourseStatus(int courseId)
        {
            var course = _repository.ReadById(courseId);
            course.IsActive = !course.IsActive;
            _repository.Update(course);
        }
    }
}
