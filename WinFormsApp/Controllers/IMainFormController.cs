using Model;

namespace WinFormsApp.Controllers
{
    /// <summary>
    /// Интерфейс контроллера главной формы
    /// </summary>
    public interface IMainFormController
    {
        /// <summary>
        /// Обновляет список курсов
        /// </summary>
        void RefreshCourses();
        
        /// <summary>
        /// Открывает форму добавления курса
        /// </summary>
        void OpenAddCourseForm();
        
        /// <summary>
        /// Открывает форму редактирования курса
        /// </summary>
        /// <param name="course">Курс для редактирования</param>
        void OpenEditCourseForm(Course course);
        
        /// <summary>
        /// Удаляет выбранные курсы
        /// </summary>
        /// <param name="courseIds">Список ID курсов для удаления</param>
        void DeleteSelectedCourses(List<int> courseIds);
        
        /// <summary>
        /// Переключает статус курса
        /// </summary>
        /// <param name="courseId">ID курса</param>
        void ToggleCourseStatus(int courseId);
    }
}
