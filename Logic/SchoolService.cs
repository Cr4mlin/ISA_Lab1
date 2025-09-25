using System.Text.RegularExpressions;
using Model;

namespace Logic
{
    public class SchoolService
    {
        private readonly List<Course> _courses = new List<Course>();

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
        /// Валидация свойства курса
        /// </summary>
        /// <param name="requestedProperty">Свойство курса</param>
        /// <returns>Значение true если свойство проходит проверку, иначе falseф</returns>
        //public bool IsValidSearchProperties(string requestedProperty)
        //{
        //    List<string> pattern = ["название", "преподаватель", "идентификатор"];

        //    if (pattern.Contains(requestedProperty.ToLower().Trim()))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

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
            if (_courses.Any(c => c.Id.Equals(courseId, StringComparison.OrdinalIgnoreCase)))
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

            _courses.Add(newCourse);
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
            var existingCourse = GetCourseById(oldId);

            bool isActive = true;

            if (_courses.Any(c => c.Id.Equals(courseId, StringComparison.OrdinalIgnoreCase)) && courseId != existingCourse.Id)
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

            List<Course> filteredCourses = _courses.Where(course =>
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
            var courseToRemove = GetCourseById(courseId);
            if (courseToRemove != null)
            {
                return _courses.Remove(courseToRemove);
            }
            return false;
        }

        /// <summary>
        /// Возвращает список всех курсов
        /// </summary>
        /// <returns>List<Course></returns>
        public List<Course> GetAllCourses() => new List<Course>(_courses);

        /// <summary>
        /// Возвращает курс с таким же ID, какое было передано в функцию
        /// </summary>
        /// <returns>Course</returns>
        public Course GetCourseById(string courseId)
        {
            return _courses.FirstOrDefault(c => c.Id == courseId)
                    ?? throw new CourseNotFoundException(courseId);
        }

        /// <summary>
        /// Возвращает список активных курсов
        /// </summary>
        /// <returns>List<Course></returns>
        public List<Course> GetActiveCourses()
        {
            var courseList = _courses.Where(c => c.IsActive).ToList();
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

            var filterCourses = _courses.Where(c => c.Price >= minPrice && c.Price <= maxPrice).ToList();
            return filterCourses;
        }

        /// <summary>
        /// Функция для переключения состояния курса
        /// </summary>
        /// <param name="courseId">ID курса</param>
        public void ToggleCourseStatus(string courseId)
        {
            var course = GetCourseById(courseId);
            course.IsActive = !course.IsActive;
        }
    }
}
