using Model;
using WinFormsApp.Services;
using WinFormsApp.Views;
using System.Collections.Generic;

namespace WinFormsApp.Controllers
{
    /// <summary>
    /// Контроллер главной формы
    /// </summary>
    public class MainFormController : IMainFormController
    {
        private readonly ICourseFormService _courseFormService;
        private readonly IMainFormView _view;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера главной формы
        /// </summary>
        /// <param name="courseFormService">Сервис работы с курсами</param>
        /// <param name="view">Представление главной формы</param>
        public MainFormController(ICourseFormService courseFormService, IMainFormView view)
        {
            _courseFormService = courseFormService;
            _view = view;
        }

        /// <summary>
        /// Обновляет список курсов
        /// </summary>
        public void RefreshCourses()
        {
            try
            {
                var courses = _courseFormService.GetAllCourses();
                _view.DisplayCourses(courses);
            }
            catch (System.Exception ex)
            {
                _view.ShowError($"Ошибка при загрузке курсов: {ex.Message}");
            }
        }

        /// <summary>
        /// Открывает форму добавления курса
        /// </summary>
        public void OpenAddCourseForm()
        {
            _view.OpenAddCourseDialog();
        }

        /// <summary>
        /// Открывает форму редактирования курса
        /// </summary>
        /// <param name="course">Курс для редактирования</param>
        public void OpenEditCourseForm(Course course)
        {
            _view.OpenEditCourseDialog(course);
        }

        /// <summary>
        /// Удаляет выбранные курсы
        /// </summary>
        /// <param name="courseIds">Список ID курсов для удаления</param>
        public void DeleteSelectedCourses(List<int> courseIds)
        {
            try
            {
                int deletedCount = 0;
                var errors = new List<string>();

                foreach (var courseId in courseIds)
                {
                    try
                    {
                        if (_courseFormService.DeleteCourse(courseId))
                        {
                            deletedCount++;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        errors.Add($"Ошибка при удалении курса {courseId}: {ex.Message}");
                    }
                }

                if (deletedCount > 0)
                {
                    _view.ShowSuccess($"Успешно удалено курсов: {deletedCount}");
                }

                if (errors.Count > 0)
                {
                    _view.ShowError($"Ошибки при удалении:\n{string.Join("\n", errors)}");
                }
            }
            catch (System.Exception ex)
            {
                _view.ShowError($"Ошибка при удалении курсов: {ex.Message}");
            }
        }

        /// <summary>
        /// Переключает статус курса
        /// </summary>
        /// <param name="courseId">ID курса</param>
        public void ToggleCourseStatus(int courseId)
        {
            try
            {
                _courseFormService.ToggleCourseStatus(courseId);
                _view.ShowSuccess("Статус курса изменен");
                RefreshCourses();
            }
            catch (System.Exception ex)
            {
                _view.ShowError($"Ошибка при изменении статуса: {ex.Message}");
            }
        }
    }
}
