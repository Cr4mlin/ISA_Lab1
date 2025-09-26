using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Logic;
using Model;

namespace WinFormsApp
{
    public partial class SearchForm : Form
    {
        private readonly SchoolService _schoolService;
        public List<Course> SearchResults { get; private set; } = new List<Course>();

        public SearchForm(SchoolService schoolService)
        {
            InitializeComponent();
            _schoolService = schoolService;
            this.Text = "Поиск курсов";

            // Добавляем свойства для поиска
            clbSearchProperties.Items.Add("Название", true);
            clbSearchProperties.Items.Add("Преподаватель", true);
            clbSearchProperties.Items.Add("Идентификатор", true);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchText.Text.Trim();
            List<string> searchProperties = new List<string>();

            foreach (var item in clbSearchProperties.CheckedItems)
            {
                if (item is string propertyName)
                {
                    searchProperties.Add(propertyName);
                }
            }

            try
            {
                SearchResults = _schoolService.SearchCourses(searchText, searchProperties);
                dataGridViewSearchResults.DataSource = SearchResults;
                this.DialogResult = DialogResult.OK;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (PropertyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске курсов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
