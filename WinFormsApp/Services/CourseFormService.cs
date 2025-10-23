using Logic;
using Model;

namespace WinFormsApp.Services
{
    /// <summary>
    /// Сервис для работы с формами курсов
    /// </summary>
    public class CourseFormService : ICourseFormService
    {
        private readonly SchoolService _schoolService;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса форм курсов
        /// </summary>
        /// <param name="schoolService">Сервис управления курсами</param>
        public CourseFormService(SchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        /// <summary>
        /// Получает все курсы для отображения
        /// </summary>
        /// <returns>Список курсов</returns>
        public List<Course> GetAllCourses()
        {
            return _schoolService.GetAllCourses();
        }

        /// <summary>
        /// Удаляет курс по ID
        /// </summary>
        /// <param name="courseId">ID курса</param>
        /// <returns>true если удален успешно</returns>
        public bool DeleteCourse(int courseId)
        {
            return _schoolService.DeleteCourse(courseId);
        }

        /// <summary>
        /// Переключает статус курса
        /// </summary>
        /// <param name="courseId">ID курса</param>
        public void ToggleCourseStatus(int courseId)
        {
            _schoolService.ToggleCourseStatus(courseId);
        }
    }
}
