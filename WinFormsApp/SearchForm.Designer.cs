namespace WinFormsApp
{
    partial class SearchForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblSearchText;
        private TextBox txtSearchText;
        private Label lblProperties;
        private CheckedListBox clbSearchProperties;
        private Button btnSearch;
        private DataGridView dataGridViewResults;
        private Label lblResults;
        private Button btnApply;
        private Button btnCancel;
        private Panel panelTop;
        private Panel panelMiddle;
        private Panel panelBottom;

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
            this.panelTop = new Panel();
            this.panelMiddle = new Panel();
            this.panelBottom = new Panel();
            this.lblSearchText = new Label();
            this.txtSearchText = new TextBox();
            this.lblProperties = new Label();
            this.clbSearchProperties = new CheckedListBox();
            this.btnSearch = new Button();
            this.dataGridViewResults = new DataGridView();
            this.lblResults = new Label();
            this.btnApply = new Button();
            this.btnCancel = new Button();

            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 100;
            this.panelTop.Padding = new Padding(10);

            this.lblSearchText.Text = "Текст для поиска:";
            this.lblSearchText.Location = new System.Drawing.Point(10, 15);
            this.lblSearchText.Size = new System.Drawing.Size(150, 20);

            this.txtSearchText.Location = new System.Drawing.Point(160, 12);
            this.txtSearchText.Size = new System.Drawing.Size(300, 25);
            this.txtSearchText.KeyPress += new KeyPressEventHandler(this.txtSearchText_KeyPress);

            this.lblProperties.Text = "Свойства для поиска:";
            this.lblProperties.Location = new System.Drawing.Point(10, 45);
            this.lblProperties.Size = new System.Drawing.Size(150, 20);

            this.clbSearchProperties.Location = new System.Drawing.Point(160, 45);
            this.clbSearchProperties.Size = new System.Drawing.Size(300, 45);
            this.clbSearchProperties.CheckOnClick = true;

            this.btnSearch.Text = "Поиск";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.Location = new System.Drawing.Point(470, 12);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.panelMiddle.Dock = DockStyle.Fill;
            this.panelMiddle.Padding = new Padding(10);

            this.dataGridViewResults.Dock = DockStyle.Fill;
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.lblResults.Text = "Введите критерии поиска и нажмите кнопку 'Поиск'";
            this.lblResults.Dock = DockStyle.Bottom;
            this.lblResults.Height = 25;
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Height = 50;
            this.panelBottom.Padding = new Padding(10);

            this.btnApply.Text = "Применить";
            this.btnApply.Size = new System.Drawing.Size(100, 30);
            this.btnApply.Location = new System.Drawing.Point(200, 10);
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Location = new System.Drawing.Point(310, 10);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.panelTop.Controls.Add(lblSearchText);
            this.panelTop.Controls.Add(txtSearchText);
            this.panelTop.Controls.Add(lblProperties);
            this.panelTop.Controls.Add(clbSearchProperties);
            this.panelTop.Controls.Add(btnSearch);

            this.panelMiddle.Controls.Add(dataGridViewResults);
            this.panelMiddle.Controls.Add(lblResults);

            this.panelBottom.Controls.Add(btnApply);
            this.panelBottom.Controls.Add(btnCancel);

            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Text = "Поиск курсов";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.MinimumSize = new System.Drawing.Size(600, 400);
        }
    }
}