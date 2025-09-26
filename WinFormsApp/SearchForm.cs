using Logic;

namespace WinFormsApp
{
    public partial class SearchForm : Form
    {
        private SchoolService _schoolService;
        public List<object> SearchResults { get; private set; }

        /// <summary>
        /// Инициализирует форму поиска курсов
        /// </summary>
        public SearchForm(SchoolService schoolService)
        {
            InitializeComponent();
            _schoolService = schoolService;
            SearchResults = new List<object>();
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void SearchForm_Load(object sender, EventArgs e)
        {
            clbSearchProperties.Items.AddRange(new object[] {
                "Название", "Преподаватель", "Идентификатор"
            });

            // Выбираем все свойства по умолчанию
            for (int i = 0; i < clbSearchProperties.Items.Count; i++)
            {
                clbSearchProperties.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки поиска
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearchText.Text))
                {
                    MessageBox.Show("Введите текст для поиска", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearchText.Focus();
                    return;
                }

                var selectedProperties = new List<string>();
                foreach (var item in clbSearchProperties.CheckedItems)
                {
                    selectedProperties.Add(item.ToString());
                }

                if (selectedProperties.Count == 0)
                {
                    MessageBox.Show("Выберите хотя бы одно свойство для поиска", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var results = _schoolService.SearchCourses(txtSearchText.Text.Trim(), selectedProperties);
                SearchResults = new List<object>(results);

                dataGridViewResults.DataSource = SearchResults;
                ConfigureResultsGrid();

                lblResults.Text = $"Найдено курсов: {SearchResults.Count}";

                if (SearchResults.Count == 0)
                {
                    MessageBox.Show("Курсы по заданным критериям не найдены", "Результаты поиска",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка ввода: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Настраивает отображение результатов поиска
        /// </summary>
        private void ConfigureResultsGrid()
        {
            if (dataGridViewResults.Columns.Count > 0)
            {
                dataGridViewResults.Columns["Id"].HeaderText = "ID";
                dataGridViewResults.Columns["Name"].HeaderText = "Название";
                dataGridViewResults.Columns["Description"].HeaderText = "Описание";
                dataGridViewResults.Columns["Duration"].HeaderText = "Длительность (ч)";
                dataGridViewResults.Columns["Price"].HeaderText = "Цена (руб)";
                dataGridViewResults.Columns["TeacherName"].HeaderText = "Преподаватель";
                dataGridViewResults.Columns["IsActive"].HeaderText = "Активен";

                dataGridViewResults.Columns["Price"].DefaultCellStyle.Format = "N2";
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки применения результатов
        /// </summary>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (SearchResults.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Сначала выполните поиск", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки отмены
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Обработчик нажатия клавиши в поле поиска
        /// </summary>
        private void txtSearchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
                e.Handled = true;
            }
        }
    }
}
