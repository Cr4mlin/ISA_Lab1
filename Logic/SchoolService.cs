using Logic.Search;
using Logic.Services;
using Model;

namespace Logic
{
    /// <summary>
    /// Фасад для управления курсами
    /// Делегирует вызовы специализированным сервисам
    /// </summary>
    public class SchoolService : ISchoolService
    {
        private readonly ICourseManagementService _managementService;
        private readonly ICourseQueryService _queryService;
        private readonly ICourseFilterService _filterService;
        private readonly ICourseSearchService _searchService;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса управления курсами
        /// </summary>
        /// <param name="managementService">Сервис управления курсами</param>
        /// <param name="queryService">Сервис запросов курсов</param>
        /// <param name="filterService">Сервис фильтрации курсов</param>
        /// <param name="searchService">Сервис поиска курсов</param>
        public SchoolService(
            ICourseManagementService managementService,
            ICourseQueryService queryService,
            ICourseFilterService filterService,
            ICourseSearchService searchService)
        {
            _managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
            _filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
            _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
        }

        /// <summary>
        /// Создаёт новый курс с заданными параметрами
        /// </summary>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        /// <returns>Созданный курс</returns>
        /// <exception cref="InvalidPriceException">Выбрасывается при отрицательном значении цены</exception>
        /// <exception cref="InvalidDurationException">Выбрасывается при отрицательном значении продолжительности</exception>
        /// <exception cref="InvalidTeacherNameException">Выбрасывается при невалидном имени преподавателя</exception>
        /// <exception cref="InvalidIsActiveException">Выбрасывается при неверном указании состояния</exception>
        public Course CreateCourse(string courseName, string description,
                                   int duration, decimal price, string teacherName, string status)
        {
            return _managementService.CreateCourse(courseName, description, duration, price, teacherName, status);
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
            return _managementService.UpdateCourse(courseId, courseName, description, duration, price, teacherName, status);
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
            var allCourses = _queryService.GetAllCourses();
            return _searchService.Search(allCourses, searchText, searchProperties);
        }

        /// <summary>
        /// Удаляет курс по указанному идентификатору
        /// </summary>
        /// <param name="courseId">Идентификатор курса для удаления</param>
        /// <returns>true если курс успешно удален, иначе false</returns>
        public bool DeleteCourse(int courseId)
        {
            return _managementService.DeleteCourse(courseId);
        }

        /// <summary>
        /// Возвращает список всех курсов
        /// </summary>
        /// <returns>Список всех курсов</returns>
        public List<Course> GetAllCourses() => _queryService.GetAllCourses();

        /// <summary>
        /// Находит курс по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор курса</param>
        /// <returns>Найденный курс или null, если не найден</returns>
        public Course GetCourseById(int id) => _queryService.GetCourseById(id);

        /// <summary>
        /// Возвращает список активных курсов
        /// </summary>
        /// <returns>Список активных курсов</returns>
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
            _managementService.ToggleCourseStatus(courseId);
        }
    }
}
