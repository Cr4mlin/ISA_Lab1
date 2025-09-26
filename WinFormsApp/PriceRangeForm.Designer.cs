namespace WinFormsApp
{
    partial class PriceRangeForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblMinPrice;
        private NumericUpDown numMinPrice;
        private Label lblMaxPrice;
        private NumericUpDown numMaxPrice;
        private Button btnFilter;
        private DataGridView dataGridViewResults;
        private Label lblResults;
        private Button btnApply;
        private Button btnCancel;
        private Panel panelTop;
        private Panel panelMiddle;
        private Panel panelBottom;
        private GroupBox groupBoxPrice;

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
            this.groupBoxPrice = new GroupBox();
            this.lblMinPrice = new Label();
            this.numMinPrice = new NumericUpDown();
            this.lblMaxPrice = new Label();
            this.numMaxPrice = new NumericUpDown();
            this.btnFilter = new Button();
            this.dataGridViewResults = new DataGridView();
            this.lblResults = new Label();
            this.btnApply = new Button();
            this.btnCancel = new Button();

            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 100;
            this.panelTop.Padding = new Padding(10);

            this.groupBoxPrice.Text = "Укажите диапазон цен";
            this.groupBoxPrice.Dock = DockStyle.Fill;
            this.groupBoxPrice.Padding = new Padding(10);

            this.lblMinPrice.Text = "Минимальная цена:";
            this.lblMinPrice.Location = new System.Drawing.Point(10, 25);
            this.lblMinPrice.Size = new System.Drawing.Size(120, 20);
            this.lblMinPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.numMinPrice.Location = new System.Drawing.Point(140, 22);
            this.numMinPrice.Size = new System.Drawing.Size(120, 25);
            this.numMinPrice.DecimalPlaces = 2;
            this.numMinPrice.Minimum = 0;
            this.numMinPrice.Maximum = 1000000;
            this.numMinPrice.Increment = 100;
            this.numMinPrice.ValueChanged += new System.EventHandler(this.numMinPrice_ValueChanged);

            this.lblMaxPrice.Text = "Максимальная цена:";
            this.lblMaxPrice.Location = new System.Drawing.Point(10, 55);
            this.lblMaxPrice.Size = new System.Drawing.Size(120, 20);
            this.lblMaxPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.numMaxPrice.Location = new System.Drawing.Point(140, 52);
            this.numMaxPrice.Size = new System.Drawing.Size(120, 25);
            this.numMaxPrice.DecimalPlaces = 2;
            this.numMaxPrice.Minimum = 0;
            this.numMaxPrice.Maximum = 1000000;
            this.numMaxPrice.Increment = 100;
            this.numMaxPrice.Value = 50000;
            this.numMaxPrice.ValueChanged += new System.EventHandler(this.numMaxPrice_ValueChanged);

            this.btnFilter.Text = "Применить фильтр";
            this.btnFilter.Size = new System.Drawing.Size(140, 35);
            this.btnFilter.Location = new System.Drawing.Point(280, 30);
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

            this.groupBoxPrice.Controls.Add(lblMinPrice);
            this.groupBoxPrice.Controls.Add(numMinPrice);
            this.groupBoxPrice.Controls.Add(lblMaxPrice);
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

            this.groupBoxPrice.Controls.Add(lblMinPrice);
            this.groupBoxPrice.Controls.Add(numMinPrice);
            this.groupBoxPrice.Controls.Add(lblMaxPrice);
            this.groupBoxPrice.Controls.Add(numMaxPrice);
            this.groupBoxPrice.Controls.Add(btnFilter);

            this.panelTop.Controls.Add(groupBoxPrice);

            this.panelMiddle.Dock = DockStyle.Fill;
            this.panelMiddle.Padding = new Padding(10);

            this.dataGridViewResults.Dock = DockStyle.Fill;
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.lblResults.Text = "Укажите диапазон цен и нажмите кнопку 'Применить фильтр'";
            this.lblResults.Dock = DockStyle.Bottom;
            this.lblResults.Height = 25;
            this.lblResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblResults.Padding = new Padding(0, 5, 0, 0);

            this.panelMiddle.Controls.Add(dataGridViewResults);
            this.panelMiddle.Controls.Add(lblResults);

            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Height = 60;
            this.panelBottom.Padding = new Padding(10);

            this.btnApply.Text = "Применить результаты";
            this.btnApply.Size = new System.Drawing.Size(150, 35);
            this.btnApply.Location = new System.Drawing.Point(200, 12);
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            this.btnCancel.Text = "Отмена";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Location = new System.Drawing.Point(360, 12);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.panelBottom.Controls.Add(btnApply);
            this.panelBottom.Controls.Add(btnCancel);

            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Text = "Фильтр курсов по цене";
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = new System.Drawing.Size(600, 400);
        }
    }
}