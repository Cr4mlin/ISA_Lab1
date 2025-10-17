using System.Data;
using DataAccessLayer;
using Logic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model;

namespace WinFormsApp
{
    public partial class MainForm : Form
    {
        private SchoolService _schoolService;
        private IRepository<Course> _repository;

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
            try
            {
                var courses = _schoolService.GetAllCourses();
                dataGridViewCourses.DataSource = courses;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке курсов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        /// <summary>
        /// Обработчик нажатия кнопки добавления курса
        /// </summary>
        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            var addForm = new AddEditCourseForm(_schoolService);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                RefreshCoursesList();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки редактирования курса
        /// </summary>
        private void btnEditCourse_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                var selectedCourse = dataGridViewCourses.SelectedRows[0].DataBoundItem;
                if (selectedCourse != null)
                {
                    var editForm = new AddEditCourseForm(_schoolService, selectedCourse);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshCoursesList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите курс для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки удаления курса
        /// </summary>
        private void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                if (dataGridViewCourses.SelectedRows.Count > 0)
                {
                    var selectedCourses = new List<object>();
                    var courseIds = new List<string>();

                    foreach (DataGridViewRow row in dataGridViewCourses.SelectedRows)
                    {
                        if (row.DataBoundItem != null)
                        {
                            selectedCourses.Add(row.DataBoundItem);
                            var courseId = row.DataBoundItem.GetType().GetProperty("Id")?.GetValue(row.DataBoundItem)?.ToString();
                            if (!string.IsNullOrEmpty(courseId))
                            {
                                courseIds.Add(courseId);
                            }
                        }
                    }

                    if (courseIds.Count > 0)
                    {
                        string message = courseIds.Count == 1
                            ? "Вы уверены, что хотите удалить выбранный курс?"
                            : $"Вы уверены, что хотите удалить {courseIds.Count} выбранных курсов?";

                        var result = MessageBox.Show(message, "Подтверждение удаления",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                int deletedCount = 0;
                                var errors = new List<string>();

                                // Удаляем курсы по одному, собирая ошибки
                                foreach (var courseId in courseIds)
                                {
                                    try
                                    {
                                        if (_schoolService.DeleteCourse(Convert.ToInt32(courseId)))
                                        {
                                            deletedCount++;
                                        }
                                    }
                                    catch (CourseNotFoundException ex)
                                    {
                                        errors.Add($"Курс с ID '{courseId}' не найден: {ex.Message}");
                                    }
                                    catch (Exception ex)
                                    {
                                        errors.Add($"Ошибка при удалении курса '{courseId}': {ex.Message}");
                                    }
                                }

                                // Показываем результат операции
                                string resultMessage = $"Успешно удалено курсов: {deletedCount}";
                                if (errors.Count > 0)
                                {
                                    resultMessage += $"\nОшибок: {errors.Count}";
                                    if (errors.Count <= 5) // Показываем первые 5 ошибок
                                    {
                                        resultMessage += "\n\n" + string.Join("\n", errors.Take(5));
                                    }
                                    else
                                    {
                                        resultMessage += $"\n\nПервые 5 ошибок:\n" + string.Join("\n", errors.Take(5));
                                        resultMessage += $"\n... и еще {errors.Count - 5} ошибок";
                                    }
                                }

                                RefreshCoursesList();

                                MessageBox.Show(resultMessage, "Результат удаления",
                                    deletedCount > 0 ? MessageBoxButtons.OK : MessageBoxButtons.OK,
                                    deletedCount > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Общая ошибка при удалении курсов: {ex.Message}", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }   
            else
            {
                MessageBox.Show("Выберите курс для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки поиска курсов
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchForm = new SearchForm(_schoolService);
            if (searchForm.ShowDialog() == DialogResult.OK)
            {
                dataGridViewCourses.DataSource = searchForm.SearchResults;
                ConfigureDataGridView();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки смены статуса курса
        /// </summary>
        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                var selectedCourse = dataGridViewCourses.SelectedRows[0].DataBoundItem;
                if (selectedCourse != null)
                {
                    var courseId = selectedCourse.GetType().GetProperty("Id")?.GetValue(selectedCourse)?.ToString();

                    if (!string.IsNullOrEmpty(courseId))
                    {
                        try
                        {
                            _schoolService.ToggleCourseStatus(Convert.ToInt32(courseId));
                            RefreshCoursesList();
                            MessageBox.Show("Статус курса изменен", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (CourseNotFoundException ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Курс не найден",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при изменении статуса: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите курс для изменения статуса", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки фильтрации по цене
        /// </summary>
        private void btnPriceRange_Click(object sender, EventArgs e)
        {
            var priceForm = new PriceRangeForm(_schoolService);
            if (priceForm.ShowDialog() == DialogResult.OK)
            {
                dataGridViewCourses.DataSource = priceForm.FilteredCourses;
                ConfigureDataGridView();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки показа активных курсов
        /// </summary>
        private void btnShowActive_Click(object sender, EventArgs e)
        {
            try
            {
                var activeCourses = _schoolService.GetActiveCourses();
                dataGridViewCourses.DataSource = activeCourses;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке активных курсов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    switch (selectedConnection)
                    {
                        case "EntityFrameWork":
                            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseSqlServer("Server=DESKTOP-VLCID23\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;")
                            .Options;

                            var context = new ApplicationDbContext(options);
                            _repository = new EntityRepository<Course>(context);
                            break;
                        case "Dapper":
                            IDbConnection connection = new SqlConnection("Server=DESKTOP-VLCID23\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;");
                            _repository = new DapperRepository<Course>(connection);
                            break;
                    }
                    _schoolService = new SchoolService(_repository);

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
