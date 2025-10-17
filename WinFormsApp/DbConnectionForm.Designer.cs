namespace WinFormsApp
{
    partial class DbConnectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private RadioButton rbEF;
        private RadioButton rbDapper;
        private Button btnOk;
        private Button btnCancel;

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
            rbEF = new RadioButton();
            rbDapper = new RadioButton();
            btnOk = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // rbEF
            // 
            rbEF.Location = new Point(20, 20);
            rbEF.Name = "rbEF";
            rbEF.Size = new Size(159, 24);
            rbEF.TabIndex = 0;
            rbEF.Text = "EntityFrameWork";
            // 
            // rbDapper
            // 
            rbDapper.Location = new Point(20, 80);
            rbDapper.Name = "rbDapper";
            rbDapper.Size = new Size(104, 24);
            rbDapper.TabIndex = 1;
            rbDapper.Text = "Dapper";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(40, 130);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 32);
            btnOk.TabIndex = 2;
            btnOk.Text = "OK";
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(140, 130);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Отмена";
            btnCancel.Click += btnCancel_Click;
            // 
            // DbConnectionForm
            // 
            ClientSize = new Size(262, 191);
            Controls.Add(rbEF);
            Controls.Add(rbDapper);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Name = "DbConnectionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Выбор подключения";
            ResumeLayout(false);
        }

        #endregion
    }
}