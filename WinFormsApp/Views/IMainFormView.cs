using Model;

namespace WinFormsApp.Views
{
    /// <summary>
    /// Интерфейс представления главной формы
    /// </summary>
    public interface IMainFormView
    {
        /// <summary>
        /// Отображает список курсов
        /// </summary>
        /// <param name="courses">Список курсов</param>
        void DisplayCourses(List<Course> courses);
        
        /// <summary>
        /// Открывает диалог добавления курса
        /// </summary>
        void OpenAddCourseDialog();
        
        /// <summary>
        /// Открывает диалог редактирования курса
        /// </summary>
        /// <param name="course">Курс для редактирования</param>
        void OpenEditCourseDialog(Course course);
        
        /// <summary>
        /// Показывает сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        void ShowError(string message);
        
        /// <summary>
        /// Показывает сообщение об успехе
        /// </summary>
        /// <param name="message">Сообщение</param>
        void ShowSuccess(string message);
        
        /// <summary>
        /// Показывает информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        void ShowInfo(string message);
        
        /// <summary>
        /// Запрашивает подтверждение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>true если пользователь подтвердил</returns>
        bool ConfirmAction(string message);
    }
}
