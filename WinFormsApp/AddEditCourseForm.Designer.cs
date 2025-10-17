namespace WinFormsApp
{
    partial class AddEditCourseForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblName;
        private TextBox txtName;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblDuration;
        private NumericUpDown numDuration;
        private Label lblPrice;
        private NumericUpDown numPrice;
        private Label lblTeacher;
        private TextBox txtTeacher;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Button btnSave;
        private Button btnCancel;
        private TableLayoutPanel tableLayoutPanel;

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
            tableLayoutPanel = new TableLayoutPanel();
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblDuration = new Label();
            numDuration = new NumericUpDown();
            lblPrice = new Label();
            numPrice = new NumericUpDown();
            lblTeacher = new Label();
            txtTeacher = new TextBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel.Controls.Add(lblName, 0, 1);
            tableLayoutPanel.Controls.Add(txtName, 1, 1);
            tableLayoutPanel.Controls.Add(lblDescription, 0, 2);
            tableLayoutPanel.Controls.Add(txtDescription, 1, 2);
            tableLayoutPanel.Controls.Add(lblDuration, 0, 3);
            tableLayoutPanel.Controls.Add(numDuration, 1, 3);
            tableLayoutPanel.Controls.Add(lblPrice, 0, 4);
            tableLayoutPanel.Controls.Add(numPrice, 1, 4);
            tableLayoutPanel.Controls.Add(lblTeacher, 0, 5);
            tableLayoutPanel.Controls.Add(txtTeacher, 1, 5);
            tableLayoutPanel.Controls.Add(lblStatus, 0, 6);
            tableLayoutPanel.Controls.Add(cmbStatus, 1, 6);
            tableLayoutPanel.Dock = DockStyle.Top;
            tableLayoutPanel.Location = new Point(10, 10);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 8;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.Size = new Size(500, 350);
            tableLayoutPanel.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Location = new Point(3, 40);
            lblName.Name = "lblName";
            lblName.Size = new Size(144, 40);
            lblName.TabIndex = 2;
            lblName.Text = "Название:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Location = new Point(153, 45);
            txtName.Margin = new Padding(3, 5, 3, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(344, 27);
            txtName.TabIndex = 3;
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Location = new Point(3, 80);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(144, 60);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Описание:";
            lblDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Location = new Point(153, 85);
            txtDescription.Margin = new Padding(3, 5, 3, 5);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(344, 50);
            txtDescription.TabIndex = 5;
            // 
            // lblDuration
            // 
            lblDuration.Dock = DockStyle.Fill;
            lblDuration.Location = new Point(3, 140);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(144, 40);
            lblDuration.TabIndex = 6;
            lblDuration.Text = "Длительность (ч):";
            lblDuration.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numDuration
            // 
            numDuration.Dock = DockStyle.Fill;
            numDuration.Location = new Point(153, 145);
            numDuration.Margin = new Padding(3, 5, 3, 5);
            numDuration.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numDuration.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(344, 27);
            numDuration.TabIndex = 7;
            numDuration.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblPrice
            // 
            lblPrice.Dock = DockStyle.Fill;
            lblPrice.Location = new Point(3, 180);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(144, 40);
            lblPrice.TabIndex = 8;
            lblPrice.Text = "Цена (руб):";
            lblPrice.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Dock = DockStyle.Fill;
            numPrice.Location = new Point(153, 185);
            numPrice.Margin = new Padding(3, 5, 3, 5);
            numPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(344, 27);
            numPrice.TabIndex = 9;
            // 
            // lblTeacher
            // 
            lblTeacher.Dock = DockStyle.Fill;
            lblTeacher.Location = new Point(3, 220);
            lblTeacher.Name = "lblTeacher";
            lblTeacher.Size = new Size(144, 40);
            lblTeacher.TabIndex = 10;
            lblTeacher.Text = "Преподаватель:";
            lblTeacher.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTeacher
            // 
            txtTeacher.Dock = DockStyle.Fill;
            txtTeacher.Location = new Point(153, 225);
            txtTeacher.Margin = new Padding(3, 5, 3, 5);
            txtTeacher.Name = "txtTeacher";
            txtTeacher.Size = new Size(344, 27);
            txtTeacher.TabIndex = 11;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(3, 260);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(144, 40);
            lblStatus.TabIndex = 12;
            lblStatus.Text = "Активен:";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbStatus
            // 
            cmbStatus.Dock = DockStyle.Fill;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(153, 265);
            cmbStatus.Margin = new Padding(3, 5, 3, 5);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(344, 28);
            cmbStatus.TabIndex = 13;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(150, 370);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 35);
            btnSave.TabIndex = 1;
            btnSave.Text = "Сохранить";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(280, 370);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 35);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Отмена";
            btnCancel.Click += btnCancel_Click;
            // 
            // AddEditCourseForm
            // 
            AcceptButton = btnSave;
            CancelButton = btnCancel;
            ClientSize = new Size(520, 420);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(tableLayoutPanel);
            Name = "AddEditCourseForm";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterParent;
            Load += AddEditCourseForm_Load;
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ResumeLayout(false);
        }
    }
}