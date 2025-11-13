using Logic.Converters;
using Logic.Validators;
using Model;

namespace Logic.Factories
{
    /// <summary>
    /// Фабрика для создания и обновления объектов курсов
    /// Отвечает за создание валидных объектов Course
    /// </summary>
    public class CourseFactory : ICourseFactory
    {
        private readonly ICourseValidator _courseValidator;
        private readonly IStatusConverter _statusConverter;

        /// <summary>
        /// Инициализирует новый экземпляр фабрики курсов
        /// </summary>
        /// <param name="courseValidator">Валидатор курса</param>
        /// <param name="statusConverter">Конвертер статуса</param>
        public CourseFactory(ICourseValidator courseValidator, IStatusConverter statusConverter)
        {
            _courseValidator = courseValidator ?? throw new ArgumentNullException(nameof(courseValidator));
            _statusConverter = statusConverter ?? throw new ArgumentNullException(nameof(statusConverter));
        }

        /// <summary>
        /// Создает новый объект курса с указанными параметрами
        /// </summary>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        /// <returns>Созданный объект курса</returns>
        public Course CreateCourse(string courseName, string description, int duration, decimal price, string teacherName, string status)
        {
            _courseValidator.Validate(price, duration, teacherName, status);
            bool isActive = _statusConverter.ConvertToBool(status);

            return new Course
            {
                Name = courseName,
                Description = description,
                Duration = duration,
                Price = price,
                TeacherName = teacherName,
                IsActive = isActive
            };
        }

        /// <summary>
        /// Обновляет существующий объект курса новыми данными
        /// </summary>
        /// <param name="existingCourse">Существующий курс для обновления</param>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        public void UpdateCourse(Course existingCourse, string courseName, string description, int duration, decimal price, string teacherName, string status)
        {
            if (existingCourse == null)
                throw new ArgumentNullException(nameof(existingCourse));

            _courseValidator.Validate(price, duration, teacherName, status);
            bool isActive = _statusConverter.ConvertToBool(status);

            existingCourse.Name = courseName;
            existingCourse.Description = description;
            existingCourse.Duration = duration;
            existingCourse.Price = price;
            existingCourse.TeacherName = teacherName;
            existingCourse.IsActive = isActive;
        }
    }
}
