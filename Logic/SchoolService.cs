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

        public SchoolService(IRepository<Course> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Создаёт курс с заданными параметрами.
        /// </summary>
        /// <param name="courseName">Название курса</param>
        /// <param name="descripton">Описание курса</param>
        /// <param name="courseId">ID курса</param>
        /// <param name="duration">Длительность курса</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса</param>
        /// <exception cref="CourseIdExistsException">Выбрасывается при дублировании кода курса</exception>
        /// <exception cref="InvalidPriceException">Выбрасывается при отрицательном значении цены</exception>
        /// <exception cref="InvalidDurationException">Выбрасывается при отрицательном значении продолжительности</exception>
        /// <exception cref="InvalidTeacherNameException">Выбрасывается при невозможном имени преподавателя</exception>
        /// <exception cref="InvalidIsActiveException">Выбрасывается при неверном указании состояния</exception>
        public Course CreateCourse(string courseName, string descripton, string courseId,
                                   int duration, decimal price, string teacherName, string status)
        {
            if (_repository.ReadById(courseId) != null)
            {
                throw new CourseIdExistsException(courseId);
            }

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
                Id = courseId,
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
        /// Функция для изменения существующего курса (сущности)
        /// </summary>
        /// <param name="oldId">Старое ID курса</param>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="courseId">ID курса</param>
        /// <param name="duration">Длительность курса</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса</param>
        /// <exception cref="CourseIdExistsException">Выбрасывается при дублировании кода курса</exception>
        /// <exception cref="InvalidPriceException">Выбрасывается при отрицательном значении цены</exception>
        /// <exception cref="InvalidDurationException">Выбрасывается при отрицательном значении продолжительности</exception>
        /// <exception cref="InvalidTeacherNameException">Выбрасывается при невозможном имени преподавателя</exception>
        /// <exception cref="InvalidIsActiveException">Выбрасывается при неверном указании состояния</exception>
        public Course UpdateCourse(string oldId, string courseName, string description, string courseId,
                                  int duration, decimal price, string teacherName, string status)
        {
            var existingCourse = _repository.ReadById(oldId);

            bool isActive = true;

            if (_repository.ReadById(courseId) != null)
            {
                throw new CourseIdExistsException(courseId);
            }

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
            existingCourse.Id = courseId;
            existingCourse.Description = description;
            existingCourse.Duration = duration;
            existingCourse.Price = price;
            existingCourse.TeacherName = teacherName;
            existingCourse.IsActive = isActive;

            _repository.Update(existingCourse);

            return existingCourse;
        }

        /// <summary>
        /// Возвращает список курсов найденных по выбранным свойствам
        /// </summary>
        /// <param name="searchText">Текст поиска</param>
        /// <param name="searchProperties">Свойства по которым производится поиск</param>
        /// <returns>List<Course></returns>
        /// <exception cref="ArgumentException">Вылазит если поле пустое</exception>
        /// <exception cref="PropertyNotFoundException">Выскакивает если свойство не найдено</exception>
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

            //foreach (string property in searchProperties)
            //{
            //    if (!IsValidSearchProperties(property))
            //    {
            //        throw new PropertyNotFoundException(property);
            //    }
            //}

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
        /// Удаляет курс
        /// </summary>
        /// <param name="courseId">ID курса</param>
        /// <returns>Если курс удалён, то возвращает true, иначе false</returns>
        public bool DeleteCourse(string courseId)
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
        /// Возвращает курс с таким же ID, какое было передано в функцию
        /// </summary>
        /// <returns>Course</returns>
        public Course GetCourseById(string id) => _repository.ReadById(id);

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
        /// Возвращает список всех курсов входящих в указанный ценовой диапазон
        /// </summary>
        /// <returns>List<Course></returns>
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
        /// Функция для переключения состояния курса
        /// </summary>
        /// <param name="courseId">ID курса</param>
        public void ToggleCourseStatus(string courseId)
        {
            var course = _repository.ReadById(courseId);
            course.IsActive = !course.IsActive;
        }
    }
}
