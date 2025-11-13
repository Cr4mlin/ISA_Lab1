using DataAccessLayer;
using Logic.Factories;
using Model;

namespace Logic.Services
{
    /// <summary>
    /// Сервис управления жизненным циклом курсов
    /// Отвечает за создание, обновление и удаление курсов
    /// </summary>
    public class CourseManagementService : ICourseManagementService
    {
        private readonly IRepository<Course> _repository;
        private readonly ICourseFactory _courseFactory;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса управления курсами
        /// </summary>
        /// <param name="repository">Репозиторий для работы с курсами</param>
        /// <param name="courseFactory">Фабрика для создания курсов</param>
        public CourseManagementService(IRepository<Course> repository, ICourseFactory courseFactory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _courseFactory = courseFactory ?? throw new ArgumentNullException(nameof(courseFactory));
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
        public Course CreateCourse(string courseName, string description, int duration, decimal price, string teacherName, string status)
        {
            var newCourse = _courseFactory.CreateCourse(courseName, description, duration, price, teacherName, status);
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
        public Course UpdateCourse(int courseId, string courseName, string description, int duration, decimal price, string teacherName, string status)
        {
            var existingCourse = _repository.ReadById(courseId);
            if (existingCourse == null)
            {
                throw new CourseNotFoundException(courseId.ToString());
            }

            _courseFactory.UpdateCourse(existingCourse, courseName, description, duration, price, teacherName, status);
            _repository.Update(existingCourse);

            return existingCourse;
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
        /// Переключает статус активности курса (активный/неактивный)
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        public void ToggleCourseStatus(int courseId)
        {
            var course = _repository.ReadById(courseId);
            if (course == null)
            {
                throw new CourseNotFoundException(courseId.ToString());
            }

            course.IsActive = !course.IsActive;
            _repository.Update(course);
        }
    }
}
