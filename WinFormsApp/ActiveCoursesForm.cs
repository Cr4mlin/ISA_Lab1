using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Logic;
using Model;

namespace WinFormsApp
{
    public partial class ActiveCoursesForm : Form
    {
        private readonly SchoolService _schoolService;
        public List<Course> ActiveCourses { get; private set; } = new List<Course>();

        public ActiveCoursesForm(SchoolService schoolService)
        {
            InitializeComponent();
            _schoolService = schoolService;
            this.Text = "Активные курсы";
            LoadActiveCourses();
        }

        private void LoadActiveCourses()
        {
            try
            {
                ActiveCourses = _schoolService.GetActiveCourses();
                dataGridViewActiveCourses.DataSource = ActiveCourses;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке активных курсов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
