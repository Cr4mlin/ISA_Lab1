namespace WinFormsApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewCourses = new DataGridView();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            btnSearch = new Button();
            btnActiveCourses = new Button();
            btnPriceRange = new Button();
            btnToggleStatus = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCourses).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewCourses
            // 
            dataGridViewCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCourses.Location = new Point(12, 12);
            dataGridViewCourses.Name = "dataGridViewCourses";
            dataGridViewCourses.RowTemplate.Height = 25;
            dataGridViewCourses.Size = new Size(776, 300);
            dataGridViewCourses.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 318);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 30);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(118, 318);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 30);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(224, 318);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 30);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(330, 318);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(12, 354);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 30);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Поиск";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnActiveCourses
            // 
            btnActiveCourses.Location = new Point(118, 354);
            btnActiveCourses.Name = "btnActiveCourses";
            btnActiveCourses.Size = new Size(130, 30);
            btnActiveCourses.TabIndex = 6;
            btnActiveCourses.Text = "Активные курсы";
            btnActiveCourses.UseVisualStyleBackColor = true;
            btnActiveCourses.Click += btnActiveCourses_Click;
            // 
            // btnPriceRange
            // 
            btnPriceRange.Location = new Point(254, 354);
            btnPriceRange.Name = "btnPriceRange";
            btnPriceRange.Size = new Size(176, 30);
            btnPriceRange.TabIndex = 7;
            btnPriceRange.Text = "Курсы в диапазоне цен";
            btnPriceRange.UseVisualStyleBackColor = true;
            btnPriceRange.Click += btnPriceRange_Click;
            // 
            // btnToggleStatus
            // 
            btnToggleStatus.Location = new Point(436, 318);
            btnToggleStatus.Name = "btnToggleStatus";
            btnToggleStatus.Size = new Size(150, 30);
            btnToggleStatus.TabIndex = 8;
            btnToggleStatus.Text = "Сменить статус курса";
            btnToggleStatus.UseVisualStyleBackColor = true;
            btnToggleStatus.Click += btnToggleStatus_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new Size(7, 15);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnToggleStatus);
            Controls.Add(btnPriceRange);
            Controls.Add(btnActiveCourses);
            Controls.Add(btnSearch);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dataGridViewCourses);
            Name = "Form1";
            Text = "Управление курсами";
            ((System.ComponentModel.ISupportInitialize)dataGridViewCourses).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewCourses;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnSearch;
        private Button btnActiveCourses;
        private Button btnPriceRange;
        private Button btnToggleStatus;
    }
}
