using System;
using System.Windows.Forms;
using Logic;
using Model;

namespace WinFormsApp
{
    public partial class CourseForm : Form
    {
        private readonly SchoolService _schoolService;
        private readonly Form1 _mainForm;
        private Course? _courseToEdit;

        public CourseForm(Form1 mainForm, SchoolService schoolService)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _schoolService = schoolService;
            this.Text = "Добавить курс";
        }

        public CourseForm(Form1 mainForm, SchoolService schoolService, Course courseToEdit) : this(mainForm, schoolService)
        {
            _courseToEdit = courseToEdit;
            this.Text = "Редактировать курс";
            LoadCourseData();
        }

        private void LoadCourseData()
        {
            if (_courseToEdit != null)
            {
                txtName.Text = _courseToEdit.Name;
                txtId.Text = _courseToEdit.Id;
                txtDescription.Text = _courseToEdit.Description;
                numDuration.Value = _courseToEdit.Duration;
                numPrice.Value = _courseToEdit.Price;
                txtTeacherName.Text = _courseToEdit.TeacherName;
                cbIsActive.Checked = _courseToEdit.IsActive;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string id = txtId.Text;
                string description = txtDescription.Text;
                int duration = (int)numDuration.Value;
                decimal price = numPrice.Value;
                string teacherName = txtTeacherName.Text;
                string status = cbIsActive.Checked ? "да" : "нет";

                if (_courseToEdit == null)
                {
                    // Добавление нового курса
                    _schoolService.CreateCourse(name, description, id, duration, price, teacherName, status);
                    MessageBox.Show("Курс успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Редактирование существующего курса
                    _schoolService.UpdateCourse(_courseToEdit.Id, name, description, id, duration, price, teacherName, status);
                    MessageBox.Show("Курс успешно обновлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _mainForm.LoadCourses();
                this.Close();
            }
            catch (CourseIdExistsException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidPriceException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidDurationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidTeacherNameException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidIsActiveException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
