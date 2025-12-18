using Shared.DTO;

namespace WinFormsApps
{
    public partial class AddEditForm : Form
    {
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        public CourseDto? CourseData { get; private set; }

        public AddEditForm()
        {
            InitializeComponent();
            InitializeForm();

            panelTop.MouseDown += Panel_MouseDown;
            panelTop.MouseMove += Panel_MouseMove;
            panelTop.MouseUp += Panel_MouseUp;
        }

        public AddEditForm(CourseDto course) : this()
        {
            LoadCourseData(course);
        }

        private void InitializeForm()
        {
            // Инициализация ComboBox статуса
            cbStatus.Items.Clear();
            cbStatus.Items.Add("да");
            cbStatus.Items.Add("нет");
            cbStatus.SelectedIndex = 0;

            // Подключение событий кнопок
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void LoadCourseData(CourseDto course)
        {
            if (course == null) return;

            txtBoxCourseName.Text = course.Name;
            txtBoxCourseDescription.Text = course.Description;
            numericUpDownCourseDuration.Value = course.Duration;
            numericUpDownCourseValue.Value = course.Price;
            textBoxTeacherName.Text = course.TeacherName;
            cbStatus.SelectedIndex = course.IsActive ? 0 : 1;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                // Валидация
                if (string.IsNullOrWhiteSpace(txtBoxCourseName.Text))
                {
                    MessageBox.Show("Введите название курса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtBoxCourseDescription.Text))
                {
                    MessageBox.Show("Введите описание курса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxTeacherName.Text))
                {
                    MessageBox.Show("Введите имя преподавателя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Создание DTO
                CourseData = new CourseDto
                {
                    Name = txtBoxCourseName.Text.Trim(),
                    Description = txtBoxCourseDescription.Text.Trim(),
                    Duration = (int)numericUpDownCourseDuration.Value,
                    Price = numericUpDownCourseValue.Value,
                    TeacherName = textBoxTeacherName.Text.Trim(),
                    IsActive = cbStatus.SelectedIndex == 0
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { _dragging = true; _dragCursorPoint = Cursor.Position; _dragFormPoint = this.Location; }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging) this.Location = Point.Add(_dragFormPoint, new Size(Point.Subtract(Cursor.Position, new Size(_dragCursorPoint))));
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e) => _dragging = false;
    }
}
