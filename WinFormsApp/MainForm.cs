using Logic;

namespace WinFormsApp
{
    public partial class MainForm : Form
    {
        private SchoolService _schoolService;

        /// <summary>
        /// Инициализирует главную форму приложения
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _schoolService = new SchoolService();
            RefreshCoursesList();
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
                var selectedCourse = dataGridViewCourses.SelectedRows[0].DataBoundItem;
                if (selectedCourse != null)
                {
                    var courseId = selectedCourse.GetType().GetProperty("Id")?.GetValue(selectedCourse)?.ToString();

                    if (!string.IsNullOrEmpty(courseId))
                    {
                        var result = MessageBox.Show($"Вы уверены, что хотите удалить курс?",
                            "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                if (_schoolService.DeleteCourse(courseId))
                                {
                                    RefreshCoursesList();
                                    MessageBox.Show("Курс успешно удален", "Успех",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (CourseNotFoundException ex)
                            {
                                MessageBox.Show($"Ошибка: {ex.Message}", "Курс не найден",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка при удалении курса: {ex.Message}", "Ошибка",
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
                            _schoolService.ToggleCourseStatus(courseId);
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
