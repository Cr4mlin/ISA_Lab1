namespace WinFormsApp
{
    partial class CourseForm
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
            this.label1 = new Label();
            this.txtName = new TextBox();
            this.label2 = new Label();
            this.txtId = new TextBox();
            this.label3 = new Label();
            this.txtDescription = new TextBox();
            this.label4 = new Label();
            this.numDuration = new NumericUpDown();
            this.label5 = new Label();
            this.numPrice = new NumericUpDown();
            this.label6 = new Label();
            this.txtTeacherName = new TextBox();
            this.cbIsActive = new CheckBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)this.numDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numPrice).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название:";
            // 
            // txtName
            // 
            this.txtName.Location = new Point(100, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(200, 23);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(21, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID:";
            // 
            // txtId
            // 
            this.txtId.Location = new Point(100, 41);
            this.txtId.Name = "txtId";
            this.txtId.Size = new Size(200, 23);
            this.txtId.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new Size(65, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Описание:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new Point(100, 70);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(200, 60);
            this.txtDescription.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 140);
            this.label4.Name = "label4";
            this.label4.Size = new Size(79, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Длительность:";
            // 
            // numDuration
            // 
            this.numDuration.Location = new Point(100, 138);
            this.numDuration.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new Size(200, 23);
            this.numDuration.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 169);
            this.label5.Name = "label5";
            this.label5.Size = new Size(38, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Цена:";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new Point(100, 167);
            this.numPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new Size(200, 23);
            this.numPrice.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new Point(12, 198);
            this.label6.Name = "label6";
            this.label6.Size = new Size(89, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Преподаватель:";
            // 
            // txtTeacherName
            // 
            this.txtTeacherName.Location = new Point(100, 195);
            this.txtTeacherName.Name = "txtTeacherName";
            this.txtTeacherName.Size = new Size(200, 23);
            this.txtTeacherName.TabIndex = 11;
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Location = new Point(100, 224);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new Size(77, 19);
            this.cbIsActive.TabIndex = 12;
            this.cbIsActive.Text = "Активный";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new Point(12, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new Point(120, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += btnCancel_Click;
            // 
            // CourseForm
            // 
            this.AutoScaleDimensions = new Size(7, 15);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(314, 292);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.txtTeacherName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numDuration);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "CourseForm";
            this.Text = "CourseForm";
            ((System.ComponentModel.ISupportInitialize)this.numDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numPrice).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtName;
        private Label label2;
        private TextBox txtId;
        private Label label3;
        private TextBox txtDescription;
        private Label label4;
        private NumericUpDown numDuration;
        private Label label5;
        private NumericUpDown numPrice;
        private Label label6;
        private TextBox txtTeacherName;
        private CheckBox cbIsActive;
        private Button btnSave;
        private Button btnCancel;
    }
}
