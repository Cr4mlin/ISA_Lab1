namespace WinFormsApp
{
    partial class PriceRangeForm
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
            this.numMinPrice = new NumericUpDown();
            this.label2 = new Label();
            this.numMaxPrice = new NumericUpDown();
            this.btnSearch = new Button();
            this.btnClose = new Button();
            this.dataGridViewPriceRange = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)this.numMinPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numMaxPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dataGridViewPriceRange).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(119, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Минимальная цена:";
            // 
            // numMinPrice
            // 
            this.numMinPrice.DecimalPlaces = 2;
            this.numMinPrice.Location = new Point(137, 12);
            this.numMinPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numMinPrice.Name = "numMinPrice";
            this.numMinPrice.Size = new Size(120, 23);
            this.numMinPrice.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(123, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Максимальная цена:";
            // 
            // numMaxPrice
            // 
            this.numMaxPrice.DecimalPlaces = 2;
            this.numMaxPrice.Location = new Point(137, 41);
            this.numMaxPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numMaxPrice.Name = "numMaxPrice";
            this.numMaxPrice.Size = new Size(120, 23);
            this.numMaxPrice.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new Point(12, 70);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(100, 30);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Поиск";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += btnSearch_Click;
            // 
            // btnClose
            // 
            this.btnClose.Location = new Point(118, 70);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(100, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += btnClose_Click;
            // 
            // dataGridViewPriceRange
            // 
            this.dataGridViewPriceRange.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPriceRange.Location = new Point(12, 106);
            this.dataGridViewPriceRange.Name = "dataGridViewPriceRange";
            this.dataGridViewPriceRange.RowTemplate.Height = 25;
            this.dataGridViewPriceRange.Size = new Size(776, 332);
            this.dataGridViewPriceRange.TabIndex = 6;
            // 
            // PriceRangeForm
            // 
            this.AutoScaleDimensions = new Size(7, 15);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.dataGridViewPriceRange);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.numMaxPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numMinPrice);
            this.Controls.Add(this.label1);
            this.Name = "PriceRangeForm";
            this.Text = "PriceRangeForm";
            ((System.ComponentModel.ISupportInitialize)this.numMinPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numMaxPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dataGridViewPriceRange).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown numMinPrice;
        private Label label2;
        private NumericUpDown numMaxPrice;
        private Button btnSearch;
        private Button btnClose;
        private DataGridView dataGridViewPriceRange;
    }
}
