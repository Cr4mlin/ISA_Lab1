using Model;

namespace WinFormsApp.Services
{
    /// <summary>
    /// Интерфейс для сервиса работы с формами курсов
    /// </summary>
    public interface ICourseFormService
    {
        /// <summary>
        /// Получает все курсы для отображения
        /// </summary>
        /// <returns>Список курсов</returns>
        List<Course> GetAllCourses();
        
        /// <summary>
        /// Удаляет курс по ID
        /// </summary>
        /// <param name="courseId">ID курса</param>
        /// <returns>true если удален успешно</returns>
        bool DeleteCourse(int courseId);
        
        /// <summary>
        /// Переключает статус курса
        /// </summary>
        /// <param name="courseId">ID курса</param>
        void ToggleCourseStatus(int courseId);
    }
}
