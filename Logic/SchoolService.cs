using System.Text.RegularExpressions;
using DataAccessLayer;
using Model;

namespace Logic
{
    public class SchoolService
    {
        private readonly IRepository<Course> _repository;
        /// <summary>
        /// Валидация имени преподавателя
        /// </summary>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <returns>Значение true если имя проходит проверку, иначе false</returns>
        private bool IsValidTeacherName(string teacherName)
        {
            if (string.IsNullOrWhiteSpace(teacherName))
                return false;

            string pattern = @"^[a-zA-Zа-яА-ЯёЁ\s\-']+$";
            if (!Regex.IsMatch(teacherName, pattern))
                return false;

            if (teacherName.StartsWith("-") || teacherName.EndsWith("-") ||
                teacherName.StartsWith("'") || teacherName.EndsWith("'"))
                return false;

            if (teacherName.Contains("--") || teacherName.Contains("  "))
                return false;

            return true;
        }

        /// <summary>
        /// Валидация состояния курса
        /// </summary>
        /// <param name="status">Состояние(активен или нет)</param>
        /// <returns>Значение true если статус проходит проверку, иначе false</returns>
        public bool IsValidIsActive(string status)
        {
            if (string.IsNullOrEmpty(status))
                return false;

            string pattern = @"да|нет";
            if (Regex.IsMatch(status, pattern))
                return true;

            return false;
        }

        /// <summary>
        /// Инициализирует новый экземпляр сервиса управления курсами
        /// </summary>
        /// <param name="repository">Репозиторий для работы с курсами</param>
        public SchoolService(IRepository<Course> repository)
        {
            _repository = repository;
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
            if (price < 0)
            {
                throw new InvalidPriceException(price);
            }

            if (duration < 0)
            {
                throw new InvalidDurationException(duration);
            }

            if (!IsValidTeacherName(teacherName))
            {
                throw new InvalidTeacherNameException(teacherName);
            }

            if (!IsValidIsActive(status))
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

            if (price < 0)
            {
                throw new InvalidPriceException(price);
            }

            if (duration < 0)
            {
                throw new InvalidDurationException(duration);
            }

            if (!IsValidTeacherName(teacherName))
            {
                throw new InvalidTeacherNameException(teacherName);
            }

            if (!IsValidIsActive(status))
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
            var allCourses = _repository.ReadAll();

            var courseList = allCourses.Where(c => c.IsActive).ToList();
            return courseList;
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

            var filterCourses = allCourses.Where(c => c.Price >= minPrice && c.Price <= maxPrice).ToList();
            return filterCourses;
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
