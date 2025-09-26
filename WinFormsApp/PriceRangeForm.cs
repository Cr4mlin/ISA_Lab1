using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Logic;
using Model;

namespace WinFormsApp
{
    public partial class PriceRangeForm : Form
    {
        private readonly SchoolService _schoolService;
        public List<Course> CoursesInPriceRange { get; private set; } = new List<Course>();

        public PriceRangeForm(SchoolService schoolService)
        {
            InitializeComponent();
            _schoolService = schoolService;
            this.Text = "Курсы в диапазоне цен";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                decimal minPrice = numMinPrice.Value;
                decimal maxPrice = numMaxPrice.Value;

                CoursesInPriceRange = _schoolService.GetCoursesInPriceRange(minPrice, maxPrice);
                dataGridViewPriceRange.DataSource = CoursesInPriceRange;
                this.DialogResult = DialogResult.OK;
            }
            catch (InvalidPriceRangeException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске курсов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
