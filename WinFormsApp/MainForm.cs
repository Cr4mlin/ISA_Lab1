using System.Data;
using DataAccessLayer;
using Logic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model;
using WinFormsApp.Services;
using WinFormsApp.Controllers;
using WinFormsApp.Views;

namespace WinFormsApp
{
    public partial class MainForm : Form, IMainFormView
    {
        private IMainFormController _controller;

        /// <summary>
        /// Инициализирует главную форму приложения
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обновляет список курсов в DataGridView
        /// </summary>
        private void RefreshCoursesList()
        {
            _controller?.RefreshCourses();
        }

        /// <summary>
        /// Настраивает отображение колонок в DataGridView
        /// </summary>
        private void ConfigureDataGridView()
        {
            if (dataGridViewCourses.Columns.Count > 0)
            {
                dataGridViewCourses.Columns["Id"].HeaderText = "ID";
                dataGridViewCourses.Columns["Name"].HeaderText = "Название";
                dataGridViewCourses.Columns["Description"].HeaderText = "Описание";
                dataGridViewCourses.Columns["Duration"].HeaderText = "Длительность (ч)";
                dataGridViewCourses.Columns["Price"].HeaderText = "Цена (руб)";
                dataGridViewCourses.Columns["TeacherName"].HeaderText = "Преподаватель";
                dataGridViewCourses.Columns["IsActive"].HeaderText = "Активен";

                dataGridViewCourses.Columns["Price"].DefaultCellStyle.Format = "N2";
            }
        }

        #region IMainFormView Implementation

        /// <summary>
        /// Отображает список курсов
        /// </summary>
        /// <param name="courses">Список курсов</param>
        public void DisplayCourses(List<Course> courses)
        {
            dataGridViewCourses.DataSource = courses;
            ConfigureDataGridView();
        }

        /// <summary>
        /// Открывает диалог добавления курса
        /// </summary>
        public void OpenAddCourseDialog()
        {
            // Получаем SchoolService из контроллера
            var schoolService = GetSchoolServiceFromController();
            if (schoolService != null)
            {
                var addForm = new AddEditCourseForm(schoolService);
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshCoursesList();
                }
            }
        }

        /// <summary>
        /// Открывает диалог редактирования курса
        /// </summary>
        /// <param name="course">Курс для редактирования</param>
        public void OpenEditCourseDialog(Course course)
        {
            // Получаем SchoolService из контроллера
            var schoolService = GetSchoolServiceFromController();
            if (schoolService != null)
            {
                var editForm = new AddEditCourseForm(schoolService, course);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshCoursesList();
                }
            }
        }

        /// <summary>
        /// Получает SchoolService из контроллера
        /// </summary>
        /// <returns>SchoolService или null</returns>
        private SchoolService GetSchoolServiceFromController()
        {
            if (_controller is MainFormController controller)
            {
                // Получаем SchoolService через рефлексию или создаем новый
                // Для простоты создаем новый через DependencyContainer
                return WinFormsDependencyContainer.CreateSchoolService(true); // По умолчанию Entity Framework
            }
            return null;
        }

        /// <summary>
        /// Показывает сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Показывает сообщение об успехе
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Показывает информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Запрашивает подтверждение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>true если пользователь подтвердил</returns>
        public bool ConfirmAction(string message)
        {
            return MessageBox.Show(message, "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        #endregion

        /// <summary>
        /// Обработчик нажатия кнопки добавления курса
        /// </summary>
        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            _controller?.OpenAddCourseForm();
        }

        /// <summary>
        /// Обработчик нажатия кнопки редактирования курса
        /// </summary>
        private void btnEditCourse_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                var selectedCourse = dataGridViewCourses.SelectedRows[0].DataBoundItem as Course;
                if (selectedCourse != null)
                {
                    _controller?.OpenEditCourseForm(selectedCourse);
                }
            }
            else
            {
                ShowInfo("Выберите курс для редактирования");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки удаления курса
        /// </summary>
        private void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                var courseIds = new List<int>();
                foreach (DataGridViewRow row in dataGridViewCourses.SelectedRows)
                {
                    if (row.DataBoundItem is Course course)
                    {
                        courseIds.Add(course.Id);
                    }
                }

                if (courseIds.Count > 0)
                {
                    string message = courseIds.Count == 1
                        ? "Вы уверены, что хотите удалить выбранный курс?"
                        : $"Вы уверены, что хотите удалить {courseIds.Count} выбранных курсов?";

                    if (ConfirmAction(message))
                    {
                        _controller?.DeleteSelectedCourses(courseIds);
                        RefreshCoursesList();
                    }
                }
            }
            else
            {
                ShowInfo("Выберите курс для удаления");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки поиска курсов
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var schoolService = GetSchoolServiceFromController();
            if (schoolService != null)
            {
                var searchForm = new SearchForm(schoolService);
                if (searchForm.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewCourses.DataSource = searchForm.SearchResults;
                    ConfigureDataGridView();
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки смены статуса курса
        /// </summary>
        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                var selectedCourse = dataGridViewCourses.SelectedRows[0].DataBoundItem as Course;
                if (selectedCourse != null)
                {
                    _controller?.ToggleCourseStatus(selectedCourse.Id);
                }
            }
            else
            {
                ShowInfo("Выберите курс для изменения статуса");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки фильтрации по цене
        /// </summary>
        private void btnPriceRange_Click(object sender, EventArgs e)
        {
            var schoolService = GetSchoolServiceFromController();
            if (schoolService != null)
            {
                var priceForm = new PriceRangeForm(schoolService);
                if (priceForm.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewCourses.DataSource = priceForm.FilteredCourses;
                    ConfigureDataGridView();
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки показа активных курсов
        /// </summary>
        private void btnShowActive_Click(object sender, EventArgs e)
        {
            try
            {
                var schoolService = GetSchoolServiceFromController();
                if (schoolService != null)
                {
                    var activeCourses = schoolService.GetActiveCourses();
                    dataGridViewCourses.DataSource = activeCourses;
                    ConfigureDataGridView();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке активных курсов: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки показа всех курсов
        /// </summary>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshCoursesList();
        }

        private void btnSelectConnection_Click(object sender, EventArgs e)
        {
            using (var form = new DbConnectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string selectedConnection = form.SelectedConnectionType;

                    bool useEntityFramework = selectedConnection == "EntityFrameWork";
                    var schoolService = WinFormsDependencyContainer.CreateSchoolService(useEntityFramework);
                    
                    // Инициализируем контроллер
                    var courseFormService = new CourseFormService(schoolService);
                    _controller = new MainFormController(courseFormService, this);

                    MessageBox.Show($"Вы подключились через: {selectedConnection}");
                    RefreshCoursesList();
                }
            }
        }

        /// <summary>
        /// Обработчик изменения выделения в DataGridView
        /// </summary>
        private void dataGridViewCourses_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dataGridViewCourses.SelectedRows.Count > 0;
            btnEditCourse.Enabled = hasSelection;
            btnDeleteCourse.Enabled = hasSelection;
            btnToggleStatus.Enabled = hasSelection;
        }
    }
}
