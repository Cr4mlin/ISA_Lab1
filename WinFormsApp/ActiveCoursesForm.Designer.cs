namespace WinFormsApp
{
    partial class ActiveCoursesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewActiveCourses = new DataGridView();
            this.btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)this.dataGridViewActiveCourses).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewActiveCourses
            // 
            this.dataGridViewActiveCourses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewActiveCourses.Location = new Point(12, 12);
            this.dataGridViewActiveCourses.Name = "dataGridViewActiveCourses";
            this.dataGridViewActiveCourses.RowTemplate.Height = 25;
            this.dataGridViewActiveCourses.Size = new Size(776, 380);
            this.dataGridViewActiveCourses.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new Point(688, 398);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(100, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += btnClose_Click;
            // 
            // ActiveCoursesForm
            // 
            this.AutoScaleDimensions = new Size(7, 15);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridViewActiveCourses);
            this.Name = "ActiveCoursesForm";
            this.Text = "ActiveCoursesForm";
            ((System.ComponentModel.ISupportInitialize)this.dataGridViewActiveCourses).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewActiveCourses;
        private Button btnClose;
    }
}
