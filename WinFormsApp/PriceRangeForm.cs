using Logic;

namespace WinFormsApp
{
    public partial class PriceRangeForm : Form
    {
        private SchoolService _schoolService;
        public List<object> FilteredCourses { get; private set; }

        /// <summary>
        /// Инициализирует форму фильтрации по цене
        /// </summary>
        public PriceRangeForm(SchoolService schoolService)
        {
            InitializeComponent();
            _schoolService = schoolService;
            FilteredCourses = new List<object>();
        }

        /// <summary>
        /// Обработчик нажатия кнопки фильтрации
        /// </summary>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                decimal minPrice = numMinPrice.Value;
                decimal maxPrice = numMaxPrice.Value;

                var results = _schoolService.GetCoursesInPriceRange(minPrice, maxPrice);
                FilteredCourses = new List<object>(results);

                dataGridViewResults.DataSource = FilteredCourses;
                ConfigureResultsGrid();

                lblResults.Text = $"Найдено курсов: {FilteredCourses.Count}";
            }
            catch (InvalidPriceRangeException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Неверный диапазон цен",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Настраивает отображение результатов фильтрации
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
            if (FilteredCourses != null && FilteredCourses.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Сначала выполните фильтрацию", "Информация",
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
        /// Обработчик изменения минимальной цены
        /// </summary>
        private void numMinPrice_ValueChanged(object sender, EventArgs e)
        {
            if (numMinPrice.Value > numMaxPrice.Value)
            {
                numMaxPrice.Value = numMinPrice.Value;
            }
        }

        /// <summary>
        /// Обработчик изменения максимальной цены
        /// </summary>
        private void numMaxPrice_ValueChanged(object sender, EventArgs e)
        {
            if (numMaxPrice.Value < numMinPrice.Value)
            {
                numMinPrice.Value = numMaxPrice.Value;
            }
        }
    }
}
