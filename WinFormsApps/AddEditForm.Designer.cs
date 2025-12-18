namespace WinFormsApps
{
    partial class AddEditForm
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
            lblNameCourse = new Label();
            lblStatus = new Label();
            lblTeacherName = new Label();
            lblValue = new Label();
            lblDuration = new Label();
            lblDescription = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtBoxCourseDescription = new Controls.CustomTextBox();
            txtBoxCourseName = new Controls.CustomTextBox();
            textBoxTeacherName = new Controls.CustomTextBox();
            cbStatus = new Controls.CustomComboBox();
            numericUpDownCourseDuration = new NumericUpDown();
            numericUpDownCourseValue = new NumericUpDown();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnSave = new Controls.Buttons();
            btnCancel = new Controls.Buttons();
            panelTop = new Panel();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCourseDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCourseValue).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblNameCourse
            // 
            lblNameCourse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblNameCourse.AutoSize = true;
            lblNameCourse.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblNameCourse.Location = new Point(56, 42);
            lblNameCourse.Name = "lblNameCourse";
            lblNameCourse.Size = new Size(108, 22);
            lblNameCourse.TabIndex = 0;
            lblNameCourse.Text = "Название:";
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblStatus.Location = new Point(69, 362);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(95, 22);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Активен:";
            // 
            // lblTeacherName
            // 
            lblTeacherName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTeacherName.AutoSize = true;
            lblTeacherName.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTeacherName.Location = new Point(3, 298);
            lblTeacherName.Name = "lblTeacherName";
            lblTeacherName.Size = new Size(161, 22);
            lblTeacherName.TabIndex = 2;
            lblTeacherName.Text = "Преподаватель:";
            // 
            // lblValue
            // 
            lblValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblValue.AutoSize = true;
            lblValue.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblValue.Location = new Point(99, 234);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(65, 22);
            lblValue.TabIndex = 3;
            lblValue.Text = "Цена:";
            // 
            // lblDuration
            // 
            lblDuration.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblDuration.AutoSize = true;
            lblDuration.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblDuration.Location = new Point(18, 170);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(146, 22);
            lblDuration.TabIndex = 4;
            lblDuration.Text = "Длительность:";
            // 
            // lblDescription
            // 
            lblDescription.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblDescription.Location = new Point(54, 106);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(110, 22);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Описание:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.875F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.125F));
            tableLayoutPanel1.Controls.Add(txtBoxCourseDescription, 1, 1);
            tableLayoutPanel1.Controls.Add(txtBoxCourseName, 1, 0);
            tableLayoutPanel1.Controls.Add(textBoxTeacherName, 1, 4);
            tableLayoutPanel1.Controls.Add(lblStatus, 0, 5);
            tableLayoutPanel1.Controls.Add(lblTeacherName, 0, 4);
            tableLayoutPanel1.Controls.Add(lblValue, 0, 3);
            tableLayoutPanel1.Controls.Add(lblDuration, 0, 2);
            tableLayoutPanel1.Controls.Add(lblDescription, 0, 1);
            tableLayoutPanel1.Controls.Add(lblNameCourse, 0, 0);
            tableLayoutPanel1.Controls.Add(cbStatus, 1, 5);
            tableLayoutPanel1.Controls.Add(numericUpDownCourseDuration, 1, 2);
            tableLayoutPanel1.Controls.Add(numericUpDownCourseValue, 1, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 6);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // txtBoxCourseDescription
            // 
            txtBoxCourseDescription.BackColor = Color.FromArgb(250, 0, 164);
            txtBoxCourseDescription.Dock = DockStyle.Bottom;
            txtBoxCourseDescription.Location = new Point(169, 106);
            txtBoxCourseDescription.Margin = new Padding(2, 0, 20, 0);
            txtBoxCourseDescription.Name = "txtBoxCourseDescription";
            txtBoxCourseDescription.Padding = new Padding(0, 0, 0, 1);
            txtBoxCourseDescription.Size = new Size(611, 22);
            txtBoxCourseDescription.TabIndex = 15;
            // 
            // txtBoxCourseName
            // 
            txtBoxCourseName.BackColor = Color.FromArgb(250, 0, 164);
            txtBoxCourseName.Dock = DockStyle.Bottom;
            txtBoxCourseName.Location = new Point(169, 42);
            txtBoxCourseName.Margin = new Padding(2, 0, 20, 0);
            txtBoxCourseName.Name = "txtBoxCourseName";
            txtBoxCourseName.Padding = new Padding(0, 0, 0, 1);
            txtBoxCourseName.Size = new Size(611, 22);
            txtBoxCourseName.TabIndex = 14;
            // 
            // textBoxTeacherName
            // 
            textBoxTeacherName.BackColor = Color.FromArgb(250, 0, 164);
            textBoxTeacherName.Dock = DockStyle.Bottom;
            textBoxTeacherName.Location = new Point(169, 298);
            textBoxTeacherName.Margin = new Padding(2, 0, 20, 0);
            textBoxTeacherName.Name = "textBoxTeacherName";
            textBoxTeacherName.Padding = new Padding(0, 0, 0, 1);
            textBoxTeacherName.Size = new Size(611, 22);
            textBoxTeacherName.TabIndex = 10;
            // 
            // cbStatus
            // 
            cbStatus.BackColor = Color.White;
            cbStatus.BorderColor = Color.FromArgb(250, 0, 164);
            cbStatus.BorderSize = 1;
            cbStatus.Dock = DockStyle.Bottom;
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cbStatus.ForeColor = Color.DimGray;
            cbStatus.IconColor = Color.FromArgb(250, 0, 164);
            cbStatus.ListBackColor = Color.White;
            cbStatus.ListTextColor = Color.White;
            cbStatus.Location = new Point(169, 352);
            cbStatus.Margin = new Padding(2, 0, 20, 0);
            cbStatus.MinimumSize = new Size(200, 30);
            cbStatus.Name = "cbStatus";
            cbStatus.Padding = new Padding(1);
            cbStatus.Size = new Size(611, 32);
            cbStatus.TabIndex = 11;
            cbStatus.Texts = "";
            // 
            // numericUpDownCourseDuration
            // 
            numericUpDownCourseDuration.BorderStyle = BorderStyle.FixedSingle;
            numericUpDownCourseDuration.Dock = DockStyle.Bottom;
            numericUpDownCourseDuration.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numericUpDownCourseDuration.Location = new Point(169, 163);
            numericUpDownCourseDuration.Margin = new Padding(2, 0, 20, 0);
            numericUpDownCourseDuration.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownCourseDuration.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownCourseDuration.Name = "numericUpDownCourseDuration";
            numericUpDownCourseDuration.Size = new Size(611, 29);
            numericUpDownCourseDuration.TabIndex = 12;
            numericUpDownCourseDuration.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericUpDownCourseValue
            // 
            numericUpDownCourseValue.BorderStyle = BorderStyle.FixedSingle;
            numericUpDownCourseValue.DecimalPlaces = 2;
            numericUpDownCourseValue.Dock = DockStyle.Bottom;
            numericUpDownCourseValue.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numericUpDownCourseValue.Location = new Point(169, 227);
            numericUpDownCourseValue.Margin = new Padding(2, 0, 20, 0);
            numericUpDownCourseValue.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownCourseValue.Name = "numericUpDownCourseValue";
            numericUpDownCourseValue.Size = new Size(611, 29);
            numericUpDownCourseValue.TabIndex = 13;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(btnSave, 0, 0);
            tableLayoutPanel2.Controls.Add(btnCancel, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(170, 387);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(627, 60);
            tableLayoutPanel2.TabIndex = 16;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnSave.BackColor = Color.FromArgb(250, 136, 230);
            btnSave.BackgroundColor = Color.FromArgb(250, 136, 230);
            btnSave.BorderColor = Color.PaleVioletRed;
            btnSave.BorderRadius = 20;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(62, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(188, 54);
            btnSave.TabIndex = 0;
            btnSave.Text = "Сохранить";
            btnSave.TextColor = Color.White;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnCancel.BackColor = Color.FromArgb(250, 136, 230);
            btnCancel.BackgroundColor = Color.FromArgb(250, 136, 230);
            btnCancel.BorderColor = Color.PaleVioletRed;
            btnCancel.BorderRadius = 20;
            btnCancel.BorderSize = 0;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Verdana", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(376, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(188, 54);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Отменить";
            btnCancel.TextColor = Color.White;
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // panelTop
            // 
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(800, 20);
            panelTop.TabIndex = 8;
            // 
            // AddEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(panelTop);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AddEditForm";
            Text = "AddEditForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCourseDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCourseValue).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblNameCourse;
        private Label lblStatus;
        private Label lblTeacherName;
        private Label lblValue;
        private Label lblDuration;
        private Label lblDescription;
        private TableLayoutPanel tableLayoutPanel1;
        private Controls.CustomTextBox textBoxTeacherName;
        private Controls.CustomComboBox cbStatus;
        private Controls.CustomTextBox txtBoxCourseDescription;
        private Controls.CustomTextBox txtBoxCourseName;
        private NumericUpDown numericUpDownCourseValue;
        private NumericUpDown numericUpDownCourseDuration;
        private TableLayoutPanel tableLayoutPanel2;
        private Controls.Buttons btnSave;
        private Controls.Buttons btnCancel;
        private Panel panelTop;
    }
}