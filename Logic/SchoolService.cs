using System.Text.RegularExpressions;
using System.Text;
using Model;

namespace Logic
{
    public class SchoolService
    {
        private readonly List<Course> _courses = new List<Course>();

        /// <summary>
        /// Валидация имени преподавателя
        /// </summary>
        /// <param name="teacherName"></param>
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
        /// <param name="teacherName"></param>
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
        /// Функция для создания курса (сущности)
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="descripton"></param>
        /// <param name="courseId"></param>
        /// <param name="duration"></param>
        /// <param name="price"></param>
        /// <param name="teacher"></param>
        /// <param name="status"></param>
        /// <exception cref="CourseIdExistsException"></exception>
        /// <exception cref="InvalidPriceException"></exception>
        /// <exception cref="InvalidDurationException"></exception>
        /// <exception cref="InvalidTeacherNameException"></exception>
        /// <exception cref="InvalidIsActiveException"></exception>
        public Course CreateCourse(string courseName, string descripton, string courseId,
                                   int duration, decimal price, string teacher, string status)
        {
            if (_courses.Any(c => c.CourseId.Equals(courseId, StringComparison.OrdinalIgnoreCase)))
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

            if (!IsValidTeacherName(teacher))
            {
                throw new InvalidTeacherNameException(teacher);
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
                CourseName = courseName,
                CourseId = courseId,
                Description = descripton,
                Duration = duration,
                Price = price,
                Teacher = teacher,
                IsActive = isActive
            };

            _courses.Add(newCourse);
            return newCourse;
        }

        /// <summary>
        /// Функция для изменения существующего курса (сущности)
        /// </summary>
        /// <param name="oldId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="id"></param>
        /// <param name="duration"></param>
        /// <param name="price"></param>
        /// <param name="teacher"></param>
        /// <param name="status"></param>
        /// <exception cref="CourseIdExistsException"></exception>
        /// <exception cref="InvalidPriceException"></exception>
        /// <exception cref="InvalidDurationException"></exception>
        /// <exception cref="InvalidTeacherNameException"></exception>
        /// <exception cref="InvalidIsActiveException"></exception>
        public Course UpdateCourse(string oldId, string name, string description, string id,
                                  int duration, decimal price, string teacher, string status)
        {
            var existingCourse = GetCourseById(oldId);

            bool isActive = true;

            if (_courses.Any(c => c.CourseId.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                throw new CourseIdExistsException(existingCourse.CourseId);
            }

            if (price < 0)
            {
                throw new InvalidPriceException(price);
            }

            if (duration < 0)
            {
                throw new InvalidDurationException(duration);
            }

            if (!IsValidTeacherName(teacher))
            {
                throw new InvalidTeacherNameException(teacher);
            }

            if (!IsValidIsActive(status))
            {
                throw new InvalidIsActiveException(status);
            }

            if (status == "да") { isActive = true; }
            if (status == "нет") { isActive = false; }

            existingCourse.CourseName = name;
            existingCourse.CourseId = id;
            existingCourse.Description = description;
            existingCourse.Duration = duration;
            existingCourse.Price = price;
            existingCourse.Teacher = teacher;
            existingCourse.IsActive = isActive;

            return existingCourse;
        }

        /// <summary>
        /// Функция для удаления курса
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Если курс удалён, то возвращает true, иначе false</returns>
        public bool DeleteCourse(string id)
        {
            var courseToRemove = GetCourseById(id);
            if (courseToRemove != null)
            {
                return _courses.Remove(courseToRemove);
            }
            return false;
        }

        /// <summary>
        /// Возвращает список всех курсов
        /// </summary>
        /// <returns>string</returns>
        public List<Course> GetAllCourses() => new List<Course>(_courses);

        /// <summary>
        /// Возвращает курс с таким же ID, какое было передано в функцию
        /// </summary>
        /// <returns>Course</returns>
        public Course GetCourseById(string id)
        {
            return _courses.FirstOrDefault(c => c.CourseId == id)
                    ?? throw new CourseNotFoundException(id);
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
        /// <returns>string</returns>
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
        /// <param name="id">ID курса</param>
        public void ToggleCourseStatus(string id)
        {
            var course = GetCourseById(id);
            course.IsActive = !course.IsActive;
        }
    }
}
