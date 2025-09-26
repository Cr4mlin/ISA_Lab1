using System.Xml.Linq;
using Logic;
using Model;

namespace WinFormsApp
{
    public partial class AddEditCourseForm : Form
    {
        private SchoolService _schoolService;
        private object _existingCourse;
        private bool _isEditMode;

        /// <summary>
        /// Инициализирует форму для создания нового курса
        /// </summary>
        public AddEditCourseForm(SchoolService schoolService)
        {
            InitializeComponent();
            _schoolService = schoolService;
            _isEditMode = false;
            Text = "Добавление нового курса";
            btnSave.Text = "Создать курс";
        }

        /// <summary>
        /// Инициализирует форму для редактирования существующего курса
        /// </summary>
        public AddEditCourseForm(SchoolService schoolService, object existingCourse)
        {
            InitializeComponent();
            _schoolService = schoolService;
            _existingCourse = existingCourse;
            _isEditMode = true;
            Text = "Редактирование курса";
            btnSave.Text = "Сохранить изменения";
            PopulateForm();
        }

        /// <summary>
        /// Заполняет форму данными существующего курса
        /// </summary>
        private void PopulateForm()
        {
            if (_existingCourse == null) return;

            var courseType = _existingCourse.GetType();

            txtId.Text = courseType.GetProperty("Id")?.GetValue(_existingCourse)?.ToString() ?? "";
            txtName.Text = courseType.GetProperty("Name")?.GetValue(_existingCourse)?.ToString() ?? "";
            txtDescription.Text = courseType.GetProperty("Description")?.GetValue(_existingCourse)?.ToString() ?? "";

            var duration = courseType.GetProperty("Duration")?.GetValue(_existingCourse);
            if (duration != null) numDuration.Value = Convert.ToInt32(duration);

            var price = courseType.GetProperty("Price")?.GetValue(_existingCourse);
            if (price != null) numPrice.Value = Convert.ToDecimal(price);

            txtTeacher.Text = courseType.GetProperty("TeacherName")?.GetValue(_existingCourse)?.ToString() ?? "";

            var isActive = courseType.GetProperty("IsActive")?.GetValue(_existingCourse);
            cmbStatus.SelectedItem = (isActive != null && Convert.ToBoolean(isActive)) ? "да" : "нет";
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void AddEditCourseForm_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.AddRange(new object[] { "да", "нет" });
            if (cmbStatus.SelectedIndex == -1)
                cmbStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Обработчик нажатия кнопки сохранения
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                string status = cmbStatus.SelectedItem?.ToString() ?? "да";

                if (_isEditMode)
                {
                    var oldId = _existingCourse.GetType().GetProperty("Id")?.GetValue(_existingCourse)?.ToString();
                    if (string.IsNullOrEmpty(oldId)) throw new InvalidOperationException("Не удалось получить ID курса");

                    _schoolService.UpdateCourse(
                        oldId,
                        txtName.Text.Trim(),
                        txtDescription.Text.Trim(),
                        txtId.Text.Trim(),
                        (int)numDuration.Value,
                        numPrice.Value,
                        txtTeacher.Text.Trim(),
                        status
                    );
                    MessageBox.Show("Курс успешно обновлен", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _schoolService.CreateCourse(
                        txtName.Text.Trim(),
                        txtDescription.Text.Trim(),
                        txtId.Text.Trim(),
                        (int)numDuration.Value,
                        numPrice.Value,
                        txtTeacher.Text.Trim(),
                        status
                    );
                    MessageBox.Show("Курс успешно создан", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (CourseIdExistsException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Дублирование ID",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidPriceException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Неверная цена",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidDurationException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Неверная длительность",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidTeacherNameException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Неверное имя преподавателя",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidIsActiveException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Неверный статус",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CourseNotFoundException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Курс не найден",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Валидирует данные формы
        /// </summary>
        /// <returns>True если данные валидны, иначе False</returns>
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Введите ID курса", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название курса", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTeacher.Text))
            {
                MessageBox.Show("Введите имя преподавателя", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTeacher.Focus();
                return false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус курса", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbStatus.Focus();
                return false;
            }

            if (numDuration.Value <= 0)
            {
                MessageBox.Show("Длительность курса должна быть положительной", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                numDuration.Focus();
                return false;
            }

            if (numPrice.Value < 0)
            {
                MessageBox.Show("Цена курса не может быть отрицательной", "Ошибка валидации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                numPrice.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработчик нажатия кнопки отмены
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
