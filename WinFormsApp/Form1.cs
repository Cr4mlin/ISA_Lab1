using Logic;
using Model;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly SchoolService _schoolService = new SchoolService();
        public Form1()
        {
            InitializeComponent();
            LoadCourses();
        }

        public void LoadCourses()
        {
            try
            {
                var courses = _schoolService.GetAllCourses();
                dataGridViewCourses.DataSource = courses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке курсов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var courseForm = new CourseForm(this, _schoolService);
            courseForm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                var selectedCourse = (Course)dataGridViewCourses.SelectedRows[0].DataBoundItem;
                var courseForm = new CourseForm(this, _schoolService, selectedCourse);
                courseForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите курс для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                var selectedCourse = (Course)dataGridViewCourses.SelectedRows[0].DataBoundItem;
                DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить курс '{selectedCourse.Name}'?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _schoolService.DeleteCourse(selectedCourse.Id);
                        LoadCourses();
                        MessageBox.Show("Курс успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (CourseNotFoundException ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении курса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите курс для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchForm = new SearchForm(_schoolService);
            searchForm.ShowDialog();
            if (searchForm.DialogResult == DialogResult.OK)
            {
                dataGridViewCourses.DataSource = searchForm.SearchResults;
            }
        }

        private void btnActiveCourses_Click(object sender, EventArgs e)
        {
            var activeCoursesForm = new ActiveCoursesForm(_schoolService);
            activeCoursesForm.ShowDialog();
            if (activeCoursesForm.DialogResult == DialogResult.OK)
            {
                dataGridViewCourses.DataSource = activeCoursesForm.ActiveCourses;
            }
        }

        private void btnPriceRange_Click(object sender, EventArgs e)
        {
            var priceRangeForm = new PriceRangeForm(_schoolService);
            priceRangeForm.ShowDialog();
            if (priceRangeForm.DialogResult == DialogResult.OK)
            {
                dataGridViewCourses.DataSource = priceRangeForm.CoursesInPriceRange;
            }
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                var selectedCourse = (Course)dataGridViewCourses.SelectedRows[0].DataBoundItem;
                try
                {
                    _schoolService.ToggleCourseStatus(selectedCourse.Id);
                    LoadCourses();
                    MessageBox.Show("Статус курса успешно изменен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (CourseNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка изменения статуса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при изменении статуса курса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите курс для изменения статуса.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
