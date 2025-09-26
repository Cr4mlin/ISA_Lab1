namespace WinFormsApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridViewCourses;
        private Button btnAddCourse;
        private Button btnEditCourse;
        private Button btnDeleteCourse;
        private Button btnSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dataGridViewCourses = new DataGridView();
            btnAddCourse = new Button();
            btnEditCourse = new Button();
            btnDeleteCourse = new Button();
            btnSearch = new Button();
            btnShowAll = new Button();
            btnShowActive = new Button();
            btnPriceRange = new Button();
            btnToggleStatus = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCourses).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewCourses
            // 
            dataGridViewCourses.AllowUserToAddRows = false;
            dataGridViewCourses.AllowUserToDeleteRows = false;
            dataGridViewCourses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCourses.ColumnHeadersHeight = 29;
            dataGridViewCourses.Location = new Point(3, 3);
            dataGridViewCourses.Name = "dataGridViewCourses";
            dataGridViewCourses.ReadOnly = true;
            dataGridViewCourses.RowHeadersWidth = 51;
            dataGridViewCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCourses.Size = new Size(880, 372);
            dataGridViewCourses.TabIndex = 2;
            dataGridViewCourses.SelectionChanged += dataGridViewCourses_SelectionChanged;
            // 
            // btnAddCourse
            // 
            btnAddCourse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAddCourse.Location = new Point(3, 3);
            btnAddCourse.Name = "btnAddCourse";
            btnAddCourse.Size = new Size(214, 35);
            btnAddCourse.TabIndex = 0;
            btnAddCourse.Text = "Добавить курс";
            btnAddCourse.Click += btnAddCourse_Click;
            // 
            // btnEditCourse
            // 
            btnEditCourse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnEditCourse.Enabled = false;
            btnEditCourse.Location = new Point(223, 3);
            btnEditCourse.Name = "btnEditCourse";
            btnEditCourse.Size = new Size(214, 35);
            btnEditCourse.TabIndex = 1;
            btnEditCourse.Text = "Редактировать";
            btnEditCourse.Click += btnEditCourse_Click;
            // 
            // btnDeleteCourse
            // 
            btnDeleteCourse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnDeleteCourse.Enabled = false;
            btnDeleteCourse.Location = new Point(443, 3);
            btnDeleteCourse.Name = "btnDeleteCourse";
            btnDeleteCourse.Size = new Size(214, 35);
            btnDeleteCourse.TabIndex = 2;
            btnDeleteCourse.Text = "Удалить";
            btnDeleteCourse.Click += btnDeleteCourse_Click;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSearch.Location = new Point(3, 44);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(214, 35);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Поиск";
            btnSearch.Click += btnSearch_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnShowAll.Location = new Point(663, 44);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(214, 35);
            btnShowAll.TabIndex = 3;
            btnShowAll.Text = "Все курсы";
            btnShowAll.Click += btnShowAll_Click;
            // 
            // btnShowActive
            // 
            btnShowActive.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnShowActive.Location = new Point(443, 44);
            btnShowActive.Name = "btnShowActive";
            btnShowActive.Size = new Size(214, 35);
            btnShowActive.TabIndex = 2;
            btnShowActive.Text = "Активные курсы";
            btnShowActive.Click += btnShowActive_Click;
            // 
            // btnPriceRange
            // 
            btnPriceRange.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnPriceRange.Location = new Point(223, 44);
            btnPriceRange.Name = "btnPriceRange";
            btnPriceRange.Size = new Size(214, 35);
            btnPriceRange.TabIndex = 1;
            btnPriceRange.Text = "Фильтр по цене";
            btnPriceRange.Click += btnPriceRange_Click;
            // 
            // btnToggleStatus
            // 
            btnToggleStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnToggleStatus.Enabled = false;
            btnToggleStatus.Location = new Point(663, 3);
            btnToggleStatus.Name = "btnToggleStatus";
            btnToggleStatus.Size = new Size(214, 35);
            btnToggleStatus.TabIndex = 0;
            btnToggleStatus.Text = "Сменить статус";
            btnToggleStatus.Click += btnToggleStatus_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(dataGridViewCourses, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Location = new Point(-2, -3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 73.11412F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 26.88588F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tableLayoutPanel1.Size = new Size(886, 517);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.Controls.Add(btnDeleteCourse, 2, 0);
            tableLayoutPanel3.Controls.Add(btnToggleStatus, 3, 0);
            tableLayoutPanel3.Controls.Add(btnShowAll, 3, 1);
            tableLayoutPanel3.Controls.Add(btnShowActive, 2, 1);
            tableLayoutPanel3.Controls.Add(btnPriceRange, 1, 1);
            tableLayoutPanel3.Controls.Add(btnAddCourse, 0, 0);
            tableLayoutPanel3.Controls.Add(btnEditCourse, 1, 0);
            tableLayoutPanel3.Controls.Add(btnSearch, 0, 1);
            tableLayoutPanel3.Location = new Point(3, 381);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 30.8270683F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 69.1729355F));
            tableLayoutPanel3.Size = new Size(880, 133);
            tableLayoutPanel3.TabIndex = 4;
            // 
            // MainForm
            // 
            ClientSize = new Size(884, 511);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(900, 550);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Управление курсами - Онлайн школа";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCourses).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Button btnShowAll;
        private Button btnShowActive;
        private Button btnPriceRange;
        private Button btnToggleStatus;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
    }
}